using DAL.Implementation.Repositories;
using DAL.Repositories.Interface;
using ReportService.BLL.Helpers;
using ReportService.BLL.Report.Interface;
using ReportService.BLL.Services.Implementation;
using ReportService.BLL.Services.Interface;
using ReportService.Mappings;

namespace ReportService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        ConfigureService(builder.Services);
       
        
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseCors();
        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
    
    public static void ConfigureService(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=localhost;Port=5432;Username=postgres;Password=password;Database=postgres;";
        
        services.AddScoped<ICommitRepository>(_ =>  new CommitRepository(connectionString));
        services.AddScoped<IReportRepository>(_ => new ReportRepository(connectionString));
        services.AddScoped<IDefectToCommitRepository>(_ => new DefectToCommitRepository(connectionString));
        services.AddScoped<IStructuralElementToCommitRepository>(_ =>
            new StructuralElementToCommitRepository(connectionString));
        services.AddScoped<IDefectRepository>(p
            => new DefectRepository(connectionString, p.GetRequiredService<IDefectToCommitRepository>()));
        services.AddScoped<IStructuralElementRepository>(p
            => new StructuralElementRepository(connectionString, p.GetRequiredService<IStructuralElementToCommitRepository>()));
        services.AddScoped<IReportService, FileReportService>();
        services.AddScoped<IReportGenerator, PdfReportGenerator>();
        
        services.AddAutoMapper(
            typeof(MappingProfile).Assembly
        );
    }
}