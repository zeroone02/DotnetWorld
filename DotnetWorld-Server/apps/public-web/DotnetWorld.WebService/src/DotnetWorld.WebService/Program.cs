using DotnetWorld.Web.Application;
using DotnetWorld.Web.Application.Contracts;
using DotnetWorld.WebService.Application;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
        SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
        SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
        SD.CartAPIBase = builder.Configuration["ServiceUrls:CartAPI"];
        ConfigureServices(builder.Services);

        #region ПОЯСНЕНИЕ
        //Схема аутентификации позволяет выбирать определенный обработчик аутентификации.

        //Eсли используется схема "Bearer", то это значит,
        //что для аутентификации будет использоваться jwt-токен,
        //а в качестве обработчика аутентификации будет
        //применяться класс Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerHandler.Стоит отметить,
        //что для аутентификации с помощью jwt - токенов необходимо добавить в проект
        //через Nuget пакет Microsoft.AspNetCore.Authentication.JwtBearer

        //Например, для аутентификации с помощью куки передается схема "Cookies".
        //Соответственно для аутентификации пользователя будет выбираться встроенный обработчик
        //аутентификации -класс Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationHandler,
        //который на основе полученных в запросе cookie выполняет аутентификацию.
        #endregion
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        //взаимодействуют с HttpContext.User,
        //устанавливая текущего пользователя и применяя политики авторизации.

        //Для выполнения аутентификации этот компонент использует сервисы аутентификации,
        //в частности, сервис IAuthenticationService,
        //которые регистрируются в приложении с помощью метода AddAuthentication():
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        ConfigureApplicationServices(services);
    }
    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        //HttpClients
        services.AddHttpClient<IProductService, ProductService>();
        services.AddHttpClient<ICouponService, CouponService>();
        services.AddHttpClient<IAuthService, AuthService>();
        services.AddHttpClient<ICartService, CartService>();
        //Services
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<ICouponService, CouponService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<ITokenProvider, TokenProvider>();

    }
   
}