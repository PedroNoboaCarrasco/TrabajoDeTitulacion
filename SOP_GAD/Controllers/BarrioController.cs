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
    public class BarrioController : Controller
    {
        // GET: Barrio
        //BARRIO agrego la clase
        private Barrio barrio = new Barrio();

        //Ir a una pag Principal
        public ActionResult Index()
        {
            return View();
        }
        //Ir a pag Ingresar Barrio
        public ActionResult IBarrioIngreso(int id=0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Cabecera parroquial", Value = "Cabecera parroquial" });
            items.Add(new SelectListItem { Text = "Comunidad El Socorro", Value = "Comunidad El Socorro" });
            items.Add(new SelectListItem { Text = "Comunidad San Clemente", Value = "Comunidad San Clemente" });
            items.Add(new SelectListItem { Text = "Comunidad El Porlón", Value = "Comunidad El Porlón" });
            items.Add(new SelectListItem { Text = "Cubijies", Value = "Cubijies" });
            ViewBag.MovieType = items;
            return View(id > 0 ? barrio.ObtenerBarrio(id) : barrio);
        }

        // Listar Los Barrios
        public ActionResult IBarriosLista()
        {
            return View(barrio.Listar());
        }

        //Para DropDownList Por Pedro Noboa
        public List<SelectListItem> ObtenerListaBarrio(string Asentamiento)
        {

            //var listado = new List<SelectListItem>() {
            var Lugar = new List<Barrio>();
            Lugar = barrio.ListarBarrio(Asentamiento);

            List<SelectListItem> estado = new List<SelectListItem>();
            foreach (var type in Lugar)
            {
                estado.Add(new SelectListItem { Text = type.NombreBarrio, Value = "1" });
            }
            return estado;
        }

        //Ingresar o Modificar

        public ActionResult Guardar(Barrio model)
        {
            
            if (ModelState.IsValid)
            {
                model.Guardar();
            // redirecciona a otra pag con un parametro
            return Redirect("~/Barrio/IBarrioIngreso/" + model.IdBarrio);
            }
            else
            {
                return View("~/views/Barrio/IBarrioIngreso.cshtml", model);
            }
        }


        [Authorize]
        public ActionResult Eliminar(Barrio model)
        {
            int result = 0;
            result = model.Eliminar(model.IdBarrio);
            if (result == 2)
            {
                model.Guardar();
            }

            return Redirect("~/Barrio/IBarrioLista");
        }


        //Fin clase
    }
}