using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula.DAL.Entities
{
    public class Rol
    {

        public Rol ()
        {

        }

        public Rol (Models.Rol rol)
        {
            Id = rol.Id;
            Nombre = rol.Nombre;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
      
    }
}