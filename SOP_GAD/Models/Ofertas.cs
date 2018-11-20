namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Ofertas
    {
        [Key]
        public int IdOferta { get; set; }

        public int? Numero { get; set; }

        [StringLength(100)]
        public string Parametro { get; set; }

        public int Cumple { get; set; }

        [StringLength(2000)]
        public string Fundamento { get; set; }

        public int? IdOfertante { get; set; }

        public int? IdActaCalificacion { get; set; }

        public int? IdActaAdjudicacion { get; set; }

        public virtual ActaAdjudicacion ActaAdjudicacion { get; set; }

        public virtual ActaCalificacionOfertas ActaCalificacionOfertas { get; set; }

        public virtual Ofertantes Ofertantes { get; set; }


        //Genera  la oferta
        public List<Ofertas> Listar(int id=0, int actaId=0)
        {
            var ofert = new List<Ofertas>();
            try
            {
                using (var context = new TextContext())
                {
                    ofert = context.Ofertas.Where(x => x.IdOfertante == id).Where(x => x.IdActaCalificacion == actaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ofert;
        }

        //Genera  numero de ofeta
        public int NumeroOfer(int id = 0)
        {
            var ofert = new List<Ofertas>();
            int numer = 0;
            try
            {
                using (var context = new TextContext())
                {
                    ofert = context.Ofertas.Where((x => x.IdOfertante == id )).ToList();
                }
                numer = ofert.Count();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return numer;
        }

        //Un oferta deacuerdo a un id
        public Ofertas ObtenerOferta(int id)
        {
            var ofert = new Ofertas();
            try
            {
                using (var context = new TextContext())
                {
                    ofert = context.Ofertas.Where((x => x.IdOferta == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ofert;
        }

        // Ingresa a la BD un nuevo registro
        public int Guardar()
        {
            int Estado = 0;
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdOferta == 0)
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


        //Fin class
    }
}
