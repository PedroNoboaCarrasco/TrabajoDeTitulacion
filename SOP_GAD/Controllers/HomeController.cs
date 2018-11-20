using SOP_GAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOP_GAD.Controllers
{
    //Para autorizar al usuario.
    [SessionExpireFilter] // Aplicar filtro a todo el controlador
    public class HomeController : Controller
    {
        Usuario usuario = new Usuario();
        // GET: Home
        public ActionResult Index()
        {
            return View(usuario.ObtenerUsuario2(Convert.ToInt32(Session["CodigoUser"])));
        }
    }
}