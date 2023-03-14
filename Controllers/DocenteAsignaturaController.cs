using DirectorAPI.Data;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocenteAsignaturaController : ControllerBase
    {
        Repository<DocenteAsignatura> repositoriAsginatura;

        public DocenteAsignaturaController(Sistem21PrimariaContext context)
        {
            repositoriAsginatura = new(context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var asignaturas = repositoriAsginatura.Get().OrderBy(x => x.Id).ToList();

            if (asignaturas.Count == 0)
            {
                return NotFound();
            }
            return Ok(asignaturas);
        }
        [HttpPost]
        public IActionResult Post(DocenteAsignatura docenteg)
        {
            DocenteAsignatura docent = new DocenteAsignatura()
            {
                IdDocente = docenteg.IdDocente,
                IdAsignatura = docenteg.IdAsignatura
            };
            repositoriAsginatura.Insert(docent);

            return Ok();

        }

    }
}
