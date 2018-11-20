namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class ActaAperturaOfertas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActaAperturaOfertas()
        {
            Ofertantes = new HashSet<Ofertantes>();
        }

        [Key]
        public int IdActaApertura { get; set; }

        public TimeSpan? HoraActaApertura { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaActaApertura { get; set; }

        public int? NumeroOfertantes { get; set; }

        public int IdObraPublica { get; set; }

        public virtual ObrasPublicas ObrasPublicas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ofertantes> Ofertantes { get; set; }

        //Una resolucion de acuerdo a un id
        public ActaAperturaOfertas ObtenerActaApertura(int id)
        {
            var actaApertira = new ActaAperturaOfertas();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    actaApertira = context.ActaAperturaOfertas.Where((x => x.IdActaApertura == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return actaApertira;
        }

        //Una resolucion de acuerdo a un id de obra
        public ActaAperturaOfertas ObtenerActaApertura2(int id)
        {
            var actaApertira = new ActaAperturaOfertas();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    actaApertira = context.ActaAperturaOfertas.Where((x => x.IdObraPublica == id)).Single();
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                actaApertira = null;
            }
            return actaApertira;
        }

        // Ingresa ala BD un nuevo registro
        public int Guardar()
        {
            int Estado = 0;
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdActaApertura == 0)
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
                Estado = 5;
            }
            return Estado;
        }


        //FIN CLASS
    }
}
