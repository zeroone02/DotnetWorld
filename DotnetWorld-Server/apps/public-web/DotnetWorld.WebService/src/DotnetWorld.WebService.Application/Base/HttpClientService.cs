﻿using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
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
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;
    public HttpClientService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("eShop");
            HttpRequestMessage message = new();
            //if (requestDto.ContentType == ContentType.MultipartFormData)
            //{
            //    message.Headers.Add("Accept", "*/*");
            //}
            //else
            //{
                message.Headers.Add("Accept", "application/json");
            //}
            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            message.RequestUri = new Uri(requestDto.Url);

            //if (requestDto.ContentType == ContentType.MultipartFormData)
            //{
            //    var content = new MultipartFormDataContent();

            //    foreach (var prop in requestDto.Data.GetType().GetProperties())
            //    {
            //        var value = prop.GetValue(requestDto.Data);
            //        if (value is FormFile)
            //        {
            //            var file = (FormFile)value;
            //            if (file != null)
            //            {
            //                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
            //            }
            //        }
            //        else
            //        {
            //            content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
            //        }
            //    }
            //    message.Content = content;
            //}
            //else
            //{
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
            //}

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
