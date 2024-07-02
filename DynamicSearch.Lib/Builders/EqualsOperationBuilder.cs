namespace DynamicSearch.Lib.Service;

internal class EqualsOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.EQUALS;

    public EqualsOperationBuilder(IValueParser<string> stringParser,
                                    IValueParser<double> numbericParser,
                                    IValueParser<bool> boolParser,
                                    IValueParser<Guid> guidParser,
                                    IValueParser<DateTime> dateParser)
        : base(stringParser: stringParser, numbericParser: numbericParser, boolParser: boolParser, guidParser: guidParser, dateParser: dateParser)
    {
    }
}