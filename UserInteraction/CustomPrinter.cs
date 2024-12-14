using ApiReader.DataModels;

namespace ApiReader.UserInteraction
{
    public class CustomPrinter
    {
        private readonly IEnumerable<string> _properties = new List<string> () {"diameter", "surface_water", "population"};
        private readonly Planets _planets; 

        public void Print(Planets planets)
        {
            Console.WriteLine($"Name Diameter Surface Population");
            foreach(var planet in planets.results)
            {
                Console.WriteLine($"{planet.name} {planet.diameter} {planet.surface_water} {planet.population}");
            }
            Console.WriteLine("");
            Console.WriteLine("The statistics of which property would you like to see?");
            foreach(var property in _properties)
            {
                Console.WriteLine(property);
            }
        }
    }
}