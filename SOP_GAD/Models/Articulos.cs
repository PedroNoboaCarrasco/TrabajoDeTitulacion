
namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Articulos")]
    public partial class Articulos
    {
        [Key]

        public int IdArticulo { get; set; }

        public int NumeroArt { get; set; }

        [StringLength(500)]
        public string DetalleArt { get; set; }

        [StringLength(100)]
        public string TipoContratoObra { get; set; }

        //Una Articulo de acuerdo a su Numero
        public Articulos ObtenerArticulo(int Num=0)
        {
            var articulo = new Articulos();
            try
            {
                using (var context = new TextContext())
                {
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones 
                    articulo = context.Articulos.Where((x => x.NumeroArt == Num)).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return articulo;
        }
    }
       
}