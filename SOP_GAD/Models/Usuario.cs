namespace SOP_GAD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            ObrasPublicas = new HashSet<ObrasPublicas>();
        }

        [Key]
        public int IdUsuario { get; set; }

        [StringLength(50)]
        public string ContraseniaUsuario { get; set; }

        [StringLength(50)]
        public string CedulaUsuario { get; set; }

        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [StringLength(50)]
        public string ApellidoUsuario { get; set; }

        [StringLength(50)]
        public string CargoUsuario { get; set; }

        public int EstadoUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObrasPublicas> ObrasPublicas { get; set; }

        //Una resolucion de acuerdo a su ocupación
        public Usuario ObtenerUsuario(string ocupacion)
        {
            var usuario = new Usuario();
            try
            {
                using (var context = new TextContext())
                {
                    usuario = (from x in context.Usuario
                               where x.CargoUsuario == ocupacion && x.EstadoUsuario == 1
                               select x).Single();
                    //x es para llamar un atributo de la clase
                    //ver como poner 2 opciones
                    //usuario = context.Usuario.Where(x => x.CargoUsuario == ocupacion).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return usuario;
        }

        public Usuario ObtenerUsuario2(int id = 0)
        {
            var usuario = new Usuario();
            try
            {
                using (var context = new TextContext())
                {
                    usuario = (from x in context.Usuario
                               where x.IdUsuario == id && x.EstadoUsuario == 1
                               select x).Single();
                    //usuario = context.Usuario.Where(x => x.IdUsuario == id).Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return usuario;
        }

        //Fin de class
    }
}
