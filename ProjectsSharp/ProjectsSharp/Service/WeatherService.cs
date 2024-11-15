using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ProjectsSharp.Models;

namespace ProjectsSharp.Service
{
    public class WeatherService
    {
        private readonly string _apiKey = "338b9134b4b0c86afe88c97b28648d6c";
        private readonly HttpClient _client = new HttpClient();

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=fr";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(jsonString);

                var weatherData = new WeatherData
                {
                    CityName = jsonDoc.RootElement.GetProperty("name").GetString(),
                    Temperature = jsonDoc.RootElement.GetProperty("main").GetProperty("temp").GetDouble(),
                    Humidity = jsonDoc.RootElement.GetProperty("main").GetProperty("humidity").GetInt32(),
                    Description = jsonDoc.RootElement.GetProperty("weather")[0].GetProperty("description").GetString()
                };

                return weatherData;
            }
            catch (HttpRequestException ex)
            {
                return new WeatherData
                {
                    CityName = "Erreur",
                    Description = "Erreur lors de la requête HTTP : " + ex.Message
                };
            }
        }
    }
}