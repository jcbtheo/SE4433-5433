using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
                inputData += "!!!";
                return Ok(new { data = inputData });
            }
            catch
            {
                return BadRequest("There was a problem analyzing your input. Please try again.");
            }
        }
    }
}