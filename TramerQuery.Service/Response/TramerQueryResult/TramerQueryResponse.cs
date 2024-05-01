using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TramerQuery.Service.Response.TramerQueryResult
{
    public class TramerQueryResponse
    {
        public int Id { get; set; }
        public DateTime TramerQueryDate { get; set; }
        public QueryResponse Response { get; set; }
    }

    public class QueryResponse
    {
        [JsonProperty("data")]
        public TramerQueryResponseData Data { get; set; }
    }

    public class TramerQueryResponseData
    {
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("status_desc")]
        public string StatusDesc { get; set; }

        [JsonProperty("content")]
        public TramerQueryResponseContent? Content { get; set; }
    }

    public class TramerQueryResponseContent
    {
        [JsonProperty("damage")]
        public List<TramerQueryResponseDamage> Damages { get; set; }

        [JsonProperty("chasisNo")]
        public string ChassisNumber { get; set; }

        [JsonProperty("brand")]
        public string Vehicle { get; set; }
    }

    public class TramerQueryResponseDamage
    {
        [JsonProperty("damageDate")]
        public string DamageDate { get; set; }

        [JsonProperty("damageChangePart")]
        public bool DamageChangePart { get; set; }

        [JsonProperty("damageDefinition")]
        public string DamageDefinition { get; set; }

        [JsonProperty("damageCurrency")]
        public string DamageCurrency { get; set; }

        [JsonProperty("damagePrice")]
        public decimal DamagePrice { get; set; }
    }
}
