const dayNames = ["Dim", "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam"];
const monthNames = ["Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"];
let currentDate = new Date();
const monthYear = document.getElementById("monthYear");
const calendar = document.getElementById("calendar");
const overlay = document.querySelector("#overlay");

const events = [];

const getData = () => {
    fetch("Gestion/GetTache/",
{
    method: 'GET',
        headers
:
    {
        'Content-Type'
    :
        'application/json',
    }
}
)
.
then(response => {
    if (!response.ok) {
        return response.json().then(err => {
            throw err
        });
    }
    return response.json();
})
    .then(data => {
        data.forEach(tache => {
            events.push({
                id: tache.Id,
                title: tache.Titre,
                description: tache.Description,
                startDate: tache.Date_debut,
                endDate: tache.Date_fin
            });
        });
        renderCalendar();
    })
    .catch(err => {
        console.error(err);
        document.querySelector(".body").innerHTML = err.message || "An unexpected error occurred.";
    });
}

function startOfMonth(year, month) {
    return new Date(year, month, 1);
}

function endOfMonth(year, month) {
    return new Date(year, month + 1, 0);
}

function startOfWeek(date) {
    const day = date.getDay();
    const diff = day === 0 ? 0 : -day;
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() + diff);
}

function endOfWeek(date) {
    const day = date.getDay();
    const diff = day === 0 ? 6 : 6 - day;
    return new Date(date.getFullYear(), date.getMonth(), date.getDate() + diff);
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

            eventsByDay[dayKey].push(event.id);
            currentDate.setDate(currentDate.getDate() + 1);
        }
    });
    return eventsByDay;
}

