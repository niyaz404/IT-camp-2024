using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Unity.Microsoft.DependencyInjection;
using WebApi.Mappings;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>  
        {  
            options.AddDefaultPolicy(
                policy  =>
                {
                    policy
                        .WithOrigins("http://localhost:80", "http://frontend:80", "http://localhost:3000", "http://frontend:3000", "http://webapi:8002")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });  
        });  

        builder.Services.AddAutoMapper(
            typeof(MappingProfile).Assembly, 
            typeof(WebApi.BLL.Mappings.MappingProfile).Assembly
        );
        
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"))
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
        builder.Services.AddTransient<MyCustomService>(); 

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
    
    public class MyCustomService
    {
        private readonly HttpClient _httpClient;

        public MyCustomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDataAsync(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            return response;
        }
    }
}