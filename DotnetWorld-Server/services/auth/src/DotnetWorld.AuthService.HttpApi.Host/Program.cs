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

        //��������� ����������� ��������� UserManager � RoleManager
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthServiceDbContext>().AddDefaultTokenProviders();

        //���������� � ����� JwtOptions ������ �� appsettings.json ("ApiSettings:JwtOptions")
        builder.Services.Configure<JwtOptions>(builder.Configuration
            .GetSection("ApiSettings:JwtOptions"));

        ConfigureServices(builder.Services);
        //builder.AddAppAuthetication();

        var app = builder.Build();

        var configuration = app.Services.GetRequiredService<IConfiguration>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        ConfigureEntityFrameworkCore(services);
        ConfigureApplicationServices(services);
        ConfigureAuthentication(services);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #region addswaggerWithAuth
        //    services.AddSwaggerGen(option =>
        //    {
        //        option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
        //        {
        //            Name = "Authorization",
        //            Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer"
        //        });
        //        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference= new OpenApiReference
        //            {
        //                Type=ReferenceType.SecurityScheme,
        //                Id=JwtBearerDefaults.AuthenticationScheme
        //            }
        //        }, new string[]{}
        //    }
        //});
        //    });
        #endregion
    }
    private static void ConfigureAuthentication(IServiceCollection services)
    {
    }
    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<UnitOfWork>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
    }
    private static void ConfigureEntityFrameworkCore(IServiceCollection services)
    {
    }
}