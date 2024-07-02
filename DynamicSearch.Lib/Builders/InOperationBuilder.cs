namespace DynamicSearch.Lib.Service;

internal class InOperationBuilder : BaseArrayBuilder
{
    protected override string Operation => Operations.IN;

    private InOperationBuilder()
    {
    }

    public InOperationBuilder(IValueArrayParser<string> stringParser,
                                IValueArrayParser<double> numbericParser,
                                IValueArrayParser<Guid> guidArrayParser,
                                IValueArrayParser<DateTime> dateTimeParser)
        : base(stringParser, numbericParser, guidArrayParser, dateTimeParser)
    {
    }
}