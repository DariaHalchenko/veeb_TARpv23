using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace veeb_TARpv23.Controllers
{
    [Route("[controller]")]// Marsruutimise atribuut: aadress algab /ParcelMachine
    [ApiController] // Näitab, et tegemist on API kontrolleriga
    public class ParcelMachineController : ControllerBase
    {
        // HttpClient'i salvestamiseks mõeldud privaatne väli, mida kasutatakse HTTP-päringute täitmiseks
        private readonly HttpClient _httpClient;

        // Kontrollerikonstruktor, milles edastatakse HttpClient'i eksemplar
        public ParcelMachineController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetParcelMachines()
        {
            // Teostame asünkroonse GET-päringu välisele API-le
            var response = await _httpClient.GetAsync("https://www.omniva.ee/locations.json");
            // Saame vastuse sisu stringina
            var responseBody = await response.Content.ReadAsStringAsync();
            // Tagastame saadud andmed JSON-vormingus
            return Content(responseBody, "application/json");
        }
    }
}

