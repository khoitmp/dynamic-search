namespace DynamicSearch.Lib.Interface;

public interface IQueryCompiler
{
    (string Query, string[] Tokens, object[] Values) Compile(JObject filter, ref int count, Action<string[]> callback = null);
}