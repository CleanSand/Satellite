using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Satellite.Api;

public class Api
{
    private static async Task Main()
    {
        using var client = new HttpClient();

        client.BaseAddress = new Uri("https://api.le-systeme-solaire.net/rest/");
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var url = "bodies";
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var resp = await response.Content.ReadAsStringAsync();
        dynamic des = JObject.Parse(resp);
        foreach (var body in des.bodies)
        {
            Console.WriteLine($"{body.id}{body.englishName} {body.isPlanet} {body.bodytype} {body.perihelion}");
        }




    }
}