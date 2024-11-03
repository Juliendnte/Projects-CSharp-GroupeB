const dayNames = ["Dim", "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam"];
const monthNames = ["Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"];
let currentDate = new Date();
const monthYear = document.getElementById("monthYear");
const calendar = document.getElementById("calendar");

const events = [];

fetch("Gestion/GetTache/", {
    method: 'GET',
    headers: {
        'Content-Type': 'application/json',
    }})
    .then(r => r.json())
    .then(data => {
        data.forEach(tache => {
            events.push({id:tache.Id, title: tache.Titre, description:tache.Description, startDate: tache.date_debut, endDate: tache.date_fin });
        });
        renderCalendar();
    });

function startOfMonth(year, month) {
    return new Date(year, month, 1);
}

function endOfMonth(year, month) {
    return new Date(year, month + 1, 0);
}

function startOfWeek(date) {
    const day = date.getDay();
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() - day);
}

function endOfWeek(date) {
    const day = date.getDay();
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() + (6 - day));
}

function datesBetween(start, end) {
    const dates = [];
    let current = new Date(start);
    while (current <= end) {
        dates.push(new Date(current));
        current.setDate(current.getDate() + 1);
    }
    return dates;
}

function indexEventsByDay(events) {
    const eventsByDay = {};
    events.forEach(event => {
        let currentDate = new Date(event.startDate);
        const endDate = new Date(event.endDate);
        while (currentDate <= endDate) {
            const dayKey = currentDate.toISOString().split('T')[0];
            if (!eventsByDay[dayKey]) eventsByDay[dayKey] = [];
            eventsByDay[dayKey].push(event);
            currentDate.setDate(currentDate.getDate() + 1);
        }
    });
    return eventsByDay;
}

function formatTimeDifference(date1, date2) {
    const timestamp1 = new Date(date1).getTime();
    const timestamp2 = new Date(date2).getTime();

    const minutes = Math.floor(Math.abs(timestamp2 - timestamp1) / 60000);
    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;

    let result = '';
    if (hours) {
        result += `${hours}h`;
    }
    if (remainingMinutes || !hours) {
        result += `${remainingMinutes}min`;
    }

    return result;
}

function getDayName(dateString, useUTC = false) {
    const date = new Date(dateString);
    const dayIndex = useUTC ? date.getUTCDay() : date.getDay();
    return dayNames[dayIndex];
}

function renderCalendar() {
    const year = currentDate.getFullYear();
    const month = currentDate.getMonth();
    const startDate = startOfWeek(startOfMonth(year, month));
    const endDate = endOfWeek(endOfMonth(year, month));
    const dates = datesBetween(startDate, endDate);
    let check = true
    
    const eventsByDay = indexEventsByDay(events);

    monthYear.textContent = `${monthNames[month]} ${year}`;
    calendar.innerHTML = "";

    const today = new Date();

    dates.forEach(date => {
        const dayKey = date.toISOString().split('T')[0];
        const dayCell = document.createElement("div");
        const numberSpan = document.createElement("span");
        dayCell.classList.add("day-cell");
        numberSpan.textContent = date.getDate();
        
        if (date.getDate() === today.getDate() && date.getMonth() === today.getMonth() && date.getFullYear() === today.getFullYear()) {
            numberSpan.classList.add("today");
            dayCell.classList.add("selected")
            detail(date, eventsByDay[dayKey]);
            check = false
        }else if (date.getDate() === 1 && check){
            detail(date, eventsByDay[dayKey]);
            check = false
        }
        dayCell.appendChild(numberSpan);

        if (eventsByDay[dayKey]) {
            eventsByDay[dayKey].forEach(event => {
                const eventElement = document.createElement("div");
                eventElement.classList.add("event");

                const startDate = new Date(event.startDate);
                const endDate = new Date(event.endDate);
                
                if (startDate.toDateString() !== endDate.toDateString()) {
                    if (dayKey === event.startDate.split('T')[0]) {
                        const formattedTime = startDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
                        eventElement.textContent = `${event.title} - ${formattedTime}`;
                        eventElement.classList.add("event-start");
                    } else if (dayKey === event.endDate.split('T')[0]) {
                        const formattedEndTime = endDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
                        eventElement.textContent = `${event.title} - Jusqu'à ${formattedEndTime}`;
                        eventElement.classList.add("event-end");
                    } else {
                        eventElement.textContent = event.title;
                        eventElement.classList.add("event-continued");
                    }
                } else {
                    const formattedTime = startDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
                    eventElement.textContent = `${event.title} - ${formattedTime}`;
                }

                dayCell.appendChild(eventElement);
            });
        }
        dayCell.addEventListener("click", ()=> detail(date, eventsByDay[dayKey]));
        dayCell.addEventListener("click", ()=> {
            document.querySelectorAll('.day-cell').forEach(c => c.classList.remove('selected'));
            dayCell.classList.add('selected');
            if (eventsByDay[dayKey]){
                console.log(formatTimeDifference(eventsByDay[dayKey][0].startDate , eventsByDay[dayKey][0].endDate))
            }
        });
        calendar.appendChild(dayCell);
    });
}

function detail(date, event = null){
    document.getElementById("date-selected").innerHTML = getDayName(date) + ", "+monthNames[date.getMonth()].slice(0, 3)+ " " + date.getDate().toString()
    document.querySelector(".detail-body").innerHTML = "";
    if (event){
        document.querySelector(".detail-body").innerHTML = `<h3>${event[0].title}</h3>
                 <div>${event[0].description}</div>
`;
    }
}

document.getElementById("prevMonth").addEventListener("click", () => {
    currentDate.setMonth(currentDate.getMonth() - 1);
    renderCalendar();
});

document.getElementById("nextMonth").addEventListener("click", () => {
    currentDate.setMonth(currentDate.getMonth() + 1);
    renderCalendar();
});

