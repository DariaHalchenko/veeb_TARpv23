using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace veeb_TARpv23.Controllers
{
    [Route("[controller]")]// Marsruutimise atribuut: aadress algab /SmartPost
    [ApiController] // Näitab, et tegemist on API kontrolleriga
    public class SmartPostController : ControllerBase
    {
        // HttpClient'i salvestamiseks mõeldud privaatne väli, mida kasutatakse HTTP-päringute täitmiseks
        private readonly HttpClient _httpClient;

        // Kontrollerikonstruktor, milles edastatakse HttpClient'i eksemplar
        public SmartPostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetSmartPost()
        {
            // Teostame asünkroonse GET-päringu välisele API-le
            var request = new HttpRequestMessage(HttpMethod.Get, "https://my.smartpost.ee/api/ext/v1/places?country=EE");
            // Mõned API-d nõuavad User-Agenti päist
            request.Headers.Add("User-Agent", "SmartPostClient/1.0");
            // Saadame päringu
            var response = await _httpClient.SendAsync(request);
            // Saame vastuse stringina (XML-vormingus)
            var xmlString = await response.Content.ReadAsStringAsync();
            // Laadime XML-stringi XDocumenti, et struktuuri oleks mugavam analüüsida.
            var xdoc = XDocument.Parse(xmlString);
            // Muundame XML C# objektide loendiks
            var places = xdoc.Descendants("item").Select(x => new SmartPostPlace
            {
                PlaceId = (string?)x.Element("place_id"),
                Name = (string?)x.Element("name"),
                Address = (string?)x.Element("address"),
                City = (string?)x.Element("city"),
                PostalCode = (string?)x.Element("postalcode"),
                Type = (string?)x.Element("type"),
                Lat = (double?)x.Element("lat"),
                Lng = (double?)x.Element("lng"),
                Availability = (string?)x.Element("availability")
            }).ToList();

            // tagastame puhta JSON-i ilma tsükliteta
            return Ok(places);
        }
    }

    //Abiklass ühe SmartPost-punkti esitamiseks
    public class SmartPostPlace
    {
        public string? PlaceId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Type { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string? Availability { get; set; }
    }
}
