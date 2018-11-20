namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class ActaCalificacionOfertas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActaCalificacionOfertas()
        {
            ActaAdjudicacion = new HashSet<ActaAdjudicacion>();
            Ofertas = new HashSet<Ofertas>();
        }

        [Key]
        public int IdActaCalificacion { get; set; }

        public TimeSpan? HoraActaCalificacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaActaCalificacion { get; set; }

        [StringLength(100)]
        public string OfertanteCalifica { get; set; }

        public int IdObraPublica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActaAdjudicacion> ActaAdjudicacion { get; set; }

        public virtual ObrasPublicas ObrasPublicas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ofertas> Ofertas { get; set; }

        // Ingresa a la BD un nuevo registro
        public void Guardar()
        {
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdActaCalificacion == 0)
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

        //Una Acta Calificación con un id
        public ActaCalificacionOfertas ObtenerActaCalificacion(int id)
        {
            var actaCalifi = new ActaCalificacionOfertas();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    actaCalifi = context.ActaCalificacionOfertas.Where((x => x.IdActaCalificacion == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return actaCalifi;
        }

        //Una acta de calificación a un id de obra
        public ActaCalificacionOfertas ObtenerActaCalificacion2(int id)
        {
            var actaCalifis = new ActaCalificacionOfertas();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    actaCalifis = context.ActaCalificacionOfertas.Where((x => x.IdObraPublica == id)).Single();
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                actaCalifis = null;
            }
            return actaCalifis;
        }

        //Fin de class
    }
}
