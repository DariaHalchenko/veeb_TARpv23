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

        // Ülesanne 1
        [HttpGet("random-number/{nr1}/{nr2}")]
        public int RandomNumber(int nr1, int nr2)
        {
            if (nr1 > nr2)
            {
                int temp = nr1;
                nr1 = nr2;
                nr2 = temp;
            }
            // Loome uue objekti Random juhusliku arvu genereerimiseks
            Random random = new Random();
            // Genereerime juhusliku arvu vahemikus (nr1, nr2)
            return random.Next(nr1, nr2);
        }

        // Ülesanne 2
        [HttpGet("age/{birthdayYears}")]
        public string GetAge(int birthdayYears)
        {
            // Saame praeguse kuupäeva ja kellaaja
            DateTime currentdate = DateTime.Now;
            // Arvutame ligikaudse vanuse
            int age = currentdate.Year - birthdayYears;
            // Loome kuupäeva objekti käesoleva aasta sünnipäevaks
            DateTime birthdayThisYear = new DateTime(currentdate.Year, 1, 1);
            // Kui praegune kuupäev on varasem kui sünnipäev sel aastal, vähendame vanust 1 aasta võrra.
            if (currentdate < birthdayThisYear)
            {
                age--;
            }
            // Tagastame rea vanusega
            return $"Oled {age} aastat vana";
        }
    }
}
