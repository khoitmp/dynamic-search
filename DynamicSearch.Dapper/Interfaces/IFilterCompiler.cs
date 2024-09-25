namespace DynamicSearch.Dapper.Interface;

public interface IFilterCompiler
{
    (string Query, ExpandoObject Value) Compile(JObject filter, ref int count);
}