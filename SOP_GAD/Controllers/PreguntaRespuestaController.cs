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
    public class PreguntaRespuestaController : Controller
    {
        // GET: PreguntaRespuesta
        PreguntaRespuesta pregunR = new PreguntaRespuesta();
        InformePreguntaR actaAclaraciones = new InformePreguntaR();
        public ActionResult IPreguntaRIngreso(int id = 0, int Informe = 0)
        {
            
            ViewBag.InformeAclaracion = Informe;
            
            return View(id > 0 ? pregunR.ObtenerPreguntaRes(id) : pregunR);
        }

        public ActionResult IPreguntasRespuestasLista(int id = 0, int Estado = 0)
        {
            if (Estado == 1)
            {
                ViewBag.Message = "La pregunta se registro CORRECTAMENTE";
            } else if (Estado == 2)
            {
                ViewBag.Message = "La pregunta del informe se MODIFICO CORRECTAMENTE";
            }
            else if (Estado == 3)
            {
                ViewBag.Message = "Ocurrio un error en la operación, revise la conexión a la base de datos";
            }
            else if (Estado == 4)
            {
                ViewBag.Message = "La pregunta del informe fue ELIMINADO CORRECTAMENTE";
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


            ViewBag.InformeAclaracion = id;
            ViewBag.IdObra = actaAclaraciones.ObtenerAclaracion(id).IdObraPublica;
            return View(pregunR.Listar(id));
        }


        public ActionResult Guardar(PreguntaRespuesta model)
        {
            int Estado = 0;
            if (ModelState.IsValid)
            {
                Estado = model.Guardar();
                // redirecciona a otra pag con un parametro
                return Redirect("~/PreguntaRespuesta/IPreguntasRespuestasLista/" + model.IdInformePR + "?Estado=" + Estado);
            }
            else
            {
                return View("~/views/PreguntaRespuesta/IPreguntaRIngreso.cshtml", model);
            }
        }

        public ActionResult Eliminar(int id  = 0, int IdInformePR = 0 )
        {
            PreguntaRespuesta model = new PreguntaRespuesta();
            int result = 0;
            result = model.Eliminar(id);

            return Redirect("~/PreguntaRespuesta/IPreguntasRespuestasLista/" + IdInformePR + "?Estado=" + result); 
        }

        //Fin de clase
    }
}