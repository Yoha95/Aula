using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Aula.Models;

namespace Aula.DAL.Entities
{
    public class Usuario
    {
       public Usuario ()
        {

        }
        public Usuario (Models.Usuario usuario)
        {
            Id = usuario.Id;
            NombreCompleto = usuario.NombreCompleto;
            FechaNacimiento = System.DateTime.Parse(usuario.FechaNacimiento, CultureInfo.CreateSpecificCulture("en-US"));
            Email = usuario.Email;
            Domicilio = usuario.Domicilio;
            RolId = usuario.Rol != null ? usuario.Rol.Id : (int?)null;


        }

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Domicilio { get; set; }

        public virtual Rol Rol { get; set; }
        public int? RolId { get; set; }

    }
}