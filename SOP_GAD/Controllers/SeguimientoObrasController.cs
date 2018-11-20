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
    public class SeguimientoObrasController : Controller
    {
        // GET: Seguimiento de Obras
        private ObrasPublicas ObrasP = new ObrasPublicas();
        private CertificacionPresupuestaria certificaP = new CertificacionPresupuestaria();
        private ResolucionInicio resolucionI = new ResolucionInicio();
        private InformePreguntaR informePR = new InformePreguntaR();

        private ActaAperturaOfertas actaAp = new ActaAperturaOfertas();
        private ActaCalificacionOfertas actaCali = new ActaCalificacionOfertas();
        private ActaAdjudicacion actaAdju = new ActaAdjudicacion();
        public ActionResult ISeguimientoObrasLista(int id = 0)
        {
            ViewBag.CertificadoID = 0;
            ViewBag.ResolucionID = 0;
            ViewBag.ActaAclaracionID = 0;

            ViewBag.ActaAperturaID = 0;
            ViewBag.ActaCalificacionID = 0;
            ViewBag.ActaAdjudicacionID = 0;

            CertificacionPresupuestaria CerPresu = certificaP.ObtenerCertificacionP2(id);
            ResolucionInicio ResoInicioObra = resolucionI.ObtenerResolucionInicial2(id);
            InformePreguntaR InformePreR = informePR.ObtenerAclaracion2(id);

            ActaAperturaOfertas AcApertura = actaAp.ObtenerActaApertura2(id);
            ActaCalificacionOfertas AcCalifica = actaCali.ObtenerActaCalificacion2(id);

            ObrasPublicas Obra = ObrasP.ObtenerObraPublica(id);

            ViewBag.ActaObraID = Obra.IdObraPublica;

            //-------------------------------------------------------
            //Informacion de Certificasion Presupuestaria
            if (CerPresu != null)
            {
                ViewBag.CertificadoONum = CerPresu.NumeroCertificadoP;
                ViewBag.CertificadoOPar = CerPresu.PartidaPresupuestaria;
                ViewBag.CertificadoODes = CerPresu.DescripcionCertificadoP;
                ViewBag.CertificadoOValor = CerPresu.ValorReferencial;
                ViewBag.CertificadoObra = Obra.ObjetoProcesoObra;
                ViewBag.CertificadoID = CerPresu.IdCertificadoP;
            }
            //-------------------------------------------------------
            //Informacion de Resolucion Inicia
            if (ResoInicioObra != null)
            {
                ViewBag.ResolucionONumR = ResoInicioObra.NumeroResolucionI;
                ViewBag.ResolucionONomA = ResoInicioObra.NombreAnalista;
                ViewBag.ResolucionObraP = Obra.ObjetoProcesoObra;
                ViewBag.ResolucionID = ResoInicioObra.IdResolucionI;
            }
            //-------------------------------------------------------
            //Informacion de Acta de Aclaracion
            if (InformePreR != null)
            {
                ViewBag.InformeNumI = InformePreR.NumeroInformePR;
                ViewBag.InformeLugar = InformePreR.LugarInformePR;
                ViewBag.InformeFecha = InformePreR.FechaInformePR;
                ViewBag.InformeHora = InformePreR.Hora;
                ViewBag.InformeObra = Obra.ObjetoProcesoObra;
                ViewBag.ResolucionID = InformePreR.IdResolucionI;
                ViewBag.ActaAclaracionID = InformePreR.IdInformePR;
            }

            //-------------------------------------------------------
            //Informacion de Acta de Apertura
            if (AcApertura != null)
            {
                ViewBag.AperturaHora = AcApertura.HoraActaApertura;
                ViewBag.AperturaFecha = AcApertura.FechaActaApertura;
                ViewBag.AperturaNumOfer = AcApertura.NumeroOfertantes;
                ViewBag.ActaObra = Obra.ObjetoProcesoObra;
                ViewBag.ActaAperturaID = AcApertura.IdActaApertura;
            }
            //-------------------------------------------------------
            //Informacion de Acta de Calificación
            if (AcCalifica != null)
            {
                ViewBag.CalificacionHora = AcCalifica.HoraActaCalificacion;
                ViewBag.CalificacionFecha = AcCalifica.FechaActaCalificacion;
                ViewBag.CalificacionOferante = AcCalifica.OfertanteCalifica;
                ViewBag.CalificacionObra = Obra.ObjetoProcesoObra;
                ViewBag.ActaCalificacionID = AcCalifica.IdActaCalificacion;
            }


            return View(id > 0 ? ObrasP.ObtenerObraPublica(id) : ObrasP);

        }
    }
}