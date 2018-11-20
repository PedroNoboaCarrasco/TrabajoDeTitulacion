namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Barrio")]
    public partial class Barrio
    {
        [Key]
        public int IdBarrio { get; set; }

        [StringLength(50)]
        public string NombreBarrio { get; set; }

        public double? PoblacionBarrio { get; set; }

        [StringLength(50)]
        public string ComunidadBarrio { get; set; }

        //Listar Barrios

        public IEnumerable<Barrio> ListarBarrios(string Asentamiento)
        {
            var barrio = new List<Barrio>();
            try
            {
                using (var context = new TextContext())
                {
                    barrio = context.Barrio.Where(x => x.ComunidadBarrio.Equals(Asentamiento)).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return barrio;
        }
        // Genera una lista por comunidad
        public List<Barrio> ListarBarrio(string Asentamiento)
        {
            var barrio = new List<Barrio>();
            try
            {
                using (var context = new TextContext())
                {
                    barrio = context.Barrio.Where(x => x.ComunidadBarrio.Equals(Asentamiento)).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return barrio;
        }

        //Genera  una lista de todos los barrios
        public List<Barrio> Listar()
        {
            var barrio = new List<Barrio>();
            try
            {
                using (var context = new TextContext())
                {
                    barrio = context.Barrio.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return barrio;
        }

        //Un barrio deacuerdo a un id
        public Barrio ObtenerBarrio(int id)
        {
            var barrio = new Barrio();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    barrio = context.Barrio.Where((x => x.IdBarrio == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return barrio;
        }

        // Ingresa ala BD un nuevo registro
        public void Guardar()
        {
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdBarrio == 0)
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

        // Elimina en la BD segun el ID
        public int Eliminar(int id)
        {
            int result = 0;
            try
            {
                using (var context = new TextContext())
                {
                    context.Entry(new Barrio { IdBarrio = id }).State = EntityState.Deleted;
                    context.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                result = 2;
            }
            return result;
        }

        //Buscar con inerjoin
        public Barrio BuscarBarrio(int id)
        {
            var barrio = new Barrio();
            try
            {
                using (var context = new TextContext())
                {
                    //inerjoin entre dos tablas ..Include reemplaza sql                
                    barrio = context.Barrio
                             .Include("Usuarios")
                             .Where(x => x.IdBarrio == id).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return barrio;
        }

    }
}
