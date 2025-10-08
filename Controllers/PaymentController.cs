using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace veeb_TARpv23.Controllers
{
    [Route("api/[controller]")]// Marsruutimise atribuut: aadress algab api/Nordpool
    [ApiController] // Näitab, et tegemist on API kontrolleriga
    public class PaymentController : ControllerBase
    {
        // HttpClient'i salvestamiseks mõeldud privaatne väli, mida kasutatakse HTTP-päringute täitmiseks
        private readonly HttpClient _httpClient;

        // Kontrollerikonstruktor, milles edastatakse HttpClient'i eksemplar
        public PaymentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Saadab andmed EveryPay-le makse loomiseks
        [HttpGet("{sum}")]
        public async Task<IActionResult> MakePayment(string sum)
        {
            // Moodustame objekti makseandmetega
            var paymentData = new
            {
                api_username = "e36eb40f5ec87fa2", // kasutaja API nimi
                account_name = "EUR3D1", // konto, millele makse laekub
                amount = sum,  // makse summa
                order_reference = Math.Ceiling(new Random().NextDouble() * 999999),  // tellimuse number
                nonce = $"a9b7f7e7as{DateTime.Now}{new Random().NextDouble() * 999999}", // unikaalne identifikaator (turvalisus)
                timestamp = DateTime.Now, // makse loomise aeg
                customer_url = "https://maksmine.web.app/makse" // ümbersuunamise aadress pärast maksmist
            };

            var json = JsonSerializer.Serialize(paymentData);
            // Päringu sisu ettevalmistamine
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // HttpClient'i loomine
            var client = new HttpClient();
            // Autoriseerimise pealkirja lisamine
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZTM2ZWI0MGY1ZWM4N2ZhMjo3YjkxYTNiOWUxYjc0NTI0YzJlOWZjMjgyZjhhYzhjZA==");
            // Saadame POST-päringu EveryPay sandbox API-le
            var response = await client.PostAsync("https://igw-demo.every-pay.com/api/v4/payments/oneoff", content);
            // Kontrollime, kas päring on edukalt täidetud
            if (response.IsSuccessStatusCode)
            {
                // Loeme vastust kui stringi
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseContent);
                // Võtame vastusest välja makselingi (payment_link)
                var paymentLink = jsonDoc.RootElement.GetProperty("payment_link");
                // Tagastame lingi kliendile
                return Ok(paymentLink);
            }
            else
            {
                return BadRequest("Payment failed.");
            }
        }
    }
}
