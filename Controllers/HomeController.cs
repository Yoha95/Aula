using Aula.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula.Controllers
{
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();
        public ActionResult Index(string Mensaje)
        {
            if (Mensaje != null)
            {
                ViewBag.Mensaje = Mensaje;
            }

            //si no hay registros en la tabla roles, cargo los roles.
            if (!db.Rols.Any())
            {
                db.Rols.Add(new DAL.Entities.Rol() { Id = 1, Nombre = "Docente" });
                db.Rols.Add(new DAL.Entities.Rol() { Id = 2, Nombre = "Alumno" });
                db.SaveChanges();
            }

            //recupero el listado de usuario desde la bd
            List<DAL.Entities.Usuario> usuarios = db.Usuarios.ToList();


            //mapeo el listado desde la bd a un listado para la vista 

            List<Models.Usuario> listaUsuarios = usuarios.Select(u => new Models.Usuario(u)).ToList();
            ViewBag.Encabezado = "Lista de Usuarios";

           

            return View(listaUsuarios);
        }

        public ActionResult Manage(int id = 0)
        {
            
            Models.Usuario usuario = new Models.Usuario();
  
            if (db.Rols.Any())
            {
       
                ViewBag.Roles = db.Rols.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            }
          
                
                usuario = new Models.Usuario(db.Usuarios.Where(s => s.Id.Equals(id)).FirstOrDefault());

                ViewBag.Encabezado = "Editar " + usuario.NombreCompleto;
                       

            ViewBag.MensajeError = string.Empty;
             
            return View(usuario);
        }

        [HttpPost]
       
        public ActionResult Manage(Models.Usuario usuario)
        {
           
            if (ModelState.IsValid)
            {
                   
                    DAL.Entities.Usuario usuarioParaEditar = db.Usuarios.Where(s => s.Id.Equals(usuario.Id)).FirstOrDefault();
                
                    usuarioParaEditar.NombreCompleto = usuario.NombreCompleto;
                    usuarioParaEditar.FechaNacimiento = System.DateTime.Parse(usuario.FechaNacimiento, CultureInfo.CreateSpecificCulture("en-US"));
                    usuarioParaEditar.Domicilio = usuario.Domicilio;
                    usuarioParaEditar.Email = usuario.Email;
                    usuarioParaEditar.RolId = usuario.Rol != null ? usuario.Rol.Id : (int?)null;

                    
                    db.SaveChanges();
                    
                    string Mensaje = "Usuario editado correctamnte";
                    return RedirectToAction("Index", new { Mensaje });
                
               
            }
            else // si el estado del modelo no es válido es porque tengo un error en algún atributo
            {
                // armo el mensaje de error concatenando todos los mensajes de error del estado del modelo (ya que puedo tener más de uno)
                ViewBag.MensajeError = String.Join(", ", ModelState.Values.Where(v => v.Errors.Any()).Select(e => e.Errors[0].ErrorMessage).ToArray());
              
                if (db.Rols.Any())
                {
                  
                    ViewBag.Roles = db.Rols.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
                }
               
                return View(usuario);
            }
        }

       

    }
}