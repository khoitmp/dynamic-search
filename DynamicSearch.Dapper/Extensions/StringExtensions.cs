namespace DynamicSearch.Dapper.Extension;

public static class StringExtensions
{
    public static string ToStringQuote(this string text)
    {
        return $"\"{text}\"";
    }
}