using Microsoft.AspNetCore.Mvc;
using KWICWeb.KWICSearch;
using System;
using System.Collections.Generic;

namespace KWICWeb.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody]string inputData)
        {
            try
            {
                KwicSearch ks = new KwicSearch();
                List<string> test = ks.Search(inputData);
                return Ok(new { data = string.Join('\n', test)});
            }
            catch (Exception ex)
            {
                return Ok(new { data = ex.Message + ex.StackTrace });
            }
        }
    }
}