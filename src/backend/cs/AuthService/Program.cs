using System.Text;
using AuthService.DI;
using AuthService.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Unity;

namespace AuthService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCors(options =>  
        {  
            options.AddDefaultPolicy(
                policy  =>
                {
                    policy
                        .AllowAnyOrigin()
                        //.WithOrigins("http://localhost:80", "http://frontend:80", "http://localhost:3000", "http://frontend:3000", "http://webapi:8002")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });  
        });
        
        var container = new UnityContainer();
                        
        // Регистрация зависимостей в Unity
        UnityConfig.ConfigureServices(builder.Services);
                        
        // Использование UnityServiceProvider для интеграции с ASP.NET Core DI
        //builder.Services.AddSingleton<IServiceProvider>(new UnityServiceProviderFactory(container));
        
        builder.Services.AddAutoMapper(
            typeof(MappingProfile).Assembly, 
            typeof(AuthService.BLL.Mappings.MappingProfile).Assembly
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
}