using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiReader.DataModels
{
    public record PlanetProperties(
        [property: JsonPropertyName("name")] string name,
        [property: JsonPropertyName("diameter")] string diameter,
        [property: JsonPropertyName("surface_water")] string surface_water,
        [property: JsonPropertyName("population")] string population
    );
    public record Planets(
        [property: JsonPropertyName("results")] IReadOnlyList<PlanetProperties> results
    );
}