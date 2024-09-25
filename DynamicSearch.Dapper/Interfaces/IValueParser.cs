namespace DynamicSearch.Dapper.Interface;

public interface IValueParser<T>
{
    T Parse(string value);
}