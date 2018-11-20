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
    public class CertificadoPresupuestoController : Controller
    {
        // GET: CertificadoPresupuesto
        CertificacionPresupuestaria CertificadoP = new CertificacionPresupuestaria();
        ObrasPublicas Obras = new ObrasPublicas();
        List<ObrasPublicas> ListaObras = new List<ObrasPublicas>();

        public ActionResult ICertificadoPresupuestoIngreso(int id = 0,int obraId =0)
        {
            ListaObras = Obras.Listar();
            List<SelectListItem> items = new List<SelectListItem>();
            
            foreach (ObrasPublicas SelectObras in ListaObras)
            {
                items.Add(new SelectListItem { Text = SelectObras.ObjetoProcesoObra, Value = SelectObras.IdObraPublica.ToString() });
            }
            
            ViewBag.MovieType = items;
            ViewBag.IdObra = obraId;
            return View(id > 0 ? CertificadoP.ObtenerCertificacionP(id) : CertificadoP);
        }

        public CertificacionPresupuestaria ObtenerCP(int id=0)
        {
            return CertificadoP.ObtenerCertificacionP(id);
        }
        

        //Ingresar o Modificar

        public ActionResult Guardar(CertificacionPresupuestaria model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
                // redirecciona a otra pag con un parametro
                return Redirect("~/CertificadoPresupuesto/ICertificadoPresupuestoIngreso/" + model.IdCertificadoP+ "?obraId=" + model.IdObraPublica);
            }
            else
            {
                return View("~/views/CertificadoPresupuesto/ICertificadoPresupuestoIngreso.cshtml", model);
            }
        }

        //Genera el pdf
        public ActionResult CertificacionPresupuestariaPdf(int id = 0)
        {
            CertificadoPresupuestoReport ResolucionReport = new CertificadoPresupuestoReport();
            byte[] abytes = ResolucionReport.PrepareReport(CertificadoP.ObtenerCertificacionP2(id));
            return File(abytes, "application/pdf");
            //return File(abytes, "application/pdf","Resolición de Inicio.pdf");
        }
        //Fin class
    }
}