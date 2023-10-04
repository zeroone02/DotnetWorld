using DotnetWorld.AuthService.Application;
using DotnetWorld.AuthService.Application.Contracts;
using DotnetWorld.AuthService.Domain;
using DotnetWorld.DDD;
using eShop.AuthService.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<IEfCoreDbContext, AuthServiceDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        //Внедряем в DI Container UserManager и RoleManager
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthServiceDbContext>().AddDefaultTokenProviders();

        //Записываем в класс JwtOptions данные из appsettings.json ("ApiSettings:JwtOptions")
        builder.Services.Configure<JwtOptions>(builder.Configuration
            .GetSection("ApiSettings:JwtOptions"));

        ConfigureServices(builder.Services);

        var app = builder.Build();

        var configuration = app.Services.GetRequiredService<IConfiguration>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        ConfigureApplicationServices(services);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<UnitOfWork>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
    }
   
}