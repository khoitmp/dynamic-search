namespace Kernel.Lib.Extension;

/// <summary>
/// Serialization/Deserialization using UTF-8 encoding takes less space and reduce the overhead in case the data being sent over the network
/// </summary> <summary>
public static class JsonExtension
{
    /// <summary>
    /// Uniform rules to control over the json object
    /// </summary>
    private static JsonSerializerOptions _jso = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter() }
    };

    public static JsonSerializerOptions JsonSerializerOptions => _jso;

    /// <summary>
    /// Serialize an object to bytes
    /// </summary>
    public static byte[] Serialize(this object input)
    {
        return JsonSerializer.SerializeToUtf8Bytes(input, _jso);
    }

    /// <summary>
    /// Deserialize bytes to an object
    /// </summary>
    public static T Deserialize<T>(this byte[] input)
    {
        return JsonSerializer.Deserialize<T>(input, _jso);
    }

    /// <summary>
    /// Convert an object to json string
    /// </summary>
    public static string ToJson(this object input)
    {
        return JsonSerializer.Serialize(input, _jso);
    }

    /// <summary>
    /// Convert json string to an object
    /// </summary>
    public static T ToObject<T>(this string input)
    {
        return JsonSerializer.Deserialize<T>(input, _jso);
    }

    /// <summary>
    /// Convert an object to byte an array
    /// </summary>
    public static byte[] ToByteArray<T>(T input)
    {
        if (input == null)
            return null;
        return Serialize(input);
    }

    /// <summary>
    /// Convert an byte array to an object
    /// </summary>
    public static (bool Success, T Result) ToObjectWithResult<T>(this byte[] input)
    {
        if (input == null)
            return (false, default(T));
        return (true, Deserialize<T>(input));
    }
}