﻿using System;
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
        // test change for commit 
    }
}