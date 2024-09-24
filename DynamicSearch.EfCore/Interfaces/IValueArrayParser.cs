namespace DynamicSearch.EfCore.Interface;

internal interface IValueArrayParser<T>
{
    T[] Parse(string value);
}