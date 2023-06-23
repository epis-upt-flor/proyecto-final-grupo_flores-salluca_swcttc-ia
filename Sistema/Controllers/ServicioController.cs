using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class ServicioController : Controller
    {
        /* Estas líneas de código crean instancias de las clases `Problema` y `Servicio` respectivamente
        y las asignan a las variables privadas `objproblema` y `objServicio`. Estas instancias se pueden usar para acceder a los métodos y propiedades de las clases `Problema` y `Servicio` dentro de la clase `ServicioController`. */
        Model1 db = new Model1();
        private Problema objproblema= new Problema();
        private Servicio objServicio = new Servicio();         

        /// <summary>La función Lista devuelve una vista con una lista de problemas.</summary>
        public ActionResult List()
        {
            return View(objproblema.ListProblem());
        }

        public ActionResult ListService()
        {
            return View(objServicio.Listar());
        }

        /// <summary>Esta función guarda un modelo de servicio y actualiza el estado de un problema relacionado a "En proceso". </summary>
        public ActionResult Guardar(Servicio model)
        {
            if (ModelState.IsValid==false)
            {
                model.Guardar();
               /* Estas líneas de código están actualizando el estado de un problema relacionado con un servicio que se está realizando. */
                Problema problema = db.Problema.Find(model.Id_Problema);
                problema.Estado = "En Revisión";
                db.SaveChanges();
                return Redirect("~/Tecnico/Index");// ~/Miembro_Proyecto/Index"
            }
            else
            {
                // Si el modelo no es válido, regresa la vista con los errores de validación
                return Redirect("~/Servicio/List");
            }
        }     

        /// <summary> Esta función devuelve una vista para crear un nuevo servicio o editar uno existente, con una lista de problemas pasada como ViewBag.</summary>
        /// <returns>El método devuelve una vista con un objeto modelo de tipo `Servicio`. Si el parámetro `id` es
        /// igual a 0, se crea una nueva instancia de `Servicio` y se pasa como objeto modelo. De lo contrario, se llama al método `Obtener` del objeto `objServicio` para recuperar un objeto `Servicio` existente con el `id` especificado </returns>
        public ActionResult Create(int id=0)  //ok
        {
            var archivo = new Archivos().Listar(id);
            var problemas = new Problema().Listar(id);
            ViewBag.Archiv = archivo;
            ViewBag.Prob = problemas;


            return View(id==0 ? new Servicio() : objServicio.Obtener(id));
        }


        public ActionResult Guardar2(int id)
        {
            // Eliminar los registros de Servicio según el id_Servicio
            var serviciosAEliminar = db.Servicio.Where(s => s.Id_Problema == id);
            db.Servicio.RemoveRange(serviciosAEliminar);
            db.SaveChanges();

            /* Estas líneas de código están actualizando el estado de un problema relacionado con un
            servicio que se está realizando. */
            Problema problema = db.Problema.Find(id);
            if (problema != null)
            {
                problema.Estado = "Pendiente";
                db.SaveChanges();
            }

            return Redirect("~/Cliente/Index");
        }

        public ActionResult modificar(int id)
        {
            /* Estas líneas de código están actualizando el estado de un problema relacionado con un
            servicio que se está realizando. */
            Problema problema = db.Problema.Find(id);
            Servicio servicio = db.Servicio.FirstOrDefault(s => s.Id_Problema == id);

            if (problema != null || servicio != null)
            {
                problema.Estado = "En Proceso";
                servicio.Id_Estado_Servicio = 3;

                db.SaveChanges();
            }

            return Redirect("~/Cliente/Index");
        }


        public ActionResult Details(int id)
        {
            var archivo = new Archivos().Listar(id);
            ViewBag.Archiv = archivo;
            ViewBag.detalles = objServicio.Detalles(id);
            return View();
        }


    }
}