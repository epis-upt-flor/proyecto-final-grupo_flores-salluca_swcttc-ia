using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SistemaArtemis.Controllers
{
    public class ProblemaController : Controller
    {
        private Model1 db = new Model1();

        // GET: Problema
        private Problema objProblema = new Problema();
        private Cliente objCliente = new Cliente();


        public ActionResult Create(int id)
        {
            ViewBag.Cliente = objCliente.Listar1(id);
            return View();
        }

        /// <summary>
        /// Esta es una función de C# que crea un nuevo objeto "Problema" y lo agrega a una base de datos, luego
        /// lo redirige a la página "Índice" del controlador "Cliente".
        /// </summary>
        /// <param name="Problema">Esta es la clase modelo para la entidad "Problema", que representa un
        /// problema en el sistema.</param>
        /// <returns>
        /// Si el ModelState no es válido, el objeto "problema" se agrega a la base de datos y la acción se
        /// redirige a la acción "Índice" del controlador "Cliente". Si ModelState es válido, el objeto
        /// "problema" se devuelve a la vista.
        /// </returns>

        [HttpPost]
        //public ActionResult Create(Problema problema)
        public ActionResult Create([Bind(Include = "Id_Problema,Descripcion,Documento,Estado,Fecha_Inicio,Fecha_Fin,Id_Cliente")] Problema problema)
        {
            if (ModelState.IsValid==false)
            {
                db.Problema.Add(problema);
                db.SaveChanges();
                return RedirectToAction("~/Cliente/Index");
            }
            return View(problema);
        }



        /// <summary>
        /// Esta función devuelve una vista con datos obtenidos de una llamada de método con un parámetro
        /// entero.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que se pasa al método de acción "Índice". Se
        /// utiliza para recuperar un registro específico de la base de datos utilizando el método "Obtener" del
        /// objeto "objProblema" y luego pasarlo a la vista correspondiente.</param>
        /// <returns>
        /// El método `Index` está devolviendo una `Vista` con el resultado de llamar al método `Obtener` del
        /// objeto `objProblema` con el parámetro `id` pasado al método.
        /// </returns>

        public ActionResult Index(int id)
        {
            return View(objProblema.Obtener(id));
        }

        /// <summary>
        /// Esta función devuelve una vista con una lista de problemas para un ID de cliente específico.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que representa el ID de un cliente. Este
        /// método se utiliza para enumerar todos los problemas asociados con un cliente en particular
        /// identificado por la ID dada.</param>
        /// <returns>
        /// El método `ListarProblemaCliente` está devolviendo una `Vista` con el resultado de llamar al
        /// método `ListarProblema` del objeto `objProblema`, pasando el parámetro `id` como argumento.
        /// </returns>
        public ActionResult ListarProblemaCliente(int id)
        {
            return View(objProblema.ListarProblema(id));
        }


     
        /// <summary>
        /// Esta función guarda un modelo "Problema" y redirige al índice "Cliente" si el estado del modelo es
        /// válido, de lo contrario devuelve la vista del modelo.
        /// </summary>
        /// <param name="Problema">Problema es una clase modelo que contiene propiedades y métodos relacionados
        /// con un problema específico en la aplicación. El método Guardar es un método de acción que se encarga
        /// de guardar los datos ingresados por el usuario en el modelo. Si ModelState no es válido, guarda los
        /// datos y redirige al usuario a</param>
        /// <returns>
        /// Si ModelState no es válido, el modelo se guarda y el usuario es redirigido a la página de índice del
        /// controlador de Cliente. Si ModelState es válido, se devuelve la vista con el modelo.
        /// </returns>
        public ActionResult Guardar(Problema model)
        {
            if (ModelState.IsValid == false)
            {
                model.Guardar();
                return Redirect("~/Cliente/index");
            }
            else
            {
                return View(model);
            }

        }      

           public ActionResult PublicarProblema()
        {
            return View();
        }

        public ActionResult Eliminar(int id)
        {
            objProblema.Id_Problema = id;
            objProblema.Eliminar();
            return Redirect("~/Problema");
        }

    }
}