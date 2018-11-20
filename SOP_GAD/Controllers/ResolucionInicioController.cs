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
    public class ResolucionInicioController : Controller
    {
        // GET: Resolucion de Inicio
        private ResolucionInicio ResolInicio = new ResolucionInicio();
        ObrasPublicas Obras = new ObrasPublicas();
        List<ObrasPublicas> ListaObras = new List<ObrasPublicas>();
        public ActionResult IResolucionInicioIngreso(int id = 0, int obraId = 0)
        {
            ListaObras = Obras.Listar();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (ObrasPublicas SelectObras in ListaObras)
            {
                items.Add(new SelectListItem { Text = SelectObras.ObjetoProcesoObra, Value = SelectObras.IdObraPublica.ToString() });
            }

            ViewBag.MovieType = items;
            ViewBag.IdObra = obraId;
            return View(id > 0 ? ResolInicio.ObtenerResolucionInicial(id) : ResolInicio);
        }

        //Ingresar o Modificar

        public ActionResult Guardar(ResolucionInicio model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
                // redirecciona a otra pag con un parametro
                return Redirect("~/ResolucionInicio/IResolucionInicioIngreso/" + model.IdResolucionI + "?obraId=" + model.IdObraPublica);
            }
            else
            {
                return View("~/views/ResolucionInicio/IResolucionInicioIngreso.cshtml", model);
            }
        }

        // Generar el PDF
        public ActionResult ResolucionDeInicio(int id=0)
        {
            ResolucionInicioReport ResolucionReport = new ResolucionInicioReport();
            byte[] abytes = ResolucionReport.PrepareReport(ResolInicio.ObtenerResolucionInicial2(id));
            return File(abytes, "application/pdf");
        }

        //public ActionResult ResolucionDeInicio(ResolucionInicio ResolucionIn)
        //{
        //    ResolucionInicioReport ResolucionReport = new ResolucionInicioReport();
        //    byte[] abytes = ResolucionReport.PrepareReport(ResolInicio.ResolucionInicialListar());
        //    return File(abytes, "application/pdf");
        //    //return File(abytes, "application/pdf","Resolición de Inicio.pdf");
        //}


    }
}