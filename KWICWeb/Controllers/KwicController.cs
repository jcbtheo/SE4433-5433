using Microsoft.AspNetCore.Mvc;
using KWICWeb.PipesAndFilters;
using System.Diagnostics;

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
                // time the action for comparison against OO
                Stopwatch sw = new Stopwatch();
                sw.Start();

                // Create the pipeline and add filters to it. The SinkFilter must be defined as a variable because we need to be able to pull
                // the accumulated data from it for the webcall return. 
                Pipeline pl = new Pipeline();
                SinkFilter sink = new SinkFilter();
                pl.Register(new PipesAndFilters.SourceFilter(inputData)).Register(new CircularShifter()).Register(new Alphabetizer()).Register(sink);
                pl.Run();

                sw.Stop();
                string timeElapsed = sw.Elapsed.ToString();

                return Ok(new {data = string.Join('\n', sink.output), time = timeElapsed});
            }
            catch
            {
                return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}