using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            CustomPrinter customPrinter = new CustomPrinter();
            //customPrinter.Print(planets);
            customPrinter.PrintProperies();
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

        public class CustomPrinter
        {
            private readonly Planets _planets; 

            public void Print(Planets planets)
            {
                foreach(var planet in planets.results)
                {
                    Console.WriteLine($"{planet.name}");
                }
            }
        }
    }
}