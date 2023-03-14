using DirectorAPI.Data;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        Repository<Asignatura> repositoriAsignatura;

        public AsignaturaController(Sistem21PrimariaContext context)
        {
            repositoriAsignatura = new(context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var asignatura = repositoriAsignatura.Get().OrderBy(x => x.Id).ToList();

            if (asignatura.Count == 0)
            {
                return NotFound();
            }
            return Ok(asignatura);
        }
    }
}
