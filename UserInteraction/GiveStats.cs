using ApiReader.DataModels;

namespace ApiReader.UserInteraction
{
    public class GiveStats
    {
        private readonly PlanetPropertiesConversion _planetPropertiesConverted;
        public void GetMinAndMax(string userInput, PlanetPropertiesConversion _planetPropertiesConversion)
        {
            if(userInput == "diameter")
            {
                var max  = _planetPropertiesConversion.PlanetDiameter.Values.ToList().Max();
                var min  = _planetPropertiesConversion.PlanetDiameter.Values.ToList().Min();
                var planetMax = _planetPropertiesConversion.PlanetDiameter.FirstOrDefault(x => x.Value == max).Key;
                var planetMin = _planetPropertiesConversion.PlanetDiameter.FirstOrDefault(x => x.Value == min).Key;

                Console.WriteLine($"Max {userInput} is {max} (planet: {planetMax})");
                Console.WriteLine($"Min {userInput} is {min} (planet: {planetMin})");
            }
            else if(userInput == "surface_water")
            {
                var max  = _planetPropertiesConversion.PlanetSurface.Values.ToList().Max();
                var min  = _planetPropertiesConversion.PlanetSurface.Values.ToList().Min();
                var planetMax = _planetPropertiesConversion.PlanetSurface.FirstOrDefault(x => x.Value == max).Key;
                var planetMin = _planetPropertiesConversion.PlanetSurface.FirstOrDefault(x => x.Value == min).Key;

                Console.WriteLine($"Max {userInput} is {max} (planet: {planetMax})");
                Console.WriteLine($"Min {userInput} is {min} (planet: {planetMin})");
            }
            else if(userInput == "population")
            {
                var max  = _planetPropertiesConversion.PlanetPopulation.Values.ToList().Max();
                var min  = _planetPropertiesConversion.PlanetPopulation.Values.ToList().Min();
                var planetMax = _planetPropertiesConversion.PlanetPopulation.FirstOrDefault(x => x.Value == max).Key;
                var planetMin = _planetPropertiesConversion.PlanetPopulation.FirstOrDefault(x => x.Value == min).Key;

                Console.WriteLine($"Max {userInput} is {max} (planet: {planetMax})");
                Console.WriteLine($"Min {userInput} is {min} (planet: {planetMin})");                    
            }
            else 
            {
                Console.WriteLine("Invalid input");
            }
        }
    }
}