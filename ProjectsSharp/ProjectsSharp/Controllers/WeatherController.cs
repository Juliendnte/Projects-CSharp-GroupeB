using Microsoft.AspNetCore.Mvc;
using ProjectsSharp.Models;
using ProjectsSharp.Service;

namespace ProjectsSharp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherModel _weatherModel;

        public WeatherController()
        {
            _weatherModel = new WeatherModel();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Weather/GetWeather")]
        public async Task<IActionResult> GetWeather([FromBody] WeatherRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.City))
            {
                return Json(new { error = "Veuillez entrer un nom de ville." });
            }

            WeatherData weatherData = await _weatherModel.GetWeatherAsync(request.City);

            if (weatherData == null || weatherData.CityName == "Erreur")
            {
                return Json(new { error = weatherData?.Description ?? "Erreur inconnue." });
            }

            return Json(weatherData); 
        }



        

    }
}