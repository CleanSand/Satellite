using Avalonia;
using Newtonsoft.Json;

namespace Satellite.Models;

public class Solar
{
 
    [JsonProperty("id")]
    public string id { get; set; }
    [JsonProperty("englishName")]
    public string englishName { get; set; }
    [JsonProperty("isPlanet")]
    public bool isPlanet { get; set; }
    [JsonProperty("bodyType")]
    public string bodyType { get; set; }
    [JsonProperty("perihelion")]
    public float perihelion { get; set; }
}