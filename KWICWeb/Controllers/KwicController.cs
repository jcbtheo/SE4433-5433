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
                // Create the pipeline and add filters to it. The SinkFilter must be defined as a variable because we need to be able to pull
                // the accumulated data from it for the webcall return. 
                Pipeline pl = new Pipeline();
                SinkFilter sink = new SinkFilter();
                pl.Register(new SourceFilter(inputData)).Register(new CircularShifter()).Register(new Alphabetizer()).Register(sink);
                pl.Run();

                return Ok(new {data = string.Join('\n', sink.output)});
            }
            catch
            {
                return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}