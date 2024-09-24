namespace DynamicSearch.EfCore.Interface;

public interface IQueryCompiler
{
    /// <summary>
    /// Compile object filter to actual query
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="count"></param>
    /// <param name="callback"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="Query"></param>
    /// <param name="Tokens"></param>
    /// <param name="filter"></param>
    /// <param name="count"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    (string Query, string[] Tokens, object[] Values) Compile(JObject filter, ref int count, Action<string[]> callback = null);
}