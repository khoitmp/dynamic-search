namespace DynamicSearch.Lib.Interface;

internal interface IValueParser<T>
{
    T Parse(string value);
}