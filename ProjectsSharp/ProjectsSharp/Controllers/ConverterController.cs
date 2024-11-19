using Microsoft.AspNetCore.Mvc;
using ProjectsSharp.Models;


namespace ProjectsSharp.Controllers
{
    public class ConverterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result(string conversionType, double value)
        {
            double result = conversionType switch
            {
                "Currency" => value * 0.85,         // USD -> EUR
                "CurrencyInverse" => value / 0.85, // EUR -> USD
                "Distance" => value * 1.60934,     // Miles -> Km
                "DistanceInverse" => value / 1.60934, // Km -> Miles
                "Weight" => value * 0.453592,      // Lbs -> Kg
                "WeightInverse" => value / 0.453592, // Kg -> Lbs
                _ => 0
            };

            var model = new ConverterModel
            {
                ConversionType = conversionType,
                InputValue = value,
                ConvertedValue = result
            };

            return View(model);
        }
    }
}
