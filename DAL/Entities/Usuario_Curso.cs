using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aula.Models;

namespace Aula.DAL.Entities
{
    public class Usuario_Curso
    {

        public Usuario_Curso()
        {
                

        }
        public Usuario_Curso (Models.Usuario_Curso usuarioCurso)
        {
            Id = usuarioCurso.Id;
            CursoId = usuarioCurso.Curso != null ? usuarioCurso.Curso.Id : (int?)null;
            UsuarioId = usuarioCurso.Usuario != null ? usuarioCurso.Usuario.Id : (int?)null;
        }

        public int Id { get; set; }
        public int? CursoId { get; set; }

        public virtual Curso Curso { get; set; }
        public int? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

       
    }
}