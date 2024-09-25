namespace DynamicSearch.Dapper.Interface;

public delegate (string Query, object[] Values, string[] Tokens) OperationBuilder(QueryFilter filter, Action<string[]> callback = null);

public interface IOperationBuilder
{
    (string Query, object[] Values, string[] Tokens) Build(QueryFilter filter, Action<string[]> callback = null);
}