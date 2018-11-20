namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActaAdjudicacion")]
    public partial class ActaAdjudicacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActaAdjudicacion()
        {
            Ofertas = new HashSet<Ofertas>();
        }

        [Key]
        public int IdActaAdjudicacion { get; set; }

        [StringLength(200)]
        public string NumeroActaAdjudicacion { get; set; }

        public TimeSpan? HoraActaCalificacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaActaCalificacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaPublicacionPliegos { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaLimitePublicacionPliegos { get; set; }

        public int? CantidadOfertas { get; set; }

        [StringLength(100)]
        public string OfertanteCalifica { get; set; }

        public int? IdObraPublica { get; set; }

        public int? IdCertificadoP { get; set; }

        public int? IdActaCalificacion { get; set; }

        public virtual ActaCalificacionOfertas ActaCalificacionOfertas { get; set; }

        public virtual CertificacionPresupuestaria CertificacionPresupuestaria { get; set; }

        public virtual ObrasPublicas ObrasPublicas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ofertas> Ofertas { get; set; }
    }
}
