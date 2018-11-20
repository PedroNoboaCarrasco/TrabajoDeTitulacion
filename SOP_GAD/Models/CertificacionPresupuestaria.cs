namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CertificacionPresupuestaria")]
    public partial class CertificacionPresupuestaria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CertificacionPresupuestaria()
        {
            ActaAdjudicacion = new HashSet<ActaAdjudicacion>();
        }

        [Key]
        public int IdCertificadoP { get; set; }

        [StringLength(200)]
        public string NumeroCertificadoP { get; set; }

        public double? PartidaPresupuestaria { get; set; }

        [StringLength(200)]
        public string DescripcionCertificadoP { get; set; }

        public double? ValorReferencial { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaCertificadoP { get; set; }

      
        public int IdObraPublica { get; set; }

        //public int Existe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActaAdjudicacion> ActaAdjudicacion { get; set; }

        public virtual ObrasPublicas ObrasPublicas { get; set; }

        //Un Certificacion Presupuestaria segun el id 
        public CertificacionPresupuestaria ObtenerCertificacionP(int id=0)
        {
            var certificacionp = new CertificacionPresupuestaria();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    certificacionp = context.CertificacionPresupuestaria.Where((x => x.IdCertificadoP== id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return certificacionp;
        }

        //Un Certificacion Presupuestaria segun el id de la Obra
        public CertificacionPresupuestaria ObtenerCertificacionP2(int id = 0)
        {
            var certificacionp = new CertificacionPresupuestaria();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    certificacionp = context.CertificacionPresupuestaria.Where((x => x.IdObraPublica == id)).Single();
                    //certificacionp.Existe = 1;
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                certificacionp = null;
            }
            return certificacionp;
        }

        // Ingresa ala BD un nuevo registro
        public void Guardar()
        {
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdCertificadoP == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }
                    else
                    {
                        context.Entry(this).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //Una Certificación Presupuestaria deacuerdo a un id
        public CertificacionPresupuestaria ObtenerCertificadoPresupuesto(int id)
        {
            var certificacionP = new CertificacionPresupuestaria();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    certificacionP = context.CertificacionPresupuestaria.Where((x => x.IdCertificadoP == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return certificacionP;
        }

        //FIN DE CLASSE
    }
}
