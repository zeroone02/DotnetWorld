using AutoMapper;
using DotnetWorld.ProductService.Application.Contracts;
using DotnetWorld.ProductService.Application;
using DotnetWorld.DDD;
using Microsoft.EntityFrameworkCore;
using DotnetWorld.ProductService.Domain;
using DotnetWorld.ProductService.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<IEfCoreDbContext, ProductServiceDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        ConfigureServices(builder.Services);
        builder.AddAppAuthetication();
        builder.Services.AddAuthorization();

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
        ConfigureEntityFrameworkCore(services);
        ConfigureApplicationServices(services);
        ConfigureAuthentication(services);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference= new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id=JwtBearerDefaults.AuthenticationScheme
                    }
                }, new string[]{}
            }
    });
        });
    }
    private static void ConfigureAuthentication(IServiceCollection services)
    {
    }
    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<UnitOfWork>();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DotnetWorldApplicationObjectMapper());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
    private static void ConfigureEntityFrameworkCore(IServiceCollection services)
    {
        services.AddTransient<IRepository<Product, Guid>, Repository<Product, Guid>>();
    }
}
