namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class ObrasPublicas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObrasPublicas()
        {
            ActaAdjudicacion = new HashSet<ActaAdjudicacion>();
            ActaAperturaOfertas = new HashSet<ActaAperturaOfertas>();
            ActaCalificacionOfertas = new HashSet<ActaCalificacionOfertas>();
            CertificacionPresupuestaria = new HashSet<CertificacionPresupuestaria>();
            InformePreguntaR = new HashSet<InformePreguntaR>();
            ResolucionInicio = new HashSet<ResolucionInicio>();
        }

        [Key]
        public int IdObraPublica { get; set; }

        [StringLength(200)]
        public string EntidadObra { get; set; }

        [StringLength(200)]
        public string ObjetoProcesoObra { get; set; }

        [StringLength(200)]
        public string CodigoProcesoObra { get; set; }

        [StringLength(100)]
        public string TipoCompraObra { get; set; }

        public double PresupuestoObra { get; set; }

        [StringLength(100)]
        public string TipoContratoObra { get; set; }

        public double? Anticipo { get; set; }

        public double? Saldo { get; set; }

        [StringLength(100)]
        public string TipoAdjudicacionObra { get; set; }

        public double? PlazoEntrega { get; set; }

        public double? VigenciaOferta { get; set; }

        [StringLength(200)]
        public string FuncionarioEncargado { get; set; }

        [StringLength(100)]
        public string EstadoProceso { get; set; }

        [StringLength(100)]
        public string Estadofinalizacion { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public int? IdUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActaAdjudicacion> ActaAdjudicacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActaAperturaOfertas> ActaAperturaOfertas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActaCalificacionOfertas> ActaCalificacionOfertas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertificacionPresupuestaria> CertificacionPresupuestaria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformePreguntaR> InformePreguntaR { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResolucionInicio> ResolucionInicio { get; set; }

        //Funciones ----------------------------------------------

        //Genera  una lista de todos los barrios
        public List<ObrasPublicas> Listar()
        {
            var obrasPu = new List<ObrasPublicas>();
            try
            {
                using (var context = new TextContext())
                {
                    obrasPu = context.ObrasPublicas.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return obrasPu;
        }

        //Una obra pública deacuerdo a un id
        public ObrasPublicas ObtenerObraPublica(int id)
        {
            var obrap = new ObrasPublicas();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones 
                    obrap = context.ObrasPublicas.Where((x => x.IdObraPublica == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return obrap;
        }

        // Ingresa a la BD un nuevo registro
        public int Guardar()
        {
            int result = 0;
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdObraPublica == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                        result = 1;
                    }
                    else
                    {
                        context.Entry(this).State = EntityState.Modified;
                        result = 2;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                result = 3;
            }
            return result;
        }

        // Elimina en la BD segun el ID
        public int Eliminar(int id)
        {
            int result = 0;
            CertificacionPresupuestaria objCP = new Models.CertificacionPresupuestaria();

            if (null == objCP.ObtenerCertificacionP2(id))
            {
                try
                {
                    using (var context = new TextContext())
                    {
                        context.Entry(new ObrasPublicas { IdObraPublica = id }).State = EntityState.Deleted;
                        context.SaveChanges();
                        result = 1;
                    }
                }
                catch (Exception e)
                {
                    //throw new Exception(e.Message);
                    result = 2;
                }
            }
            else { result = 3; }

            return result;
        }

        //Fin funciones ------------------------------------------

    }
}
