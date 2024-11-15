async function getWeather() {
    console.log("Fonction getWeather appelée"); 
    const city = document.getElementById('city').value;

    if (!city) {
        alert("Veuillez entrer une ville.");
        return;
    }

    try {
        const response = await fetch("/Weather/GetWeather", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ city: city })
        });

        const weatherData = await response.json();

        if (weatherData.error) {
            document.getElementById('weatherResult').innerHTML = `<p>${weatherData.error}</p>`;
        } else {
            displayWeather(weatherData);
        }
    } catch (error) {
        document.getElementById('weatherResult').innerHTML = `<p>Erreur : ${error.message}</p>`;
    }
}

function displayWeather(data) {
    const weatherDiv = document.getElementById('weatherResult');
    weatherDiv.innerHTML = `
        <h2>Météo pour ${data.cityName}</h2> 
        <p>Température : ${data.temperature}°C</p>
        <p>Humidité : ${data.humidity}%</p>
        <p>Conditions : ${data.description}</p>
    `;
}



