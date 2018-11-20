namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Ofertantes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ofertantes()
        {
            Ofertas = new HashSet<Ofertas>();
        }

        [Key]
        public int IdOfertantes { get; set; }

        public int CodigoOfertantes { get; set; }

        [StringLength(100)]
        public string NombreOfertante { get; set; }

        [StringLength(300)]
        public string DescripconOferta { get; set; }

        public double? NumeroHojas { get; set; }

        public int IdActaApertura { get; set; }

        public virtual ActaAperturaOfertas ActaAperturaOfertas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ofertas> Ofertas { get; set; }

        //Una Pregunta con su respuesta deacuerdo a un id
        public Ofertantes ObtenerOfertante(int id)
        {
            var preres = new Ofertantes();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    preres = context.Ofertantes.Where((x => x.IdOfertantes == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return preres;
        }


        //Genera  una lista de ofertantes segun el acta de apertura
        public List<Ofertantes> Listar(int id = 0)
        {
            var Ofertantes = new List<Ofertantes>();
            try
            {
                using (var context = new TextContext())
                {
                    Ofertantes = context.Ofertantes.Where((x => x.IdActaApertura == id)).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Ofertantes;
        }

        // Ingresa a la BD un nuevo registro
        public int Guardar()
        {
            int Estado = 0;
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdOfertantes == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                        Estado = 1;
                    }
                    else
                    {
                        context.Entry(this).State = EntityState.Modified;
                        Estado = 2;
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

        // Elimina en la BD segun el ID
        public int Eliminar(int id)
        {
            int result = 0;
            try
            {
                using (var context = new TextContext())
                {
                    context.Entry(new Ofertantes { IdOfertantes = id }).State = EntityState.Deleted;
                    context.SaveChanges();
                    result = 4;
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                result = 5;
            }
            return result;
        }

        // Traer el ultimo codigo de los ofertantes segun el acta
        //public int UltimoCodigo(int id)
        //{
        //    int result = 0;
        //    try
        //    {
        //        using (var context = new TextContext())
        //        {
        //            //result = context.Ofertantes.Where((x => x.IdActaApertura == id)).Single();
        //            result = context.Ofertantes.Where(x => x.IdActaApertura == id).Select(x => x.CodigoOfertantes).fr.Max();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //throw new Exception(e.Message);
        //        result = 0;
        //    }
        //    return result;
        //}
        //Fin de class
    }
}
