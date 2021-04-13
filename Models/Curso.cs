using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aula.Models
{
    public class Curso
    {

        public Curso()
        {
           
        }

        public Curso(DAL.Entities.Curso curso)
        {

            Titulo = curso.Titulo;
            CupoMaximo = curso.CupoMaximo;
            FechaInicio = curso.FechaInicio.ToString("yyyy-MM-dd ");
            Id = curso.Id;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Debes ingresar un titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debes ingresar un cupo maximo")]
        [Range(1, 100, ErrorMessage = "Ingresa un cupo valido")]
        public int CupoMaximo { get; set; }

        [Required(ErrorMessage = "Debes ingresar una fecha de Inicio")]
        public string FechaInicio { get; set; }

        
    }
}