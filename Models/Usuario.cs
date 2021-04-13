using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aula.Models
{
    public class Usuario
    {
       

        public Usuario()
        {
            Rol = new Rol();
        }

        public Usuario(DAL.Entities.Usuario usuario)
        {
            Rol = usuario.Rol != null ? new Rol(usuario.Rol) : new Rol();
            NombreCompleto = usuario.NombreCompleto;
            Domicilio = usuario.Domicilio;
            Email = usuario.Email;
            FechaNacimiento = usuario.FechaNacimiento.ToString("yyyy-MM-dd ");
            Id = usuario.Id;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "Debes ingresar una fecha de nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Debes ingresar una email")]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debes ingresar una domicilio")]
        public string Domicilio { get; set; }

     
        public Rol Rol { get; set; }
    }
}