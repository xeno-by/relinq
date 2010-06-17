using System;
using Relinq.Script.Semantics.Literals.Metadata;

namespace Relinq.Playground.Domain
{
    public class User
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public String FirstName { get; set; }

        [JsonProperty]
        public String LastName { get; set; }

        [JsonProperty]
        public String Comment { get; set; }

        [JsonProperty]
        public DateTime BirthDate { get; set; }

        [JsonProperty]
        public bool IsMale { get; set; }

        [JsonProperty]
        public Guid CompanyId { get; set; }
    }
}