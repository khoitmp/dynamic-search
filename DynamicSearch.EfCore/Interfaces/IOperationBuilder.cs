namespace DynamicSearch.EfCore.Interface;

internal delegate (string Query, string[] Tokens, object[] Values) OperationBuilder(FilterCriteria filter, Action<string[]> callback = null);
internal interface IOperationBuilder
{
    /// <summary>
    /// Build query in Linq.Dynamic.Core style
    /// </summary>
    /// <param name="Query">"name == @value"</param>
    /// <param name="Tokens">["@value"]</param>
    /// <param name="Values">["<sample_value>"]</param>
    /// <returns></returns>
    (string Query, string[] Tokens, object[] Values) Build(FilterCriteria filter, Action<string[]> callback = null);
}