using System.Text;
using AuthService.BLL.Services.Implementation;
using AuthService.BLL.Services.Interface;
using AuthService.DAL.Repositories.Implementation;
using AuthService.DAL.Repositories.Interface;
using AuthService.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
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

    public static void ConfigureService(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               "Host=localhost;Port=5433;Username=auth;Password=password;Database=auth;";
        
        services.AddScoped<IUserRepository>(_ =>  new UserRepository(connectionString));
        services.AddScoped<IUserToRoleRepository>(_ => new UserToToRoleRepository(connectionString));
        services.AddScoped<IUserService, UserService>();
        
        services.AddAutoMapper(
            typeof(MappingProfile).Assembly, 
            typeof(AuthService.BLL.Mappings.MappingProfile).Assembly
        );
    }
}