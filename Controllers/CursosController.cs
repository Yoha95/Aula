using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aula.DAL;

namespace Aula.Controllers
{
    public class CursosController : Controller
    {

        private Contexto db = new Contexto();
        // GET: Cursos
        public ActionResult Index(string Mensaje)
        {
            if (Mensaje != null)
            {
                ViewBag.Mensaje = Mensaje;
            }

           

            //recupero el listado de usuario desde la bd
            List<DAL.Entities.Curso> cursos = db.Cursos.ToList();


            //mapeo el listado desde la bd a un listado para la vista 

            List<Models.Curso> listaCursos = cursos.Select(c => new Models.Curso(c)).ToList();
            ViewBag.Encabezado = "Lista de Cursos";

            

            return View(listaCursos);
        }

        public ActionResult Inscriptos ( string Mensaje, int id = 0)
        {
            if (Mensaje != null)
            {
                ViewBag.Mensaje = Mensaje;
            }

            DAL.Entities.Curso curso = db.Cursos.Where(p => p.Id.Equals(id)).FirstOrDefault();
            //Si no la encontramos, devolvemos un error 404
            if (curso == null) { return HttpNotFound(); }

            List<DAL.Entities.Usuario_Curso> inscriptos = db.Usuario_Cursos.Where(u => u.CursoId == curso.Id).ToList();

            List<Models.Usuario_Curso> listaInscriptos = inscriptos.Select(x => new Models.Usuario_Curso(x)).ToList();

            ViewBag.Encabezado = "Inscriptos al curso: " + curso.Titulo;

            

            ViewBag.IDcurso = curso.Id;

            return View(listaInscriptos);
        }

        public ActionResult NuevaInscripcion (int id)
        {
            DAL.Entities.Curso curso = db.Cursos.Where(p => p.Id.Equals(id)).FirstOrDefault();

            if (db.Rols.Any())
            {

                ViewBag.Roles = db.Rols.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            }

            ViewBag.Encabezado = "Inscripcion al curso:" + curso.Titulo ;

            ViewBag.IDcurso = id;



            return View();
        }

        [HttpPost]
        public ActionResult NuevaInscripcion(Models.Usuario usuario, int idCurso)
        {
            if (ModelState.IsValid)
            {

                DAL.Entities.Usuario nuevoUsuario = new DAL.Entities.Usuario(usuario);

                db.Usuarios.Add(nuevoUsuario);

                DAL.Entities.Usuario_Curso usuarioCurso = new DAL.Entities.Usuario_Curso();

                usuarioCurso.CursoId = idCurso;
                usuarioCurso.UsuarioId = nuevoUsuario.Id;

                db.Usuario_Cursos.Add(usuarioCurso);

                db.SaveChanges();

                return RedirectToAction("Inscriptos", new {id = idCurso });
            }
            else
            {
                 ViewBag.Roles = db.Rols.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
                ViewBag.MensajeError = String.Join(", ", ModelState.Values.Where(v => v.Errors.Any()).Select(e => e.Errors[0].ErrorMessage).ToArray());
                ViewBag.IDcurso = idCurso;
                return View(usuario);
            }

            
        }

        public ActionResult Manage(int id = 0)
        {

            Models.Curso curso = new Models.Curso();

          
            //Si tiene el id que recibe tiene valor (es una edición) 
            if (id != 0)
            {

                curso = new Models.Curso(db.Cursos.Where(s => s.Id.Equals(id)).FirstOrDefault());

                ViewBag.Encabezado = "Editar " + curso.Titulo;
            }
            else
            {
                ViewBag.Encabezado = "Crear curso";
            }

            ViewBag.MensajeError = string.Empty;

            return View(curso);
        }

        [HttpPost]

        public ActionResult Manage(Models.Curso curso)
        {

            if (ModelState.IsValid)
            {
                // consulta si el identificador del curso es diferente de cero, si es así estamos ante una edición.
                if (curso.Id != 0)
                {

                    DAL.Entities.Curso cursoParaEditar = db.Cursos.Where(s => s.Id.Equals(curso.Id)).FirstOrDefault();

                    cursoParaEditar.Titulo = curso.Titulo;
                    cursoParaEditar.FechaInicio = System.DateTime.Parse(curso.FechaInicio, CultureInfo.CreateSpecificCulture("en-US"));
                    cursoParaEditar.CupoMaximo = curso.CupoMaximo;


                    db.SaveChanges();

                    string Mensaje = "Curso editado correctamnte";
                    return RedirectToAction("Index", new { Mensaje });
                }
                else // sino, estamoos ante una creación.
                {

                    DAL.Entities.Curso nuevoCurso = new DAL.Entities.Curso(curso);

                    db.Cursos.Add(nuevoCurso);

                    db.SaveChanges();

                    string Mensaje = "Curso creado correctamnte";
                    return RedirectToAction("Index", new { Mensaje });
                }
            }
            else // si el estado del modelo no es válido es porque tengo un error en algún atributo
            {
                // armo el mensaje de error concatenando todos los mensajes de error del estado del modelo (ya que puedo tener más de uno)
                ViewBag.MensajeError = String.Join(", ", ModelState.Values.Where(v => v.Errors.Any()).Select(e => e.Errors[0].ErrorMessage).ToArray());

               

                return View(curso);
            }
        }

        public ActionResult Borrar(int id = 0)
        {

            DAL.Entities.Curso cursoParaEliminar = db.Cursos.Where(p => p.Id.Equals(id)).FirstOrDefault();
            //Si no la encontramos, devolvemos un error 404
            if (cursoParaEliminar == null) { return HttpNotFound(); }
            //Si la encontramos, le indicamos a la base de datos que la elimine
            db.Cursos.Remove(cursoParaEliminar);
            //y luego que guarde los cambios
            db.SaveChanges();
            //una vez que realizó la acción, volvemos a la lista de cursos.
            string Mensaje = "Curso eliminado correctamnte";
            return RedirectToAction("Index", new { Mensaje });
        }


        public ActionResult BorrarInscripto(int id = 0)
        {

            DAL.Entities.Usuario_Curso usuarioCurso = db.Usuario_Cursos.Where(p => p.UsuarioId==id).FirstOrDefault();

            //Si no la encontramos, devolvemos un error 404
            if (usuarioCurso == null) { return HttpNotFound(); }

            int? idCurso = usuarioCurso.CursoId;
            //Si la encontramos, le indicamos a la base de datos que la elimine
            db.Usuario_Cursos.Remove(usuarioCurso);
            //y luego que guarde los cambios
            db.SaveChanges();

            DAL.Entities.Usuario usuario = db.Usuarios.Where(p => p.Id == id).FirstOrDefault();
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
           
            string Mensaje = "Inscripto eliminado correctamente";
            return RedirectToAction("Inscriptos", new {id=idCurso, Mensaje });
        }
    }
}
