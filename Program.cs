using ConvocatoriaServices.Context.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ConvocatoriaApiServices.Services;
using ConvocatoriaApiServices.Services.Interfaces;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Serilog.Formatting.Compact;

public class Program
{
    public static void Main(string[] args)
    {
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(new RenderedCompactJsonFormatter(), "logs/log.txt")
                .CreateLogger();
        try
        {
            Log.Information("Starting application");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application failed to start");
        }
        finally
        {
            Log.CloseAndFlush();
        }

        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.    
        builder.Services.AddControllers();
        builder.Services.AddScoped<IConvocatoriaService, ConvocatoriaService>();
        builder.Services.AddScoped<IComisionConvocatoriaService, ComisionConvocatoriaService>();
        builder.Services.AddScoped<IInscripcionService, InscripicionService>();
        builder.Services.AddScoped<IDocumentoService, DocumentoService>();
        builder.Services.AddScoped<IListasTiposBasicosService, ListasTiposBasicosService>();
        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddCors(options => {
            options.AddPolicy(MyAllowSpecificOrigins,
            builder => builder.WithOrigins("*")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                );
        });
        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        

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
        app.UseCors(MyAllowSpecificOrigins);

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

    public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.ClearProviders();
                     logging.AddSerilog(dispose: true);
                 })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     // No necesitas configurar nada aquí, ya que tu aplicación es minimalista
                 });


}