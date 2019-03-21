using Microsoft.AspNetCore.Mvc;
using KWICWeb.SharedDataKWIC;
using System.Diagnostics;

namespace KWICWeb.Controllers
{
    [Route("api/[controller]")]
    public class KwicOOController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody]string inputData)
        {
            try
            {
                // time the action for comparison against pipes and filters
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Input input = new Input();
                LineStorage store = new LineStorage();
                input.Store(store, inputData);
                CircularShifter cs = new CircularShifter(store);
                AlphabeticShifter al = new AlphabeticShifter(cs);
                al.Alph();
                //Output output = new Output(al);

                sw.Stop();
                string timeElapsed = sw.Elapsed.ToString();
                return Ok(new { data = string.Join('\n', al.sortedList), time = timeElapsed });
            }
            catch
            {
                return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}