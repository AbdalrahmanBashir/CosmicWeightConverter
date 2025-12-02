namespace CosmicWeightConverter.ViewModels
{
    public class CalculationResultViewModel
    {
        // The weight the user originally entered
        public double InputWeight { get; set; }

        // The unit the user selected
        public  string ResultUnit { get; set; }

        // The calculated weights
        public  Dictionary<string, double> CosmicWeights { get; set; }
    }
}
