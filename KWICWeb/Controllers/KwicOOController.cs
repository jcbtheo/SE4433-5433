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
                // time the action for comparison against pipes and filters
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Input input = new Input();
                LineStorage store = new LineStorage();
                input.Store(store, inputData);
                CircularShifter cs = new CircularShifter(store);
                cs.SetFilterWords(@"C:\Users\Jacob\Documents\School\Software Architecture and Design\SE4433-5433\KWICWeb\NoiseWords.txt");
                cs.Shift();
                AlphabeticShifter al = new AlphabeticShifter(cs);
                al.Alph();

                sw.Stop();
                string timeElapsed = sw.Elapsed.ToString();
                return Ok(new { data = new Output().GetOuputAsString(al), time = timeElapsed });
            }
            catch (Exception ex)
            {
                return Ok(new { data = ex.Message + ex.StackTrace });
                //return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}