using Microsoft.AspNetCore.Mvc;
using KWICWeb.SharedDataKWIC;
using System;
using DataStore;

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
                Input input = new Input();
                LineStorage store = new LineStorage();
                input.Store(store, inputData);
                CircularShifter cs = new CircularShifter(store);
                cs.SetFilterWords(@"C:\Users\Jacob\Documents\School\Software Architecture and Design\SE4433-5433\KWICWeb\NoiseWords.txt");
                cs.Shift();
                AlphabeticShifter al = new AlphabeticShifter(cs);
                al.Alph();
                DataLayer.StoreLines(new Output().GetOuputAsString(al));
                return Ok(new { data = new Output().GetOuputAsString(al)});
            }
            catch (Exception ex)
            {
                return Ok(new { data = ex.Message + ex.StackTrace });
                //return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}