using System;
using System.Collections.Generic;

namespace CalculatriceApp.Models
{
    public class CalculatriceModel
    {
        public double? CurrentValue { get; set; }  // Nullable pour éviter CS8625
        public string? Operation { get; set; }  // Nullable pour éviter CS8618
        public double? PreviousValue { get; set; }  // Nullable pour éviter CS8625
        public List<string> History { get; set; } = new List<string>(); // Initialisation pour éviter CS8618

        public void Clear()
        {
            CurrentValue = null;
            PreviousValue = null;
            Operation = null;
            History.Clear();
        }

        public double? Calculate()
        {
            double? result = null;
            if (PreviousValue.HasValue && CurrentValue.HasValue)
            {
                switch (Operation)
                {
                    case "+":
                        result = PreviousValue + CurrentValue;
                        break;
                    case "-":
                        result = PreviousValue - CurrentValue;
                        break;
                    case "*":
                        result = PreviousValue * CurrentValue;
                        break;
                    case "/":
                        result = CurrentValue != 0 ? PreviousValue / CurrentValue : null;
                        break;
                    case "sqrt":
                        result = Math.Sqrt(CurrentValue.Value);
                        break;
                    case "square":
                        result = Math.Pow(CurrentValue.Value, 2);
                        break;
                    case "inverse":
                        result = CurrentValue != 0 ? 1 / CurrentValue : null;
                        break;
                    case "%":
                        result = PreviousValue * (CurrentValue / 100);
                        break;
                }

                if (result.HasValue)
                {
                    History.Add($"{PreviousValue} {Operation} {CurrentValue} = {result}");
                    PreviousValue = result;
                    CurrentValue = null;
                }
            }
            return result;
        }
    }
}
