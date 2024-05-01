using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TramerQuery.Service.Response.TramerQueryResult
{
    public class TramerQueryResultResponse
    {
        public int Id { get; set; }
        public bool IsNewQuery { get; set; }
        public string ChassisNumber { get; set; }
        public string Vehicle { get; set; }
        public DateTime TramerQueryDate { get; set; }
        public int TotalDamageCount { get; set; }
        public decimal TotalDamagePrice { get; set; }
        public List<DamageResponse> Damages { get; set; }
    }

    public class DamageResponse
    {
        public string DamageDate { get; set; }
        public bool DamageChangePart { get; set; }

        public string DamageDefinition { get; set; }
        public string DamageCurrency { get; set; }
        public decimal DamagePrice { get; set; }
    }
}
