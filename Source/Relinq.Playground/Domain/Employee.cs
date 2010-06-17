using System;
using Relinq.Script.Semantics.Literals.Metadata;

namespace Relinq.Playground.Domain
{
    public class Employee
    {
        [JsonProperty]
        public String Id { get; set; }

        [JsonProperty]
        public String FirstName { get; set; }

        [JsonProperty]
        public String LastName { get; set; }

        [JsonProperty]
        public String CompanyId { get; set; }
    }
}
