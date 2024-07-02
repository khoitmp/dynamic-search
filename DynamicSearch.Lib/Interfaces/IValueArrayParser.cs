namespace DynamicSearch.Lib.Interface;

internal interface IValueArrayParser<T>
{
    T[] Parse(string value);
}