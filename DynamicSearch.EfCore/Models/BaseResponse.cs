namespace DynamicSearch.EfCore.Model;

public class BaseResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public BaseResponse(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static BaseResponse Success
    {
        get
        {
            return new BaseResponse(true, null);
        }
    }

    public static BaseResponse Failed
    {
        get
        {
            return new BaseResponse(false, null);
        }
    }
}