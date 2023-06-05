using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SistemaArtemis.Models.Login
{
    public class SessionHelper
    {

       /// <summary> La función comprueba si hay un usuario autenticado en la sesión actual.</summary>
       /// <returns> Un valor booleano que indica si hay un usuario autenticado en la sesión actual o no.</returns>
        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

       /// <summary>La función destruye la sesión del usuario cerrándolo mediante FormsAuthentication</summary>
        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }


        /// <summary> Esta función recupera el ID de usuario de FormsAuthenticationTicket en el HttpContext actual.</summary>
        /// <returns> El método devuelve un valor entero que representa el ID de usuario.</returns>
        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }

        /// <summary> Esta función agrega una ID de usuario a la sesión actual y actualiza la cookie de autenticación con la nueva ID. </summary>
        /// <param name="id">El ID de usuario que desea agregar a la sesión. Suele ser un identificador único para el usuario, como su nombre de usuario o una ID de base de datos.</param>

        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                          ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

       /// <summary> La función "AddUserToSession" se declara como interna y estática, toma un parámetro de objeto y lanza una NotImplementedException.</summary>
       /// <param name="p">El parámetro "p" es del tipo "objeto" y es probable que se use para pasar algunos datos u objetos relacionados con la adición de un usuario a una sesión. Sin embargo, dado que el cuerpo del método arroja una "NotImplementedException", no está claro qué se supone que debe hacer el método con el parámetro.</param>
        internal static void AddUserToSession(object p)
        {
            throw new NotImplementedException();
        }

    }
}