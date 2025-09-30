using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using veeb_TARpv23.models;

namespace veeb_TARpv23.Controllers
{
    [Route("api/[controller]")] // Marsruutimise atribuut: aadress algab api/Toode
    [ApiController] // Näitab, et tegemist on API kontrolleriga
    public class ToodeController : ControllerBase
    {
        // Toote staatiline objekt (ID, name, price, isActive)
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: api/toode - Marsruut
        [HttpGet]
        public Toode GetToode()
        {
            // Tagastab praeguse toote objekti
            return _toode;
        }

        // GET: api/toode/suurenda-hinda - Marsruut
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            // Suurendab toote hinda 1 võrra
            _toode.Price = _toode.Price + 1;
            // Tagastab uuendatud toote
            return _toode;
        }
    }
}