function indexEventsById(events) {
    const eventsById = {};
    events.forEach(event => {
        eventsById[event.id] = event;
    });
    return eventsById;
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

function formatTime(dateString) {
    const date = new Date(dateString);

    let hours = date.getHours();
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const period = hours >= 12 ? "PM" : "AM";
    
    return `${hours % 12 || 12}:${minutes} ${period}`;
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
    const eventsByDay = indexEventsByDay(events);
    const eventsById = indexEventsById(events);

    let check = true
    

    monthYear.textContent = `${monthNames[month]} ${year}`;
    calendar.innerHTML = "";

    const today = new Date();
    
    dates.forEach(date => {
        function getLocalDateKey(date) {
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');  
            const day = String(date.getDate()).padStart(2, '0');

            return `${year}-${month}-${day}`;
        }
        const dayKey = getLocalDateKey(date);
        const dayCell = document.createElement("div");
        const numberSpan = document.createElement("span");
        dayCell.classList.add("day-cell");
        numberSpan.textContent = date.getDate();
        
        if (date.getDate() === today.getDate() && date.getMonth() === today.getMonth() && date.getFullYear() === today.getFullYear()) {
            numberSpan.classList.add("today");
            dayCell.classList.add("selected")
            document.querySelectorAll('.day-cell').forEach(c => c.classList.remove('selected'));
            detail(date, eventsByDay[dayKey]);
            check = false
        }else if (date.getDate() === 1 && check){
            detail(date, eventsByDay[dayKey]);
            dayCell.classList.add("selected")
            check = false
        }
        dayCell.appendChild(numberSpan);
        if (eventsByDay[dayKey]) {
            eventsByDay[dayKey].forEach(eventId => {
                const event = eventsById[eventId];
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

                eventElement.addEventListener("dblclick", () => {
                    document.querySelector(".more-detail").classList.add("show-detail");
                    document.querySelector("#title-detail").value = event.title;
                    document.querySelector("#description-detail").value = event.description;
                    document.querySelector("#began-time-detail").value = new Date(event.startDate).toISOString().slice(0, 16);
                    document.querySelector("#end-time-detail").value = new Date(event.endDate).toISOString().slice(0, 16);

                    document.querySelector(".more-detail-header .save").addEventListener("click", ()=> update(event.id));
                    document.querySelector(".more-detail-header .discard").addEventListener("click", ()=> del(event.id));
                    
                    overlay.style.display = "block";
                });

                dayCell.appendChild(eventElement);
            });
        }
        
        dayCell.addEventListener("dblclick", (ev)=> {
            ev.stopImmediatePropagation();
            document.querySelector(".create").classList.add("show-detail")
            date.setDate(date.getDate() + 1)
            document.querySelector("#began-time-create").value = date.toISOString().slice(0, 16);
            let plustard = date
            plustard.setMinutes(plustard.getMinutes() + 30)

            document.querySelector("#end-time-create").value = plustard.toISOString().slice(0, 16);
            document.querySelector(".create-header .save").addEventListener("click", create);
            overlay.style.display = "block";
        });
        
        dayCell.addEventListener("click", ()=> detail(date, eventsByDay[dayKey]));
        dayCell.addEventListener("click", ()=> {
            document.querySelectorAll('.day-cell').forEach(c => c.classList.remove('selected'));
            dayCell.classList.add('selected');
        });
        calendar.appendChild(dayCell);
    });
    overlay.addEventListener("click", ()=> {
        overlay.style.display = "none";
        document.querySelector(".more-detail").classList.remove("show-detail")
        document.querySelector(".create").classList.remove("show-detail")
    });
}

function detail(date, eventByDay = null){
    document.getElementById("date-selected").innerHTML = getDayName(date) + ", "+monthNames[date.getMonth()].slice(0, 3)+ " " + date.getDate().toString()
    document.querySelector(".detail-body").innerHTML = "";
    const eventsById = indexEventsById(events);

    if (eventByDay){
        document.querySelector(".detail-body").innerHTML = "";
        eventByDay.forEach((eventId) => {
            const event = eventsById[eventId];
            document.querySelector(".detail-body").innerHTML += `<div class="ctn-detail">
            <div class="ctn-hour"><div class="began-hour">${formatTime(event.startDate)}</div><div class="time">${formatTimeDifference(event.startDate, event.endDate)}</div></div
    ><div class="ctn-title"><img src="img/file.png" alt="title" style="
        width: 20px;
        height: 20px;
    "/><h5>${event.title}</h5></div></div>`;
        })
    }
}

function update(id) {
    const Titre = document.querySelector("#title-detail").value;
    const Description = document.querySelector("#description-detail").value;
    const beganTime = new Date(document.querySelector("#began-time-detail").value).toISOString(); 
    const endTime = new Date(document.querySelector("#end-time-detail").value).toISOString();


    fetch(`/UpdateTache/${id}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Titre,            
            Description,
            Date_debut: beganTime,
            Date_fin: endTime
        })
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(err => { throw new Error(err || "An error occurred"); });
            }
            return response.json();
        })
        .catch(err => {
            console.error("Fetch error:", err);
        });
    overlay.style.display = "none";
    document.querySelector(".more-detail").classList.remove("show-detail");
    document.querySelector(".create").classList.remove("show-detail");
    getData();
    location.reload();
}

function del(id) {
    fetch(`/DeleteTache/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(err => { throw new Error(err || "An error occurred"); });
            }
            return response.json();
        })
        .catch(err => {
            console.error("Fetch error:", err);
        });
    overlay.style.display = "none";
    document.querySelector(".more-detail").classList.remove("show-detail");
    document.querySelector(".create").classList.remove("show-detail");
    getData();
    location.reload();
}

function create() {
    const Titre = document.querySelector("#title-create").value;
    const Description = document.querySelector("#description-create").value;
    const beganTime = new Date(document.querySelector("#began-time-create").value).toISOString();
    const endTime = new Date(document.querySelector("#end-time-create").value).toISOString();
 
    fetch("/CreateTache", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Titre,
            Description,
            Date_debut: beganTime,
            Date_fin: endTime
        })
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(err => { throw new Error(err || "An error occurred"); });
            }
            return response.json();
        })
        .catch(err => {
            console.error("Fetch error:", err);
        });
    
    overlay.style.display = "none";
    document.querySelector(".more-detail").classList.remove("show-detail");
    document.querySelector(".create").classList.remove("show-detail");
    getData();
    location.reload();
}

document.getElementById("prevMonth").addEventListener("click", () => {
    currentDate.setMonth(currentDate.getMonth() - 1);
    getData();
});

document.getElementById("nextMonth").addEventListener("click", () => {
    currentDate.setMonth(currentDate.getMonth() + 1);
    getData();
});

getData()