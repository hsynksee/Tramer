using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TramerQuery.Service.Request.TramerQueryResult
{
    public class TramerQueryResultResquest
    {
        [JsonProperty("auth")]
        public TramerQueryAuthResquest Auth { get; set; }

        [JsonProperty("query")]
        public TramerQueryDetailResquest Query { get; set; }
    }


    public class TramerQueryAuthResquest
    {

        [JsonProperty("apiUser")]
        public string ApiUser { get; set; }

        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }
    }


    public class TramerQueryDetailResquest
    {
        [JsonProperty("chassisNumber")]
        public string ChassisNumber { get; set; }

        [JsonProperty("plate")]
        public string Plate { get; set; }

        [JsonProperty("refType")]
        public string RefType { get; set; }

        [JsonProperty("refId")]
        public string RefId { get; set; }
    }
}
