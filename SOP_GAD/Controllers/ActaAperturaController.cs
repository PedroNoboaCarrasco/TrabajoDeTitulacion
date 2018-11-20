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
    public class ActaAperturaController : Controller
    {
        // GET: ActaApertura
        ObrasPublicas Obras = new ObrasPublicas();
        List<ObrasPublicas> ListaObras = new List<ObrasPublicas>();
        private ActaAperturaOfertas actaapertura = new ActaAperturaOfertas();
        public ActionResult IActaAperturaIngreso(int id = 0, int obraId = 0)
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
            {
                   ViewBag.ActaObraID = actaapertura.ObtenerActaApertura(id).IdObraPublica;
                   ViewBag.Obra = Obras.ObtenerObraPublica(actaapertura.ObtenerActaApertura(id).IdObraPublica).ObjetoProcesoObra;
            }


            return View(id > 0 ? actaapertura.ObtenerActaApertura(id) : actaapertura);
        }


        //Ingresar o Modificar

        public ActionResult Guardar(ActaAperturaOfertas model)
        {
            int Estado = 0;

            if (ModelState.IsValid)
            {
               Estado = model.Guardar();
                // redirecciona a otra pag con un parametro
                //return Redirect("~/ActaApertura/IActaAperturaIngreso/" + model.IdActaApertura + "?obraId=" + model.IdObraPublica);
                if (Estado == 6 || Estado == 7)
                {
                    return Redirect("~/Ofertantes/IOfertantesLista/" + model.IdActaApertura + "?Estado=" + Estado);

                }
                else
                {
                    return View("~/views/InformeAclaraciones/IInformeAclaracionIngreso.cshtml", model);
                }
            }
            else
            {
                return View("~/views/ActaApertura/IActaAperturaIngreso.cshtml", model);
            }
        }

        // Generar el PDF
        public ActionResult ActaDeApertura(int id = 0)
        {
            ActaAperturaOfertas actaAper = new ActaAperturaOfertas();
            ActaAperturaReport ActaDEAperturaReport = new ActaAperturaReport();
            byte[] abytes = ActaDEAperturaReport.PrepareReport(actaAper.ObtenerActaApertura2(id));
            return File(abytes, "application/pdf");
        }


        //Fin declase
    }
}