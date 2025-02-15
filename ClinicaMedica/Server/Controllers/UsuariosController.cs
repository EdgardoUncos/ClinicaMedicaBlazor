using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaMedica.Server.Data;
using ClinicaMedica.Server.Entities;
using ClinicaMedica.Shared.DTOs.Create;
using ClinicaMedica.Server.Utilities;
using ClinicaMedica.Shared.Models;

namespace ClinicaMedica.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _confi;
       

        public UsuariosController(ApplicationDbContext context, IConfiguration confi)
        {
            _context = context;
            _confi = confi;
        }

        [HttpGet("Datos")]
        public async Task<ActionResult<List<Usuarios>>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult<string>> CreateUser([FromBody] UsuarioDTO usuario)
        {
            
            // Crear el Passordhash y el Salt

            FuncionesToken.CreatePasswordHash(usuario.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Creamos un usuario para guardar
            Usuarios userCreate = new Usuarios
            {
                NombreUsuario = usuario.NombreUsuario,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Rol = usuario.Rol
            };

            // Guardamos
            _context.Usuarios.Add(userCreate);
            await _context.SaveChangesAsync();
            var respuesta = "Registrado con Exito";

            return Ok(respuesta);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUsuario logUser)
        {
            // Valida si existe el usuario y valida la contraseña
            var verify = await FuncionesToken.ValidarUsuario(logUser, _context);

            if (verify == null)
            {
                return BadRequest("Usuario o contraseña incorrectos");
            }

            var token = FuncionesToken.GenerarToken(verify, _confi);

            return Ok(token);

        }
    }
}
