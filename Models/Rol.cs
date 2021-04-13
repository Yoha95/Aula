using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aula.Models
{
    public class Rol
    {

        public Rol()
        {
            
        }

        public Rol(DAL.Entities.Rol rol)
        {
            Id = rol.Id;
            Nombre = rol.Nombre;
        }

        [Required(ErrorMessage = "Debes ingresar un rol")]
        public int Id { get; set; }

     
        public string Nombre { get; set; }
    }
}