using DirectorAPI.Data;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        Repository<Grupo> repositorigrupo;

        public GrupoController(Sistem21PrimariaContext context)
        {
            repositorigrupo = new(context);
        }
        [HttpGet]
        public IActionResult Get() 
        {
            var grupo = repositorigrupo.Get().OrderBy(x => x.Id).ToList();

            if (grupo.Count == 0)
            {
                return NotFound();
            }
            return Ok(grupo);
        }
    }
}
