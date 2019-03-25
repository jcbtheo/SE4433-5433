using Microsoft.AspNetCore.Mvc;
using KWICWeb.SharedDataKWIC;
using System.Diagnostics;
using System;

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
                //FullShareData f = new FullShareData();
                //f.Input(inputData);
                //f.CircularShift();
                //f.Alpabetize();
                //f.Output();

                //string test = string.Join('\n', f.output);

                // time the action for comparison against pipes and filters
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Input input = new Input();
                LineStorage store = new LineStorage();
                input.Store(store, inputData);
                CircularShifter cs = new CircularShifter(store);
                AlphabeticShifter al = new AlphabeticShifter(cs);
                al.Alph();
                Output output = new Output(al);

                sw.Stop();
                string timeElapsed = sw.Elapsed.ToString();
                return Ok(new { data = output.GetOuputAsString(), time = timeElapsed });
            }
            catch (Exception ex)
            {
                return Ok(new { data = ex.Message + ex.StackTrace });
                //return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}