namespace DynamicSearch.Dapper.Interface;

public interface IValueArrayParser<T>
{
    T[] Parse(string value);
}