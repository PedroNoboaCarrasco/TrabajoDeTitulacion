namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("PreguntaRespuesta")]
    public partial class PreguntaRespuesta
    {
        [Key]
        public int IdPreguntaRespuesta { get; set; }

        public int? TipoPreguntaRespuesta { get; set; }

        [StringLength(200)]
        public string Pregunta { get; set; }

        [StringLength(200)]
        public string Respuesta { get; set; }

        public int? IdInformePR { get; set; }

        public virtual InformePreguntaR InformePreguntaR { get; set; }

        //Una Pregunta con su respuesta deacuerdo a un id
        public PreguntaRespuesta ObtenerPreguntaRes(int id)
        {
            var preres = new PreguntaRespuesta();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    preres = context.PreguntaRespuesta.Where((x =>x.IdPreguntaRespuesta == id)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return preres;
        }

        //Genera  una lista de prguntas con sus respuestassegun el informe
        public List<PreguntaRespuesta> Listar(int id = 0)
        {
            var Preguntas = new List<PreguntaRespuesta>();
            try
            {
                using (var context = new TextContext())
                {
                    Preguntas = context.PreguntaRespuesta.Where((x => x.IdInformePR == id)).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Preguntas;
        }


        // Ingresa a la BD un nuevo registro
        public int Guardar()
        {
            int Estado = 0;
            try
            {
                using (var context = new TextContext())
                {
                    if (this.IdPreguntaRespuesta == 0)
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
                    context.Entry(new PreguntaRespuesta { IdPreguntaRespuesta = id }).State = EntityState.Deleted;
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
        //Fin clase
    }
}
