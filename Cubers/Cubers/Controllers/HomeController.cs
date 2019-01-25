using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cubers.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ICuberService CuberService;

        public HomeController(ICuberService cuberService)
        {
            CuberService = cuberService;
        }

        [HttpPost]
        [Route("cuber")]
        public IActionResult PostCuber(Cuber c)
        {
            if (c.Id < 0)
            {
                CuberService.AddCuber(c);
            }
            else
            {
                CuberService.SaveCuber(c);
            }
            return Created("/api/cuber/" + c.Id, c);
        }

        [HttpPost]
        [Route("cuber/{id}")]
        public IActionResult AddCuberSolve(PostedTime solve)
        {
            if (solve.Time > 0)
            {
                CuberService.AddSolve(solve);
                return Ok();
            }
            else
                return Content("Invalid request.  The solve cannot have a negative time");
        }

        [HttpDelete]
        [Route("cuber/{id}")]
        public IActionResult DeleteCuberById(int id)
        {
            CuberService.DeleteCuber(id);
            return NoContent();
        }

        [HttpGet]
        [Route("cuber/{id}")]
        public virtual IActionResult FindCuberById(int id)
        {
            var cuber = CuberService.GetCuber(id);
            return Ok(cuber);
        }

        [HttpGet]
        [Route("cuber")]
        public virtual IActionResult GetCuberSummary()
        {
            var summary = CuberService.GetCuberSummary();
            return Ok(summary);
        }



        
    }
}
