using ConvocatoriaServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace ConvocatoriaServices.Context.Application
{
    
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Convocatoria> Convocatorias { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Inscripcion_Convocatoria> Inscripcion_Convocatorias { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Tipo_Documento> Tipo_Documentos { get; set; }
        public DbSet<Tipo_Identificacion> Tipo_Identificaciones { get; set; }
        // Otros DbSets...

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura la cadena de conexión a tu base de datos PostgreSQL
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Ef_Postgres_Db"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tipo_Identificacion>()
                .HasOne(ti => ti.Participante)
                .WithOne(p => p.TipoIdentificacion)
                .HasForeignKey<Participante>(i => i.tipo_identificacion);

            modelBuilder.Entity<Tipo_Documento>()
                .HasOne(td => td.Documento)
                .WithOne(d => d.Tipo_Documento)
                .HasForeignKey<Documento>(dc => dc.tipo_documento);

            modelBuilder.Entity<Inscripcion_Convocatoria>()
                .HasOne(ic => ic.Documento)
                .WithOne(d => d.Inscripcion_Convocatoria)
                .HasForeignKey<Documento>(dc => dc.codigo_inscripcion);

            modelBuilder.Entity<Convocatoria>()
               .HasOne(c => c.Inscripcion_Convocatoria)
               .WithOne(ic => ic.Convocatoria)
               .HasForeignKey<Inscripcion_Convocatoria>(i => i.codigo_convocatoria);

            modelBuilder.Entity<Participante>()
               .HasOne(p => p.Inscripcion_Convocatoria)
               .WithOne(ic => ic.Participante)
               .HasForeignKey<Inscripcion_Convocatoria>(i => i.identificacion_participante);

            modelBuilder.Entity<Perfil>()
               .HasOne(p => p.Inscripcion_Convocatoria)
               .WithOne(ic => ic.Perfil)
               .HasForeignKey<Inscripcion_Convocatoria>(i => i.codigo_perfil);
        }
    }
}
