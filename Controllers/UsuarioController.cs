﻿using DirectorAPI.Data;
using DirectorAPI.DTO;
using DirectorAPI.Models;
using DirectorAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DirectorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        Repository<Usuario> repositories;
        Repository<Director> directorrepository;
        public UsuarioController(Sistem21PrimariaContext conetxt)
        {
            repositories = new(conetxt);
            directorrepository = new(conetxt);
        }
        [HttpGet]
        public IActionResult Get()
        {
        var usuarios = repositories.Get().OrderBy(x => x.Id).ToList();
            if(usuarios.Count== 0)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usuario = repositories.Get().Where(x => x.Id == id).FirstOrDefault();
            if(usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        
        [HttpPost("login")]
        public IActionResult PostLogin(UsuarioDTO usuario)
        {
            var usuarioDirector = repositories.Get().FirstOrDefault(x => x.Usuario1 == usuario.Usuario1 && x.Contraseña == usuario.Contraseña);
            if (usuarioDirector == null || usuarioDirector.Rol != 1)
            {
                return NotFound("Usuario o Contraseña Incorrectos");
            }
            //var d = directorrepository.Get().FirstOrDefault(x => x.Idusuario == usuarioDirector.Id);
            var d = directorrepository.Get(usuarioDirector.Id);
            Director director;
            director = new Director
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Telefono = d.Telefono,
                Direccion = d.Direccion,
                Idusuario = d.Idusuario
            };
            return Ok(d);
        }
        [HttpPost]
        public IActionResult Post(UsuarioDTO usuario)
        {
            if (usuario == null)
            {
                return NotFound();
            }
            if (Validar(usuario, out List<string> errors))
            {
                Usuario usuario1 = new Usuario()
                {
                    Usuario1 = usuario.Usuario1,
                    Rol = 2,
                    Contraseña = usuario.Contraseña

                };
                repositories.Insert(usuario1);
                return Ok(usuario1.Id);
            }
            return BadRequest(errors);
        }
        [HttpPut]
        public IActionResult Put(UsuarioDTO usuario)
        {
            var usu = repositories.Get(usuario.Id);

            if (usu == null)
            {
                NotFound();
            }
            if (Validar(usuario, out List<string> errors))
            {
                usu.Usuario1 = usuario.Usuario1;
                usu.Rol = usuario.Rol;
                usu.Contraseña = usuario.Contraseña;

                repositories.Update(usu);
                return Ok();
            }
            return BadRequest(errors);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = repositories.Get(id);
            
            if (usuario != null)
            {
                repositories.Delete(usuario);
                return Ok();
            }
            else
                return NotFound("El usuario no existe o ya ha sido eliminado");

        }

        private bool Validar(UsuarioDTO usuario, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario.Usuario1))
            {
                errors.Add("El nombre de usuario no puede ir vacio");
            }

            if (repositories.Get().Any(x => x.Usuario1 == usuario.Usuario1 && x.Id != usuario.Id))
            {
                errors.Add("Ya existe un usuario con el mismo nombre, ingresa uno diferente");
            }
            if (usuario.Rol == 0)
            {
                errors.Add("Asigne un rol al usuario");
            }
            if (repositories.Get().Any(x => x.Rol == 1 && usuario.Rol == 1))
            {
                errors.Add("Solo puede haber un usuairo Director. Ingresa otro rol");
            }
            if (string.IsNullOrWhiteSpace(usuario.Contraseña))
            {
                errors.Add("La contraseña no puede ir vacia. Ingresa una");
            }
            
            return errors.Count == 0;
        }
    }
}
