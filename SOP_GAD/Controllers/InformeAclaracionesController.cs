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
    public class InformeAclaracionesController : Controller
    {
        // GET: InformeAclaraciones
        InformePreguntaR aclaracion = new InformePreguntaR();
        ObrasPublicas obra = new ObrasPublicas();
        List<ObrasPublicas> ListaObras = new List<ObrasPublicas>();
        ResolucionInicio resolucion = new ResolucionInicio();
        InformePreguntaR iformePR = new InformePreguntaR();
        public ActionResult IInformeAclaracionIngreso(int id = 0, int obraId = 0)
        {
            ListaObras = obra.Listar();
            List<SelectListItem> items = new List<SelectListItem>();


            foreach (ObrasPublicas SelectObras in ListaObras)
            {
                items.Add(new SelectListItem { Text = SelectObras.ObjetoProcesoObra, Value = SelectObras.IdObraPublica.ToString() });
            }

            ViewBag.MovieType = items;
            ViewBag.IdObra = obraId;
            return View(id > 0 ? aclaracion.ObtenerAclaracion2(obraId) : aclaracion);
        }

        //Ingresar o Modificar

        public ActionResult Guardar(InformePreguntaR model)
        {
            int Estado=0;

            if (ModelState.IsValid)
            {
                model.IdResolucionI= resolucion.ObtenerResolucionInicial2(model.IdObraPublica).IdResolucionI; 
                Estado = model.Guardar();
                // redirecciona a otra pag con un parametro
                //return Redirect("~/InformeAclaraciones/IInformeAclaracionIngreso/" + model.IdObraPublica);
                if (Estado == 6 || Estado == 7)
                {
                    return Redirect("~/PreguntaRespuesta/IPreguntasRespuestasLista/" + model.IdInformePR + "?Estado=" + Estado);
                }
                else
                {
                        return View("~/views/InformeAclaraciones/IInformeAclaracionIngreso.cshtml", model);
                }
            }
            else
            {
                return View("~/views/InformeAclaraciones/IInformeAclaracionIngreso.cshtml", model);
            }
        }


        // Generar el PDF
        public ActionResult ActaDeAclaraciones(int id = 0)
        {
            ActaAclaracionReport ResolucionReport = new ActaAclaracionReport();
            byte[] abytes = ResolucionReport.PrepareReport(iformePR.ObtenerAclaracion2(id));
            return File(abytes, "application/pdf");
            //return File(abytes, "application/pdf","Resolición de Inicio.pdf");
        }

        //Fin de class
    }
}