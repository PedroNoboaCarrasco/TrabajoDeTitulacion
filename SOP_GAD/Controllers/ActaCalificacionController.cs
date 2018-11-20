using SOP_GAD.Models;
using SOP_GAD.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOP_GAD.Controllers
{
    //Para autorizar al usuario.
    [SessionExpireFilter] // Aplicar filtro a todo el controlador
    public class ActaCalificacionController : Controller
    {
        // GET: ActaCalificacion
        ObrasPublicas Obras = new ObrasPublicas();
        List<ObrasPublicas> ListaObras = new List<ObrasPublicas>();
        private ActaCalificacionOfertas actacali = new ActaCalificacionOfertas();
        public ActionResult IActaCalificacionIngreso(int id = 0, int obraId = 0)
        {
            ListaObras = Obras.Listar();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (ObrasPublicas SelectObras in ListaObras)
            {
                items.Add(new SelectListItem { Text = SelectObras.ObjetoProcesoObra, Value = SelectObras.IdObraPublica.ToString() });
            }

            ViewBag.MovieType = items;
            ViewBag.IdObra = obraId;

            if (id > 0)
            { ViewBag.Obra = Obras.ObtenerObraPublica(actacali.ObtenerActaCalificacion(id).IdObraPublica).ObjetoProcesoObra; }

            return View(id > 0 ? actacali.ObtenerActaCalificacion(id) : actacali);
        }


        //Ingresar o Modificar

        public ActionResult Guardar(ActaCalificacionOfertas model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
                // redirecciona a otra pag con un parametro
                return Redirect("~/ActaCalificacion/IActaCalificacionIngreso/" + model.IdActaCalificacion);
            }
            else
            {
                return View("~/views/ActaCalificacion/IActaCalificacionIngreso.cshtml", model);
            }
        }

        // Generar el PDF
        public ActionResult ActaDeCalificacion(int id = 0)
        {
            ActaCalificacionOfertas actaCali = new ActaCalificacionOfertas();
            ActaCalificacionReport ActaDEAperturaReport = new ActaCalificacionReport();
            byte[] abytes = ActaDEAperturaReport.PrepareReport(actaCali.ObtenerActaCalificacion2(id));
            return File(abytes, "application/pdf");
        }

        // Generar el PDF de Prueba
        public ActionResult Pruebapdf(int id = 0)
        {
            PruebaReport ActaDEAperturaReport = new PruebaReport();
            byte[] abytes = ActaDEAperturaReport.PrepareReport();
            return File(abytes, "application/pdf");
        }

        //Fin de clase
    }
}