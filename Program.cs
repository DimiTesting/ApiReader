using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

using ApiReader.Interfaces;
using ApiReader.WorkWithApi;
using ApiReader.DataModels;
using ApiReader.UserInteraction;

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
            PlanetPropertiesConversion planetPropertiesConversion = new PlanetPropertiesConversion();
            planetPropertiesConversion.ConvertToDict(planets);

            CustomPrinter customPrinter = new CustomPrinter();
            customPrinter.Print(planets);

            var userInput = Console.ReadLine();
            GiveStats giveStats = new GiveStats();
            giveStats.GetMinAndMax(userInput, planetPropertiesConversion);
        }
    }
}