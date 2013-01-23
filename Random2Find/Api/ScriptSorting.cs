using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Api;
using Newtonsoft.Json;

namespace Random2Find.Api
{
    public enum ScriptSortType
    {
        Number,
        String
    }

    [JsonConverter(typeof(ScriptSortingConverter))]
    public class ScriptSorting : ISorting
    {
        public ScriptSorting()
        {
            this.Parameters = new Dictionary<string, FieldFilterValue>();
        }

        public string Script { get; set; }

        public ScriptSortType Type { get; set; }

        public Dictionary<string, FieldFilterValue> Parameters { get; private set; }

        public SortOrder? Order { get; set; }
    }
}
