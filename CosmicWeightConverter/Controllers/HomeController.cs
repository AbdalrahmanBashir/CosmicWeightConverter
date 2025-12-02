using CosmicWeightConverter.Models;
using CosmicWeightConverter.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmicWeightConverter.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        // Conversion factor: 1 kg ≈ 2.20462 lbs
        private const double PoundsPerKilogram = 2.20462;

        
        public IActionResult Index()
        {
            // Pass an empty input ViewModel for the form to use
            return View(new CalculationInputViewModel());
        }

        
        [HttpPost]
        public IActionResult Calculate(CalculationInputViewModel input)
        {
            // Check if the input is valid
            if (!ModelState.IsValid || input.InputWeight <= 0 || (input.Unit != "kg" && input.Unit != "lb"))
            {
                // Add a generic error for the user
                ModelState.AddModelError("", "Please enter a valid weight greater than zero and select a unit.");
                return View("Index", input);
            }

            // Conversion Logic
            double earthWeightKg;
            if (input.Unit == "lb")
            {
                // Convert pounds to kilograms for the Model calculation
                earthWeightKg = input.InputWeight / PoundsPerKilogram;
                _logger.LogInformation("Converted {0} lb to {1} kg.", input.InputWeight, earthWeightKg);
            }
            else // "kg"
            {
                earthWeightKg = input.InputWeight;
            }
            

            // A. Controller interacts with the Model
            var calculator = new WeightCalculator { EarthWeight = earthWeightKg };
            var cosmicResults = calculator.CalculateCosmicWeights();

            // B. Controller creates and populates the strongly-typed ViewModel for results
            var viewModel = new CalculationResultViewModel
            {
                // The results need to be converted back to the user's original unit
                InputWeight = input.InputWeight,
                ResultUnit = input.Unit,
                CosmicWeights = []
            };

            // C. Convert the calculated results back to the user's selected unit
            foreach (var result in cosmicResults)
            {
                double finalWeight = result.Value;
                if (input.Unit == "lb")
                {
                    // Convert kg results back to lbs
                    finalWeight *= PoundsPerKilogram;
                }
                viewModel.CosmicWeights.Add(result.Key, Math.Round(finalWeight, 2));
            }


            // D. Controller passes the ViewModel to the Results View
            return View("Results", viewModel);
        }
    }
}