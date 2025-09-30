using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace veeb_TARpv23.Controllers
{
    [Route("api/[controller]")] // Marsruutimise atribuut: aadress algab api/Primitiivid
    [ApiController] // Näitab, et tegemist on API kontrolleriga
    public class PrimitiividController : ControllerBase
    {
        // GET: api/primitiivid/hello-world - Marsruut
        [HttpGet("hello-world")] 
        public string HelloWorld()
        {
            // Tagastab stringi „Hello world“ koos praeguse kuupäeva ja kellaajaga
            return "Hello world at " + DateTime.Now;
        }

        // GET: api/primitiivid/hello-variable/mari  - Marsruut
        [HttpGet("hello-variable/{nimi}")]
        public string HelloVariable(string nimi)
        {
            // Tagastab tervituse koos edastatud nimega
            return "Hello " + nimi;
        }

        // GET: api/primitiivid/add/5/6   - Marsruut
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            // Liidab kaks arvu ja tagastab tulemuse
            return nr1 + nr2;
        }

        // GET: api/primitiivid/multiply/5/6 - Marsruut
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            // Korrutab kaks arvu ja tagastab tulemuse
            return nr1 * nr2;
        }

        // GET: api/primitiivid/do-logs/5 - Marsruut
        [HttpGet("do-logs/{arv}")]
        public void DoLogs(int arv)
        {
            // Väljastab konsooli määratud arvu sõnumeid
            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("See on logi nr " + i);
            }
        }
    }
}
