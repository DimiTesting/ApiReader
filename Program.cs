using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace ApiReader
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("New project to Read from public API and print stats per user request");
            string baseAddress = "https://swapi.dev/api/";
            string requestUri = "planets";

            IApiDataReader apiDataReader = new Api();
            string jsonResponse = await apiDataReader.Read(baseAddress, requestUri);
            Planets planets = JsonSerializer.Deserialize<Planets>(jsonResponse);
            ConvertPlanetProperties convertPlanetProperties = new ConvertPlanetProperties();
            convertPlanetProperties.FillInProperties(planets);
            CustomPrinter customPrinter = new CustomPrinter();
            customPrinter.Print(planets);

            var userInput = Console.ReadLine();
            GiveStats giveStats = new GiveStats();
            giveStats.GetMinAndMax(userInput, convertPlanetProperties);

        }

        public interface IApiDataReader
        {
            Task<string> Read(string baseAddress, string requestUri);
        }

        public class Api : IApiDataReader
        {
            public async Task<string> Read(string baseAddress, string requestUri)
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(baseAddress);
                HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); 
            }
        }
        public record PlanetProperties(
            [property: JsonPropertyName("name")] string name,
            [property: JsonPropertyName("diameter")] string diameter,
            [property: JsonPropertyName("surface_water")] string surface_water,
            [property: JsonPropertyName("population")] string population
        );

        public record Planets(
            [property: JsonPropertyName("results")] IReadOnlyList<PlanetProperties> results
        );

        //CustomPrinter class is very limited and terribly formating data, will be fixed later
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

        //GiveStats class is very limited and written as a function, will be fixed later
        public class GiveStats
        {
            private readonly ConvertPlanetProperties _convertPlanetProperties;
            public void GetMinAndMax(string userInput, ConvertPlanetProperties _convertPlanetProperties)
            {
                if(userInput == "diameter")
                {
                    var convertedToList = _convertPlanetProperties.PlanetDiameter.Values.ToList();
                    var max  = convertedToList.Max();
                    var min  = convertedToList.Min();
                    var planetMax = _convertPlanetProperties.PlanetDiameter.FirstOrDefault(x => x.Value == max).Key;
                    var planetMin = _convertPlanetProperties.PlanetDiameter.FirstOrDefault(x => x.Value == min).Key;

                    Console.WriteLine($"Max {userInput} is {max} (planet: {planetMax})");
                    Console.WriteLine($"Min {userInput} is {min} (planet: {planetMin})");
                }
                else if(userInput == "surface_water")
                {
                    var convertedToList = _convertPlanetProperties.PlanetSurface.Values.ToList();
                    var max  = convertedToList.Max();
                    var min  = convertedToList.Min();
                    var planetMax = _convertPlanetProperties.PlanetSurface.FirstOrDefault(x => x.Value == max).Key;
                    var planetMin = _convertPlanetProperties.PlanetSurface.FirstOrDefault(x => x.Value == min).Key;

                    Console.WriteLine($"Max {userInput} is {max} (planet: {planetMax})");
                    Console.WriteLine($"Min {userInput} is {min} (planet: {planetMin})");
                }
                else if(userInput == "population")
                {
                    var convertedToList = _convertPlanetProperties.PlanetPopulation.Values.ToList();
                    var max  = convertedToList.Max();
                    var min  = convertedToList.Min();
                    var planetMax = _convertPlanetProperties.PlanetPopulation.FirstOrDefault(x => x.Value == max).Key;
                    var planetMin = _convertPlanetProperties.PlanetPopulation.FirstOrDefault(x => x.Value == min).Key;

                    Console.WriteLine($"Max {userInput} is {max} (planet: {planetMax})");
                    Console.WriteLine($"Min {userInput} is {min} (planet: {planetMin})");                    
                }
                else 
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        public class ConvertPlanetProperties
        {
            private readonly Planets _planets; 
            public Dictionary<string, int> PlanetDiameter = new Dictionary<string, int>();
            public Dictionary<string, int> PlanetSurface = new Dictionary<string, int>();
            public Dictionary<string, long> PlanetPopulation = new Dictionary<string, long>();

            public void FillInProperties(Planets _planets)
            {
                int x = 0;
                long z = 0;

                foreach(var planet in _planets.results)
                {
                    if(!PlanetDiameter.ContainsKey(planet.name))
                    {
                        if(Int32.TryParse(planet.diameter, out x))
                        {
                            PlanetDiameter[planet.name] = x; 
                        }
                        
                    }
                    if(!PlanetSurface.ContainsKey(planet.name))
                    {
                        if(Int32.TryParse(planet.surface_water, out x))
                        {
                            PlanetSurface[planet.name] = x; 
                        }
                        
                    }
                    if(!PlanetPopulation.ContainsKey(planet.name))
                    {
                        if(Int64.TryParse(planet.population, out z))
                        {
                            PlanetPopulation[planet.name] = z; 
                        }
                        
                    }
                }
            }
        }
    }
}