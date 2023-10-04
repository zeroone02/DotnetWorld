
using DotnetWorld.WebService.Application;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
        SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

        ConfigureServices(builder.Services);



        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        ConfigureApplicationServices(services);
        ConfigureAuthentication(services);
    }
    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        //HttpClients
        services.AddHttpClient<ICouponService, CouponService>();
        //Services
        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<ICouponService, CouponService>();

    }
    private static void ConfigureAuthentication(IServiceCollection services)
    {
    }
}