using Microsoft.AspNetCore.Mvc;
using CalculatriceApp.Models;
using System.Diagnostics;

namespace CalculatriceApp.Controllers
{
    public class CalculatriceController : Controller
    {
        private static CalculatriceModel _model = new CalculatriceModel();

        public IActionResult Index()
        {
            return View(_model);
        }

        [HttpPost]
        public IActionResult Update(string operation, double? value)
        {
            Console.WriteLine("Méthode Update appelée"); // Journalisation pour débogage
            Debug.WriteLine($"Operation reçue : {operation}, Valeur reçue : {value}"); // Autre journalisation

            if (operation == "clear")
            {
                _model.Clear();
            }
            else if (operation == "=")
            {
                _model.CurrentValue = _model.Calculate();
                _model.Operation = null;
            }
            else if (!string.IsNullOrEmpty(operation))
            {
                _model.PreviousValue = _model.CurrentValue;
                _model.Operation = operation;
                _model.CurrentValue = null;
            }
            else if (value.HasValue)
            {
                _model.CurrentValue = (_model.CurrentValue ?? 0) * 10 + value;
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Test()
        {
            return Content("Le contrôleur fonctionne correctement");
        }

    }
}