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
    public class ObrasPublicasController : Controller
    {
        // GET: ObrasPublicas
        private Barrio objBarrio = new Barrio();
        private List<Barrio> ListaBarrio;
        private BarrioController BarrioC = new BarrioController();
        private ObrasPublicas ObrasP = new ObrasPublicas();

        [AllowAnonymous]
        public ActionResult IObrasPublicas(int id = 0)
        {
            ViewBag.ListaBarriosCP = BarrioC.ObtenerListaBarrio("Cabecera parroquial");
            ViewBag.ListaBarriosCS = BarrioC.ObtenerListaBarrio("Comunidad El Socorro");
            ViewBag.ListaBarriosCC = BarrioC.ObtenerListaBarrio("Comunidad San Clemente");
            ViewBag.ListaBarriosCPo = BarrioC.ObtenerListaBarrio("Comunidad El Porlón");

            if (id == 1)
            {
                ViewBag.Message = "La Obra se ELIMINO CORRECTAMENTE";
            }
            else if (id == 2)
            {
                ViewBag.Message = "Ocurrio un error en la operacion, revise la conexion a la base de datos";
            }
            else if (id == 3)
            {
                ViewBag.Message = "La Informacion de la Obra Publica no se puede eliminas porque ya esta en ejecucion";
            }

            return View(ObrasP.Listar());
        }

        public ActionResult IObraPublicaIngreso(int id = 0, int Estado=0)
        {
            ListaBarrio = objBarrio.Listar();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Barrio SelectBarrio in ListaBarrio)
            {
                items.Add(new SelectListItem { Text = SelectBarrio.NombreBarrio, Value = SelectBarrio.NombreBarrio });
            }

            ViewBag.MovieType = items;

            if (Estado == 1)
            {
                ViewBag.Message = "La Obra se a registrado CORRECTAMENTE";
            }
            else if (Estado == 2)
            {
                ViewBag.Message = "La infornmacion de la Obra se MODIFICO CORRECTAMENTE";
            }
            else if (Estado == 3)
            {
                ViewBag.Message = "Ocurrio un error en la operacion, revise la conexion a la base de datos";
            }

            if (id < 0) {
                double asd = ObrasP.ObtenerObraPublica(id).PresupuestoObra;
                String asd2 = asd.ToString();
                ViewBag.Presu = asd2;
            }
            
            //asd2.Replace(".",",");
            return View(id > 0 ? ObrasP.ObtenerObraPublica(id) : ObrasP);
        }

      
        public ActionResult IVerObraPublica(int id=0)
        {
            return View(id > 0 ? ObrasP.ObtenerObraPublica(id) : ObrasP);
        }
        //Ingresar o Modificar

        public ActionResult Guardar(ObrasPublicas model)
        {
            int Estado = 0;
            if (ModelState.IsValid)
            {
                Estado = model.Guardar();
                // redirecciona a otra pag con un parametro
                return Redirect("~/ObrasPublicas/IObraPublicaIngreso/" + model.IdObraPublica + "?Estado=" + Estado);
            }
            else
            {
                return View("~/views/ObrasPublicas/IObraPublicaIngreso.cshtml", model);
            }
        }

        public ActionResult Eliminar(ObrasPublicas model)
        {
            int Estado = 0;
            Estado = model.Eliminar(model.IdObraPublica);

            //return View("~/views/ObrasPublicas/IObrasPublica.cshtml", Estado);
            return Redirect("~/ObrasPublicas/IObrasPublicas/" + Estado);
        }

        public ObrasPublicas ObtenerOP(int id = 0)
        {
            return ObrasP.ObtenerObraPublica(id);
        }
        

        //Fin de class
    }
}