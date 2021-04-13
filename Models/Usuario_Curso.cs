using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula.Models
{
    public class Usuario_Curso
    {
        public Usuario_Curso()
        {
            Curso = new Curso();
            Usuario = new Usuario();

        }

        public Usuario_Curso(DAL.Entities.Usuario_Curso usuarioCurso)
        {
            Curso = usuarioCurso.Curso != null ? new Curso(usuarioCurso.Curso) : new Curso();
            Usuario = usuarioCurso.Usuario != null ? new Usuario(usuarioCurso.Usuario) : new Usuario ();
           
        }
        public int Id { get; set; }

        public Curso Curso { get; set; }

        public Usuario Usuario { get; set; }
    }
}