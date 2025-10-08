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
        [HttpGet]
        public async Task<IActionResult> GetNordpoolPrices()
        {
            // Saadame asünkroonselt GET-päringu välisele Nord Pooli API-le
            var response = await _httpClient.GetAsync("https://dashboard.elering.ee/api/nps/price");
            // Saame vastuse sisu stringina
            var responseBody = await response.Content.ReadAsStringAsync();
            // Tagastame saadud andmed JSON-vormingus
            return Content(responseBody, "application/json");
        }

        [HttpGet("{country}/{start}/{end}")]
        public async Task<IActionResult> GetNordPoolPrices(
            string country,
            string start,
            string end)
            {
            var response = await _httpClient.GetAsync(
                $"https://dashboard.elering.ee/api/nps/price?start={start}&end={end}");
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

            var jsonDoc = JsonDocument.Parse(responseBody);
            var dataProperty = jsonDoc.RootElement.GetProperty("data");

            string prices;

            switch (country)
            {
                case "ee":
                    prices = dataProperty.GetProperty("ee").ToString();
                    Console.WriteLine(responseBody);

                    return Content(prices, "application/json");

                case "lv":
                    prices = dataProperty.GetProperty("lv").ToString();
                    return Content(prices, "application/json");

                case "lt":
                    prices = dataProperty.GetProperty("lt").ToString();
                    return Content(prices, "application/json");

                case "fi":
                    prices = dataProperty.GetProperty("fi").ToString();
                    return Content(prices, "application/json");

                default:
                    return BadRequest("Invalid country code.");
            }
        }
    }
}
