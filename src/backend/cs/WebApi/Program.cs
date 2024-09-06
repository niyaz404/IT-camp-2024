using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.BLL.Services.Implementation.Auth;
using WebApi.BLL.Services.Implementation.Commit;
using WebApi.BLL.Services.Implementation.Magnetogram;
using WebApi.BLL.Services.Implementation.Report;
using WebApi.BLL.Services.Interface.Auth;
using WebApi.BLL.Services.Interface.Commit;
using WebApi.BLL.Services.Interface.Magnetogram;
using WebApi.BLL.Services.Interface.Report;
using WebApi.DAL.Providers.Implementation;
using WebApi.DAL.Providers.Interface;
using WebApi.Mappings;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var hosts = new[]
            { "http://localhost:3000", "http://frontend:3000", "http://localhost:8081", "http://frontend:8081" };
        builder.Services.AddCors(options =>  
        {  
            options.AddDefaultPolicy(
                policy  =>
                {
                    policy
                        .WithOrigins(hosts)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });  
        });

        ConfigureService(builder.Services);
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "AuthService",
                    ValidAudience = "WebAPI",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("itcamp_secretkey"))
                };
            });

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
        
        builder.Services.AddHttpClient(); 

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseCors();

        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            await next();

            if (context.Request.Path == "/swagger/v1/swagger.json")
            {
                context.Response.Body.Position = 0; // Важно: сбросить позицию потока перед чтением
                using var reader = new StreamReader(context.Response.Body);
                var swaggerJson = await reader.ReadToEndAsync();
                await File.WriteAllTextAsync("swagger.json", swaggerJson);
            }
        });

        app.MapControllers();
        
        app.Run();
    }

    public static void ConfigureService(IServiceCollection services)
    {
        bool isRunningInDocker = Environment.GetEnvironmentVariable("IS_DOCKER_CONTAINER") == "true";
        var authServiceUrl = Environment.GetEnvironmentVariable("AUTH_SERVICE_URL") ?? "http://localhost:5000";
        var reportServiceUrl = Environment.GetEnvironmentVariable("REPORT_SERVICE_URL") ?? "http://localhost:5001";
        var mlServiceUrl = Environment.GetEnvironmentVariable("ML_SERVICE_URL") ?? "http://localhost:8080";

        services.AddScoped(_ =>  new HttpClient());
        services.AddScoped<IAuthProvider>(p => new AuthProvider(authServiceUrl, p.GetRequiredService<HttpClient>()));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReportProvider>(p => new ReportProvider(reportServiceUrl, p.GetRequiredService<HttpClient>()));
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<ICommitService, CommitService>();
        services.AddScoped<IMagnetogramService, MagnetogramService>();
        services.AddScoped<IProcessingProvider>(p => new ProcessingProvider(mlServiceUrl, p.GetRequiredService<HttpClient>()));
        
        services.AddAutoMapper(
            typeof(MappingProfile).Assembly,
            typeof(WebApi.BLL.Mappings.MappingProfile).Assembly
        );
    }
}