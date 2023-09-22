namespace DotnetWorld.WebService.Domain;
/// <summary>
/// SD - STATIC DATA
/// СТАТИЧЕСКИЕ ДАННЫЕ
/// </summary>
public class SD
{
    public static string CouponAPIBase { get; set; }
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE,
    }
}
