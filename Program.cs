using ConvocatoriaServices.Context.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ConvocatoriaApiServices.Services;
using ConvocatoriaApiServices.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<IConvocatoriaService, ConvocatoriaService>();
        builder.Services.AddScoped<IInscripcionService, InscripicionService>();
        builder.Services.AddScoped<IDocumentoService, DocumentoService>();
        builder.Services.AddScoped<IListasTiposBasicosService, ListasTiposBasicosService>();
        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });


        var app = builder.Build();
        ApplyMigrations(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

       
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors();

        app.MapControllers();

        app.Run();
    }

    private static void ApplyMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            // Aplica las migraciones pendientes
            dbContext.Database.Migrate();
        }
    }
}