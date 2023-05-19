using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class ServicioController : Controller
    {
       /* Estas líneas de código crean instancias de las clases `Problema` y `Servicio` respectivamente
       y las asignan a las variables privadas `objproblema` y `objServicio`. Estas instancias se
       pueden usar para acceder a los métodos y propiedades de las clases `Problema` y `Servicio`
       dentro de la clase `ServicioController`. */
        private Problema objproblema= new Problema();
        private Servicio objServicio = new Servicio();
        //private Tecnico objTecnico= new Tecnico();

        Model1 db=new Model1();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// La función Lista devuelve una vista con una lista de problemas.
        /// </summary>
        /// <returns>
        /// El método `List()` está devolviendo una vista con una lista de problemas obtenidos del método
        /// `ListProblem()` del objeto `objproblema`.
        /// </returns>
        public ActionResult List()
        {
            return View(objproblema.ListProblem());
        }


  
        /// <summary>
        /// Esta función guarda un modelo de servicio y actualiza el estado de un problema relacionado a
        /// "En proceso".
        /// </summary>
        /// <param name="Servicio">una clase de modelo que contiene datos relacionados con un servicio
        /// que se está realizando.</param>
        /// <returns>
        /// Si el estado del modelo es válido, el método devuelve una redirección a la acción "Index"
        /// del controlador "Tecnico". Si el estado del modelo no es válido, el método devuelve la vista
        /// "Guardar" con errores de validación.
        /// </returns>
        public ActionResult Guardar(Servicio model)
        {
            if (ModelState.IsValid==false)
            {
                model.Guardar();

               /* Estas líneas de código están actualizando el estado de un problema relacionado con un
               servicio que se está realizando. */
                Problema problema = db.Problema.Find(model.Id_Problema);
                problema.Estado ="En Proceso";
                db.SaveChanges();


                return Redirect("~/Tecnico/Index");// ~/Miembro_Proyecto/Index"
            }
            else
            {
                // Si el modelo no es válido, regresa la vista con los errores de validación
                return View(model);
            }
        }


        /// <summary>
        /// Esta función devuelve una vista para crear un nuevo servicio o editar uno existente, con una
        /// lista de problemas pasada como ViewBag.
        /// </summary>
        /// <param name="id">El parámetro "id" es un número entero que tiene un valor predeterminado de 0.
        /// Se utiliza para identificar un registro o entidad específica en la base de datos. En este caso,
        /// se utiliza para recuperar un objeto "Servicio" específico de la base de datos o para crear uno
        /// nuevo si el "id</param>
        /// <returns>
        /// El método devuelve una vista con un objeto modelo de tipo `Servicio`. Si el parámetro `id` es
        /// igual a 0, se crea una nueva instancia de `Servicio` y se pasa como objeto modelo. De lo
        /// contrario, se llama al método `Obtener` del objeto `objServicio` para recuperar un objeto
        /// `Servicio` existente con el `id` especificado, que es
        /// </returns>
        public ActionResult Create(int id=0)
        {
            var problemas = new Problema().Listar(id);
            ViewBag.Prob = problemas;

            return View(id==0 ? new Servicio() : objServicio.Obtener(id));
        }


    }
}