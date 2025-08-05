using ConvocatoriaApiServices.Models;
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
        public DbSet<Comision_Convocatoria> Comision_Convocatorias { get; set; }
        public DbSet<Tipo_DocumentoMinimo> Tipo_DocumentoMinimo { get; set; }
        public DbSet<FacultadPerfil> FacultadPerfil { get; set; }
        public DbSet<Inscripcion_DocumentoMinimo> Inscripcion_DocumentoMinimo { get; set; }
        public DbSet<Verificacion_HV> Verificacion_HV { get; set; }
        public DbSet<CompentenciasAcademicas> CompentenciasAcademicas { get; set; }
        public DbSet<Evaluacion_Competencia> Evaluacion_Competencia { get; set; }
        public DbSet<Detalle_Evaluacion> Detalle_Evaluacion { get; set; }
        public DbSet<Evaluacion_Competencia_Consolidado> Evaluacion_Competencia_Consolidado { get; set; }

        public DbSet<SubTipo_Documento> SubtipoDocumento { get; set; }
        // Otros DbSets...

        private readonly ILogger<ApplicationDbContext> _logger;



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
            ChangeTracker.LazyLoadingEnabled = true;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura la cadena de conexión a tu base de datos PostgreSQL
            try
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Ef_Postgres_Db"));
            }
            catch(Exception e)
            {
                _logger.LogError("OnConfiguring" + e.Message);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Tipo_Identificacion>()
                    .HasOne(ti => ti.Participante)
                    .WithOne(p => p.TipoIdentificacion)
                    .HasForeignKey<Participante>(i => i.tipo_identificacion);

                modelBuilder.Entity<Tipo_DocumentoMinimo>()
                    .HasOne(ti => ti.Inscripcion_DocumentoMinimo)
                    .WithOne(p => p.Tipo_DocumentoMinimo)
                    .HasForeignKey<Inscripcion_DocumentoMinimo>(i => i.codigo_documento);

                modelBuilder.Entity<Inscripcion_Convocatoria>()
                    .HasOne(ti => ti.Inscripcion_DocumentoMinimo)
                    .WithOne(p => p.Inscripcion_Convocatoria)
                    .HasForeignKey<Inscripcion_DocumentoMinimo>(i => i.codigo_inscripcion);

                /*modelBuilder.Entity<Tipo_Documento>()
                    .HasOne(td => td.Documento)
                    .WithMany(d => d.TipoDocumento)
                    .HasForeignKey(dc => dc.codigo);

                modelBuilder.Entity<Documento>()
                    .HasMany(d => d.TipoDocumento)
                    .WithOne(t => t.Documento)
                    .HasForeignKey(t => t.codigo);*/

             

                modelBuilder.Entity<SubTipo_Documento>()
                  .HasOne(d => d.TipoDocumento)
                  .WithMany(t => t.Subtipos)
                  .HasForeignKey(d => d.codigo_tipo_documento);

                modelBuilder.Entity<Documento>()
                   .HasOne(d => d.TipoDocumento)
                   .WithMany(t => t.Documentos)
                   .HasForeignKey(d => d.tipo_documento);

                modelBuilder.Entity<Detalle_Evaluacion>()
                   .HasOne(d => d.Evaluacion_Competencia)
                   .WithMany(t => t.Detalle_Evaluacion)
                   .HasForeignKey(d => d.id_evaluacionCompetencia);

                modelBuilder.Entity<Detalle_Evaluacion>()
                  .HasOne(d => d.CompentenciasAcademicas)
                  .WithMany(t => t.Detalle_Evaluacion)
                  .HasForeignKey(d => d.id_competencia);


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

                modelBuilder.Entity<Inscripcion_Convocatoria>()
                   .HasOne(ic => ic.Verificacion_HV)
                   .WithOne(v => v.Inscripcion_Convocatoria)
                   .HasForeignKey<Verificacion_HV>(vh => vh.codigoinscripcion);
            }
            catch (Exception ex) {

                _logger.LogError("OnModelCreating: " + ex.Message);
            }
        }
    }
}
