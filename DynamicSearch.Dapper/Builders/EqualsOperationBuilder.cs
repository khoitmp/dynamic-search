namespace DynamicSearch.Dapper.Builder;

public class EqualsOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.EQUALS;

    public EqualsOperationBuilder(IValueParser<string> stringParser,
                            IValueParser<double> numbericParser,
                            IValueParser<bool> boolParser,
                            IValueParser<Guid> guidParser,
                            IValueParser<DateTime> dateParser)
        : base(stringParser, numbericParser, boolParser, guidParser, dateParser)
    {
    }
}