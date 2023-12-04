/*
Versión: 1.0
Descripción: ServicioController.cs representada gestionar la asignaciòn de trabajo.
Para el caso de uso: 
    Gestionar Asignaciòn de Trabajo
    Gestionar Calificaciòn
    Gestionar Progreso

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/



using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
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
        private Archivos objarchivo = new Archivos();
        private Tecnico objtecnico = new Tecnico();

        /// <summary>La función Lista devuelve una vista con una lista de problemas.</summary>
        public ActionResult List()
        {
            ViewBag.listServicio=objServicio.Mispostulantes();

            return View(objproblema.ListProblem());
        }

        public ActionResult ListService()
        {
            return View(objServicio.ListarTrabajosAceptados());
        }

        /// <summary>Esta función guarda un modelo de servicio y actualiza el estado de un problema relacionado a "En proceso". </summary>
        public ActionResult Guardar(Servicio model)
        {
            if (ModelState.IsValid==true)
            {
                model.Guardar();
               /* Estas líneas de código están actualizando el estado de un problema relacionado con un servicio que se está realizando. */
                Problema problema = db.Problema.Find(model.Id_Problema);
                //problema.Estado = "En Revisión";
                db.SaveChanges();
                return Redirect("~/Servicio/List");// ~/Miembro_Proyecto/Index"
            }
            else
            {

                model.Guardar();
                /* Estas líneas de código están actualizando el estado de un problema relacionado con un servicio que se está realizando. */
                Problema problema = db.Problema.Find(model.Id_Problema);
                //problema.Estado = "En Revisión";
                db.SaveChanges();
                return Redirect("~/Servicio/List");// ~/Miembro_Proyecto/Index"
                // Si el modelo no es válido, regresa la vista con los errores de validación
                //return Redirect("~/Servicio/List");
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
            ViewBag.Lservicio=objServicio.Obtener(id);

            return View();
            
            //return View(id==0 ? new Servicio() : objServicio.Obtener());
        }


        public ActionResult Guardar2(int id)
        {
            // Eliminar registros de la tabla Archivos
            var archivosAEliminar = from archivo in db.Archivos
                                    join servicio in db.Servicio on archivo.Id_Tecnico equals servicio.Id_Tecnico
                                    where servicio.Id_Servicio == id
                                    select archivo;
            db.Archivos.RemoveRange(archivosAEliminar);
            db.SaveChanges();

            var servicioAEliminar = db.Servicio.FirstOrDefault(s => s.Id_Servicio == id);
            if (servicioAEliminar != null)
            {
                db.Servicio.Remove(servicioAEliminar);
                db.SaveChanges();
            }


            return Redirect("~/Cliente/Index");
        }

        public ActionResult Modificar(int id)
        {
            // Estas líneas de código están actualizando el estado de un problema relacionado con un servicio que se está realizando.
            Problema problema = db.Problema.Find(id);
            Servicio servicio = db.Servicio.FirstOrDefault(s => s.Id_Problema == id);

            problema.Estado = "En Proceso";
            servicio.Id_Estado_Servicio = 3;
            db.SaveChanges();

            // Cambiar el estado de todos los registros en Servicio con Id_Estado_Servicio igual a 2 a 5
            var serviciosConEstado2 = db.Servicio.Where(s => s.Id_Estado_Servicio == 2).ToList();
            foreach (var servicioEstado2 in serviciosConEstado2)
            {
                servicioEstado2.Id_Estado_Servicio = 5;
            }
            db.SaveChanges();

            return Redirect("~/Cliente/Index");
        }



        public ActionResult Details(int id)
        {
            var archivo = new Archivos().Listar(id);
            ViewBag.Archiv = archivo;
            ViewBag.detalles = objServicio.Detalles(id);
            return View();
        }

        public ActionResult VerPostulantes()
        {
            ViewBag.listecnico=objtecnico.Listar();
            ViewBag.listar = objarchivo.ListarTodo();
            ViewBag.listproblema = objproblema.ListProblem();
            return View(objServicio.Mispostulantes());
        }

       

        //Vista Cliente
        public ActionResult DetallesServicio(int id)
        {
            return View(objServicio.DetallesServicio(id));
        }

        //Vista Tecnico
        public ActionResult DetallesServicioTecnico(int id)
        {
            return View(objServicio.DetallesServicio(id));
        }



    }
}