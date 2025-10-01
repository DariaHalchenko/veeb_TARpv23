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

        // Ülesanne 1
        // GET: api/toode/aktiivsust - Marsruut
        [HttpGet("aktiivsust")]
        // lülitab aktiivsusseisundit 
        public Toode Aktiivsus()
        {
            // Inverteerime IsActive objekti omaduste hetkeseisu _toode 
            // Kui oli true - saab false, kui oli false - saab true
            _toode.IsActive = !_toode.IsActive;
            return _toode;
        }

        // Ülesanne 2
        // GET: toode/nime-muutmine/{uusNimi}
        [HttpGet("nime-muutmine/{uusNimi}")]
        // muudab toote nime
        public Toode NimeMuutmine(string uusNimi)
        {
            // Omandame uue nime 
            _toode.Name = uusNimi;
            return _toode;
        }

        // Ülesanne 3
        // GET: toode/muuda-hinda/{jarjekorranumber}
        [HttpGet("muuda-hinda/{jarjekorranumber}")]
        // toote hinnamuutused järjekorranumbri alusel
        public Toode MuudaHind(int jarjekorranumber)
        {
            // Kontrolli, et jarjekorranumber oleks positiivne
            if (jarjekorranumber <= 0)
            {
                throw new ArgumentException("Järjekorra number peab ületama nulli");
            }
            // Korrutame toote hetkehinna jarjekorranumbriga
            _toode.Price *= jarjekorranumber;
            return _toode;
        }
    }
}
