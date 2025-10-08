using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]// Marsruutimise atribuut: aadress algab api/Nordpool
    [ApiController] // Näitab, et tegemist on API kontrolleriga
    public class NordpoolController : ControllerBase
    {
        // HttpClient'i salvestamiseks mõeldud privaatne väli, mida kasutatakse HTTP-päringute täitmiseks
        private readonly HttpClient _httpClient;

        // Kontrollerikonstruktor, milles edastatakse HttpClient'i eksemplar
        public NordpoolController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet("{country}/{start}/{end}")]
        public async Task<IActionResult> GetNordPoolPrices(
            string country, // riigi kood (ee, lv, lt, fi)
            string start, // perioodi alguskuupäev
            string end)  // perioodi lõppkuupäev
        {
            // Teostame HTTP-päringu Nord Pooli API-le
            var response = await _httpClient.GetAsync(
                $"https://dashboard.elering.ee/api/nps/price?start={start}&end={end}");
            // Loeme vastuse rea kujul
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

            var jsonDoc = JsonDocument.Parse(responseBody);
            // Võtame omaduse „data“ juur-JSONist välja
            var dataProperty = jsonDoc.RootElement.GetProperty("data");

            string prices;

            // Sõltuvalt riigist tagastame vastava JSON-osa
            switch (country)
            {
                // Saame andmed Eesti kohta
                case "ee":
                    prices = dataProperty.GetProperty("ee").ToString();
                    Console.WriteLine(responseBody);

                    return Content(prices, "application/json");
                // Saame andmed Läti kohta
                case "lv":
                    prices = dataProperty.GetProperty("lv").ToString();
                    return Content(prices, "application/json");
                // Saame andmed Leedu kohta
                case "lt":
                    prices = dataProperty.GetProperty("lt").ToString();
                    return Content(prices, "application/json");
                // Saame andmed Soome kohta
                case "fi":
                    prices = dataProperty.GetProperty("fi").ToString();
                    return Content(prices, "application/json");
                // Kui edastatud riigi kood on vale, tagastame vea 400.
                default:
                    return BadRequest("Invalid country code.");
            }
        }
    }
}
