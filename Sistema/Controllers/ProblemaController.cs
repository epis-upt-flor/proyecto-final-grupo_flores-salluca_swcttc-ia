/*
Versión: 1.0
Descripción: ProblemaController representada gestionar la solitud del servicio
Para el caso de uso: 
    Gestionar Solicitud de Servicio
    Gestionar Recomendacion
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

        /// <summary>Esta función devuelve una vista con una lista de problemas para un ID de cliente específico.</summary>
        /// <returns>El método `ListarProblemaCliente` está devolviendo una `Vista` con el resultado de llamar al método `ListarProblema` del objeto `objProblema`, pasando el parámetro `id` como argumento.
        /// </returns>
        public ActionResult ListarProblemaCliente(int id)
        {
            
            return View(objProblema.ListarProblema(id));
        }

        public ActionResult PublicarProblema()
        {
            return View();
        }

        /// <summary>Esta función guarda un modelo "Problema" y redirige al índice "Cliente" si el estado del modelo es válido.     
        /// <returns>Si ModelState no es válido, el modelo se guarda y el usuario es redirigido a la página de índice del controlador de Cliente.
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

        //public ActionResult Eliminar(int id)
        //{
        //    objProblema.Id_Problema = id;
        //    objProblema.Eliminar();
        //    return Redirect("~/Problema");
        //}

    }
}