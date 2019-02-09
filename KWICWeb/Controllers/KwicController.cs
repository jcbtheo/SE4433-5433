using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KWICWeb.PipesAndFilters;

namespace KWICWeb.Controllers
{
    [Route("api/[controller]")]
    public class KwicController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody]string inputData)
        {
            try
            {
                Pipeline<string> pl = new Pipeline<string>();
                pl.Add(new SourceFilter(inputData)).Add(new CircularShifter()).Add(new Alphabetizer());
                pl.Run();

                //List<string> test = inputData.Split("\n").ToList();
                //CircularShifter shifter = new CircularShifter();
                //IEnumerable<string> test2 = shifter.Filter(test);
                //string final = string.Join('\n', test2.ToArray());
                return Ok(new { data = string.Join('\n', pl.output) });
            }
            catch
            {
                return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
        // test change for commit 
    }
}