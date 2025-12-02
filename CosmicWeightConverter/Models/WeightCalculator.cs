namespace CosmicWeightConverter.Models
{
    public class WeightCalculator
    {
        // Now using the full solar system gravity factors
        private static readonly Dictionary<string, double> GravityFactors = new()
        {
            { "Mercury", 0.38 },
            { "Venus", 0.91 },
            { "Earth", 1.00 }, // Earth is included for reference
            { "Mars", 0.38 },
            { "Jupiter", 2.34 },
            { "Saturn", 1.06 },
            { "Uranus", 0.92 },
            { "Neptune", 1.19 },
            { "Pluto", 0.06 },
            { "Moon", 0.165 },
            { "Sun", 27.01 }
        };

        // This property *must* be in kilograms for the calculation to be correct
        public double EarthWeight { get; set; }

        public Dictionary<string, double> CalculateCosmicWeights()
        {
            var results = new Dictionary<string, double>();
            foreach (var planet in GravityFactors)
            {
                // This logic is simple because EarthWeight is always in KG
                double cosmicWeight = EarthWeight * planet.Value;
                results.Add(planet.Key, Math.Round(cosmicWeight, 2));
            }
            return results;
        }
    }
}