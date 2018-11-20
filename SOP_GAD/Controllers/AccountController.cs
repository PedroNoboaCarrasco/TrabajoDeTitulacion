using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOP_GAD.Models;
using System.Web.Security;

namespace SOP_GAD.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario model, string returnUrl)
        {
            // Insertar un nuevo usuario en la base de datos
            try
            {
                using (var context = new TextContext())
                {
                    var user = context.Usuario.Where(a => a.CedulaUsuario.Equals(model.CedulaUsuario) && a.ContraseniaUsuario.Equals(model.ContraseniaUsuario) && a.EstadoUsuario == 1).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.CedulaUsuario, true);
                        AccountController.AddUserToSession(user.IdUsuario.ToString());

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["User"] = user;
                            Session["CodigoUser"] = user.IdUsuario;
                            return RedirectToAction("Index", "Home"/*,new {id=user.IdUsuario}*/);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "");
                    }
                }
            }
            catch
            {
                throw;
            }
            ModelState.Remove("Password");
            return View();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session["User"] = null;
            
            return RedirectToAction("Login", "Account"); ;
        }

        public static int GetUser()
        {
            int user_id = 1;
            if (System.Web.HttpContext.Current.User!= null && System.Web.HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }


        public static void AddUserToSession(string id)
        {
            var usuario = new Usuario();
            usuario = usuario.ObtenerUsuario2(int.Parse(id));
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie(usuario.CedulaUsuario, persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}