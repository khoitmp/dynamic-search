namespace DynamicSearch.Dapper.Interface;

public interface IFilterCompiler
{
    (string Query, ExpandoObject Value) Compile(JsonObject filter, ref int count);
}