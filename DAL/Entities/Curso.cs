using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Aula.DAL.Entities
{
    public class Curso
    {
        public Curso ()
        {

        }
        public Curso (Models.Curso curso)
        {
            Id = curso.Id;
            Titulo = curso.Titulo;
            CupoMaximo = curso.CupoMaximo;
            FechaInicio = System.DateTime.Parse(curso.FechaInicio, CultureInfo.CreateSpecificCulture("en-US"));
        }


        public int Id { get; set; }

        public string Titulo { get; set; }

        public int CupoMaximo { get; set; }

        public System.DateTime FechaInicio { get; set; }

       
    }
}