namespace SOP_GAD.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TextContext : DbContext
    {
        public TextContext()
            : base("name=TextContext")
        {
        }

        public virtual DbSet<ActaAdjudicacion> ActaAdjudicacion { get; set; }
        public virtual DbSet<ActaAperturaOfertas> ActaAperturaOfertas { get; set; }
        public virtual DbSet<ActaCalificacionOfertas> ActaCalificacionOfertas { get; set; }
        public virtual DbSet<Barrio> Barrio { get; set; }
        public virtual DbSet<CertificacionPresupuestaria> CertificacionPresupuestaria { get; set; }
        public virtual DbSet<InformePreguntaR> InformePreguntaR { get; set; }
        public virtual DbSet<ObrasPublicas> ObrasPublicas { get; set; }
        public virtual DbSet<Ofertantes> Ofertantes { get; set; }
        public virtual DbSet<Ofertas> Ofertas { get; set; }
        public virtual DbSet<PreguntaRespuesta> PreguntaRespuesta { get; set; }
        public virtual DbSet<ResolucionInicio> ResolucionInicio { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<Articulos> Articulos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActaAdjudicacion>()
                .Property(e => e.NumeroActaAdjudicacion)
                .IsUnicode(false);

            modelBuilder.Entity<ActaAdjudicacion>()
                .Property(e => e.OfertanteCalifica)
                .IsUnicode(false);

            modelBuilder.Entity<ActaCalificacionOfertas>()
                .Property(e => e.OfertanteCalifica)
                .IsUnicode(false);

            modelBuilder.Entity<Barrio>()
                .Property(e => e.NombreBarrio)
                .IsUnicode(false);

            modelBuilder.Entity<Barrio>()
                .Property(e => e.ComunidadBarrio)
                .IsUnicode(false);

            modelBuilder.Entity<CertificacionPresupuestaria>()
                .Property(e => e.NumeroCertificadoP)
                .IsUnicode(false);

            modelBuilder.Entity<CertificacionPresupuestaria>()
                .Property(e => e.DescripcionCertificadoP)
                .IsUnicode(false);

            modelBuilder.Entity<InformePreguntaR>()
                .Property(e => e.NumeroInformePR)
                .IsUnicode(false);

            modelBuilder.Entity<InformePreguntaR>()
                .Property(e => e.LugarInformePR)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.EntidadObra)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.ObjetoProcesoObra)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.CodigoProcesoObra)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.TipoCompraObra)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.TipoContratoObra)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.TipoAdjudicacionObra)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.FuncionarioEncargado)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.EstadoProceso)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.Estadofinalizacion)
                .IsUnicode(false);

            modelBuilder.Entity<ObrasPublicas>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Ofertantes>()
                .Property(e => e.NombreOfertante)
                .IsUnicode(false);

            modelBuilder.Entity<Ofertantes>()
                .Property(e => e.DescripconOferta)
                .IsUnicode(false);

            modelBuilder.Entity<Ofertantes>()
                .HasMany(e => e.Ofertas)
                .WithOptional(e => e.Ofertantes)
                .HasForeignKey(e => e.IdOfertante);

            modelBuilder.Entity<Ofertas>()
                .Property(e => e.Parametro)
                .IsUnicode(false);

            modelBuilder.Entity<Ofertas>()
                .Property(e => e.Fundamento)
                .IsUnicode(false);

            modelBuilder.Entity<PreguntaRespuesta>()
                .Property(e => e.Pregunta)
                .IsUnicode(false);

            modelBuilder.Entity<PreguntaRespuesta>()
                .Property(e => e.Respuesta)
                .IsUnicode(false);

            modelBuilder.Entity<ResolucionInicio>()
                .Property(e => e.NumeroResolucionI)
                .IsUnicode(false);

            modelBuilder.Entity<ResolucionInicio>()
                .Property(e => e.Articulo)
                .IsUnicode(false);

            modelBuilder.Entity<ResolucionInicio>()
                .Property(e => e.NombreAnalista)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.ApellidoUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.CargoUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Articulos>()
                .Property(e => e.DetalleArt)
                .IsUnicode(false);
            modelBuilder.Entity<Articulos>()
                .Property(e => e.TipoContratoObra)
                .IsUnicode(false);
        }
    }
}
