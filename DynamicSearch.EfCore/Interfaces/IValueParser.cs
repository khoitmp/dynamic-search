namespace DynamicSearch.EfCore.Interface;

internal interface IValueParser<T>
{
    T Parse(string value);
}