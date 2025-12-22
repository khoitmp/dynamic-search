namespace Kernel.Lib.Constant;

public static class JsonSettings
{
    private static JsonSerializerOptions _jso = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter() }
    };

    public static string DEFAULT_FULL_DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ss:ffff";
    public static string DEFAULT_SHORT_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
    public static string DEFAULT_ALIAS_DATETIME_FORMAT = "yyyyMMddHHmmss";
    public static JsonSerializerOptions JsonSerializerOptions => _jso;
}