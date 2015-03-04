using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Find;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Api;
using EPiServer.Find.Api.Querying.Queries;
using Random2Find.Api;

namespace Random2Find
{
    public static class SortExtensions
    {
        public static ITypeSearch<T> ThenOrderRandom<T>(this ITypeSearch<T> search)
        {
            return search.OrderRandom();
        }

        public static ITypeSearch<T> OrderRandom<T>(this ITypeSearch<T> search)
        {
            return new Search<T, IQuery>(search, context => 
                {
                    context.RequestBody.Sort.Add(new ScriptSorting()
                    {
                        Script = "randomvaluegenerator",
                        Language = ScriptLanguage.Native,
                        Type = ScriptSortType.Number
                    });
                });
        }
    }
}
