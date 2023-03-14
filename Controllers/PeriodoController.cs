using DirectorAPI.Data;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoController : ControllerBase
    {
        Repository<Periodo> repositoriPeriodo;

        public PeriodoController(Sistem21PrimariaContext context)
        {
            repositoriPeriodo = new(context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var periodo = repositoriPeriodo.Get().OrderBy(x => x.Id).ToList();

            if (periodo.Count == 0)
            {
                return NotFound();
            }
            return Ok(periodo);
        }
    }
}
