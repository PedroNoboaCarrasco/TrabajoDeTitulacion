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
    public class OfertasController : Controller
    {
        // GET: Ofertas
        private Ofertas ofer = new Ofertas();
        private Ofertantes ofertante = new Ofertantes();
        private ActaCalificacionOfertas actaCali = new ActaCalificacionOfertas();
        List<Ofertas> lisofer;
        int numero = 0;
        public ActionResult IOfertasIngreso(int id = 0, int Acta=0, int Numof = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Si Cumple", Value = "1" });
            items.Add(new SelectListItem { Text = "No Cumple", Value = "0" });
            ViewBag.MovieType = items;

            if (id == 0)
            {
                ViewBag.OferNumero = ofer.NumeroOfer(Numof) + 1;
                String[] Parametro = { "Integridad de la oferta", "Equipo Mínimo", "Personal Técnico Mínimo", "Experiencia General Mínima", "Experiencia Mínima Personal Técnico", "Experiencia Especifica Mínima", "Metodología y Cronograma", " Otro (s) parámetros (s) resueltos por la entidad contratante" };
                ViewBag.ParametroOf = Parametro[ofer.NumeroOfer(Numof)];
                
                ViewBag.Acta = ofertante.ObtenerOfertante(Numof).IdActaApertura;
                ViewBag.IdOfer = Numof;

            }

                ViewBag.IdActaCalificacion = Acta;
                ViewBag.IdOfertante = Numof;
            return View(id > 0 ? ofer.ObtenerOferta(id) : ofer);
                
        }

        public ActionResult Guardar(Ofertas model)
        {
            int Estado = 0;
            if (ModelState.IsValid)
            {
                    Estado = model.Guardar();
                    return Redirect("~/Ofertas/IListaOfertas?idActa=" + model.IdActaCalificacion + "&idOfer=" + model.IdOfertante+ "&Estado="+ Estado);
                
            }
            else
            {
                return View("~/views/Ofertas/IOfertasIngreso.cshtml", model);
            }

        }

        public ActionResult IListaOfertas(int idActa = 0,int idOfer = 0, int Estado = 0)
        {
            ViewBag.Numero = ofer.NumeroOfer(idOfer);
            if (Estado == 1)
            {
                ViewBag.Message = "La calificación de la oferta se registro CORRECTAMENTE";
            }
            else if (Estado == 2)
            {
                ViewBag.Message = "La calificación de la oferta se ha MODIFICO CORRECTAMENTE";
            }
            else if (Estado == 3)
            {
                ViewBag.Message = "Ocurrio un error en la operación, revise la conexión a la base de datos";
            }
            else if (Estado == 4)
            {
                ViewBag.Message = "La calificación de la oferta fue ELIMINADO CORRECTAMENTE";
            }
            else if (Estado == 5)
            {
                ViewBag.Message = "Ocurrio un error en la elimación de la calificación, revise la conexión a la base de datos";
            }
            ViewBag.Ofertante = idOfer;
            ViewBag.IdCalificacion = idActa;
            ViewBag.IdObra = actaCali.ObtenerActaCalificacion(idActa).IdObraPublica;
            return View(ofer.Listar(idOfer,idActa));
        }


        //Fin de class
    }
}