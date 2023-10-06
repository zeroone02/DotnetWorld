using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static DotnetWorld.WebService.Domain.SD;

namespace DotnetWorld.WebService.Application;
/// <summary>
/// Общая цель класса HttpClientService - предоставить базовую функциональность для отправки HTTP-запросов
/// и обработки ответов.
/// </summary>
/// <param name="requestDto"></param>
/// <returns></returns>
public class HttpClientService : IHttpClientService
{
    //используется для создания экземпляра HttpClient.
    private readonly IHttpClientFactory _httpClientFactory;
    public HttpClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        {
            //создается экземпляр HttpClient с использованием _httpClientFactory.CreateClient("DotnetWorld").
            //Это позволяет получить экземпляр HttpClient из фабрики, используя имя "DotnetWorld".
            HttpClient client = _httpClientFactory.CreateClient("DotnetWorld");
            HttpRequestMessage message = new();
            //Обозначаем формат данных ( в данном случае "application/json")
            message.Headers.Add("Accept", "application/json");
            //todo token
            message.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.
                    SerializeObject(requestDto.Data),
                    Encoding.UTF8, "application/json");
            }

            switch (requestDto.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            HttpResponseMessage? apiResponse = await client.SendAsync(message);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    //Метод ReadAsStringAsync преобразует содержимое ответа в строку.
                    //если сервер возвращает данные в формате JSON,
                    //мы можем использовать этот метод для получения JSON-строки из ответа и затем десериализовать ее в объекты в коде.
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (Exception ex)
        {
            var responseDto = new ResponseDto
            {
                Message = ex.Message.ToString(),
                IsSuccess = false
            };
            return responseDto;
        }
    }
}
