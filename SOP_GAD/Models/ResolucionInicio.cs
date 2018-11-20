namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("ResolucionInicio")]
    public partial class ResolucionInicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResolucionInicio()
        {
            InformePreguntaR = new HashSet<InformePreguntaR>();
        }

        [Key]
        public int IdResolucionI { get; set; }

        [StringLength(200)]
        public string NumeroResolucionI { get; set; }

        [StringLength(500)]
        public string Articulo { get; set; }

        [StringLength(100)]
        public string NombreAnalista { get; set; }

        public int IdObraPublica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformePreguntaR> InformePreguntaR { get; set; }

        public virtual ObrasPublicas ObrasPublicas { get; set; }

        //Una resolucion de acuerdo a un id
        public ResolucionInicio ObtenerResolucionInicial(int id)
        {
            var resolucioninicio = new ResolucionInicio();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    resolucioninicio = context.ResolucionInicio.Where((x => x.IdResolucionI == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return resolucioninicio;
        }

        //Una resolucion de acuerdo a un id de la obra
        public ResolucionInicio ObtenerResolucionInicial2(int id)
        {
            var resolucioninicio = new ResolucionInicio();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    resolucioninicio = context.ResolucionInicio.Where((x => x.IdObraPublica == id)).Single();
                }
            }
            catch (Exception e)
            {

                resolucioninicio = null;
                //throw new Exception(e.Message);

            }
            return resolucioninicio;
        }


        //Genera  una lista de todos los Resolicion
        public List<ResolucionInicio> ResolucionInicialListar()
        {
            var resolucion = new List<ResolucionInicio>();
            try
            {
                using (var context = new TextContext())
                {
                    resolucion = context.ResolucionInicio.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return resolucion;
        }

        // Ingresa ala BD un nuevo registro
        public void Guardar()
        {
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdResolucionI == 0)
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

        // fin de clase
    }
}
