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
    public class OfertantesController : Controller
    {
        // GET: Ofertantes

        private Ofertantes ofert = new Ofertantes();

        public ActionResult IOfertantesIngreso(int id = 0, int Acta = 0)
        {
            if (id == 0)
            {

                int codi = ofert.Listar(Acta).Count();
                if (codi == 0)
                {
                    ViewBag.Codigo = 1;
                }
                else if (codi > 0)
                { 
                List<Ofertantes> listofertantes = ofert.Listar(Acta);
                ViewBag.Codigo = listofertantes[codi - 1].CodigoOfertantes + 1;
                }
            }
            else {
                ViewBag.Codigo = ofert.ObtenerOfertante(id).CodigoOfertantes;
            }

            ViewBag.ActaApertura = Acta;
            return View(id > 0 ? ofert.ObtenerOfertante(id) : ofert );
        }

        public ActionResult IOfertantesCalificacion(int id = 0, int obraId=0)
        {
            ViewBag.ActaObraID = obraId;
            return View(ofert.Listar(id));
        }
        public ActionResult IOfertantesLista(int id = 0, int Estado = 0)
        {
            ActaAperturaOfertas actaofer = new ActaAperturaOfertas();
            ObrasPublicas obra = new ObrasPublicas();
            if (Estado == 1)
            {
                ViewBag.Message = "El ofertante se a registro CORRECTAMENTE";
            }
            else if (Estado == 2)
            {
                ViewBag.Message = "La infornmacion del ofertante se MODIFICO CORRECTAMENTE";
            }
            else if (Estado == 3)
            {
                ViewBag.Message = "Ocurrio un error en la operacion, revise la conexion a la base de datos";
            }
            else if (Estado == 4)
            {
                ViewBag.Message = "La información del ofertante fue ELIMINADO CORRECTAMENTE";
            }
            else if (Estado == 5)
            {
                ViewBag.Message = "Ocurrio un error en la realizacion de la operacion, revise la conexión a la base de datos";
            }
            else if (Estado == 6)
            {
                ViewBag.Message = "La informacion fue REGISTRADA CORRECTAMENTE";
            }
            else if (Estado == 7)
            {
                ViewBag.Message = "La informacion fue MODIFICADA CORRECTAMENTE";
            }
            //ViewBag.Obra= obra.ObtenerObraPublica(actaofer.ObtenerActaApertura(id).IdObraPublica).ObjetoProcesoObra;    
            ViewBag.Obra = actaofer.ObtenerActaApertura(id).IdObraPublica;
            ViewBag.ActaOfertantes = id;
            return View(ofert.Listar(id));
        }


        public ActionResult Guardar(Ofertantes model)
        {
            int Estado = 0;
            if (ModelState.IsValid)
            {
                Estado = model.Guardar();
                // redirecciona a otra pag con un parametro
                return Redirect("~/Ofertantes/IOfertantesLista/" + model.IdActaApertura + "?Estado=" + Estado);
            }
            else
            {
                return View("~/views/Ofertantes/IOfertantesIngreso.cshtml", model);
            }
        }


        public ActionResult Eliminar(int id = 0, int Acta = 0)
        {
            Ofertantes model = new Ofertantes();
            int result = 0;
            result = model.Eliminar(id);

            return Redirect("~/Ofertantes/IOfertantesLista/" + Acta + "?Estado=" + result);
        }




        //Fin class
    }
}