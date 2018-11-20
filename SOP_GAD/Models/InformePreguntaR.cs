namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("InformePreguntaR")]
    public partial class InformePreguntaR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public InformePreguntaR()
        {
            PreguntaRespuesta = new HashSet<PreguntaRespuesta>();
        }

        [Key]
        public int IdInformePR { get; set; }

        [StringLength(200)]
        public string NumeroInformePR { get; set; }

        [StringLength(100)]
        public string LugarInformePR { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInformePR { get; set; }

        public TimeSpan? Hora { get; set; }

        public int IdObraPublica { get; set; }

        public int? IdResolucionI { get; set; }

        public virtual ObrasPublicas ObrasPublicas { get; set; }

        public virtual ResolucionInicio ResolucionInicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreguntaRespuesta> PreguntaRespuesta { get; set; }

        //Un Informe de alcaraciones de preguntas deacuerdo a su id
        public InformePreguntaR ObtenerAclaracion(int id)
        {
            var informe = new InformePreguntaR();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    informe = context.InformePreguntaR.Where((x => x.IdInformePR== id)).Single();
                }
            }
            catch (Exception e)
            {
                informe = null;
                //throw new Exception(e.Message);
            }
            return informe;
        }

        //Un Informe de alcaraciones de preguntas deacuerdo a su id de obra
        public InformePreguntaR ObtenerAclaracion2(int id = 0)
        {
            var informe = new InformePreguntaR();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    informe = context.InformePreguntaR.Where((x => x.IdObraPublica == id)).Single();
                }
            }
            catch (Exception e)
            {
                informe = null;
            }
            return informe;
        }

        // Ingresa a la BD un nuevo registro
        public int Guardar()
        {
            int Estado = 0;
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdInformePR == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                        Estado = 6;
                    }
                    else
                    {
                        context.Entry(this).State = EntityState.Modified;
                        Estado = 7;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                Estado = 3;
            }
            return Estado;
        }


        //Fin de clase
    }
}
