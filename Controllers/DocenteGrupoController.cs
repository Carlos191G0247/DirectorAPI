using DirectorAPI.Data;
using DirectorAPI.DTO;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocenteGrupoController : ControllerBase
    {
        Repository<DocenteGrupo> repositoriDocentegrupo;

        public DocenteGrupoController(Sistem21PrimariaContext context)
        {
            repositoriDocentegrupo = new(context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var grupo = repositoriDocentegrupo.Get().OrderBy(x => x.Id).ToList();

            if (grupo.Count == 0)
            {
                return NotFound();
            }
            return Ok(grupo);
        }
        [HttpPost]
        public IActionResult Post(DocenteGrupo docenteg)
        {

            DocenteGrupo docent = new DocenteGrupo()
            {
                IdDocente = docenteg.IdDocente,
                IdGrupo = docenteg.IdGrupo,
                IdPeriodo = docenteg.IdPeriodo
                       
            };

                    repositoriDocentegrupo.Insert(docent);

                    return Ok();
              
        }
    }
}
