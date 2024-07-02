namespace DynamicSearch.Lib.Service;

internal class QueryCompiler : IQueryCompiler
{
    protected IDictionary<string, IOperationBuilder> SupportedOperations;

    public QueryCompiler(IDictionary<string, IOperationBuilder> supportedOperations)
    {
        SupportedOperations = supportedOperations;
    }

    public (string Query, string[] Tokens, object[] Values) Compile(JObject filter, ref int count, Action<string[]> callback = null)
    {
        if (filter.Count != 1)
        {
            /*
                - Flat object (single condition)
                {
                    "queryKey": "<query_key>",
                    "queryType": "<query_type>",
                    "operation": "<operation>",
                    "queryValue": "<query_value>"
                }
            */

            var filterCriteria = filter.ToObject<FilterCriteria>(Defaults.JsonSerializer);

            if (!SupportedOperations.ContainsKey(filterCriteria.Operation))
            {
                throw new NotSupportedException($"{filterCriteria.Operation} is not supported");
            }

            var builder = SupportedOperations[filterCriteria.Operation];
            var rs = builder.Build(filterCriteria);
            var query = rs.Query;
            var tokens = new List<string>();

            for (int i = 0; i < rs.Values.Length; i++)
            {
                // Replace token name to avoid dupplication
                var replacedToken = $"@{count}";

                query = query.Replace(rs.Tokens[i], replacedToken);
                tokens.Add(replacedToken);

                count++;
            }

            return (query, tokens.ToArray(), rs.Values);
        }
        else
        {
            /*
                - Hierachical object (and/or condition)
                {
                    "and": [
                        {
                            "queryKey": "<query_key>",
                            "queryType": "<query_type>",
                            "operation": "<operation>",
                            "queryValue": "<query_value>"
                        },
                        {
                            "queryKey": "<query_key>",
                            "queryType": "<query_type>",
                            "operation": "<operation>",
                            "queryValue": "<query_value>"
                        }
                    ]
                }
            */

            var dic = filter.ToObject<IDictionary<string, JArray>>(Defaults.JsonSerializer);
            var listQuery = new List<string>();
            var listToken = new List<string>();
            var listValue = new List<object>();
            var query = string.Empty;

            foreach (var kv in dic)
            {
                foreach (var v in kv.Value)
                {
                    var rs = Compile(v as JObject, ref count, callback);
                    if (rs.Values.Length != rs.Tokens.Length)
                    {
                        throw new Exception("Number of values & tokens must be equals");
                    }

                    listQuery.Add(rs.Query);
                    listToken.AddRange(rs.Tokens);
                    listValue.AddRange(rs.Values);
                }

                // Join and/or condition with list query
                query = $"({string.Join($" {kv.Key.TrimStart('$')} ", listQuery)})";
            }

            return (query, listToken.ToArray(), listValue.ToArray());
        }
    }
}