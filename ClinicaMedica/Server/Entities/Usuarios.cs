using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string Rol { get; set; } = null!;

    public string Token { get; set; } = null!;
}
