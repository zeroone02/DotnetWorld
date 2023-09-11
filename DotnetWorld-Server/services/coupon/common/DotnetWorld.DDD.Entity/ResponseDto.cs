namespace DotnetWorld.DDD;

public class ResponseDto<TData> : ResponseDto<TData, ErrorInfoResponse>
{

}

public class ResponseDto<TData, TError>
    where TError: ErrorInfoResponse
{
    public TData? Result { get; set; }
    public TError? Error { get; set; }
    public bool IsSuccess { get; set; } = true;
}

public class ErrorInfoResponse
{
    public string Message { get; set; }
    public long Code { get; set; }
}

public static class ApiResponseBuilder
{
    public static ResponseDto<TData> CreateApiResponse<TData>(TData data)
    {
        return new ResponseDto<TData>
        {
            Result = data
        };
    }

    public static ResponseDto<TData> CreateErrorApiResponse<TData>(long code, string message = default)
    {
        return new ResponseDto<TData>
        {
            Error = new ErrorInfoResponse
            {
                Code = code,
                Message = message
            },
            IsSuccess = false
        };
    }
}

