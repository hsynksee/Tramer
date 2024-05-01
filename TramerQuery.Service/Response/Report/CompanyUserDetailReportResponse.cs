using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TramerQuery.Data.Enums;

namespace TramerQuery.Service.Response.Report
{
    public class CompanyUserDetailReportResponse
    {
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public DateTime QueryDate { get; set; }
        public TramerQueryTypeEnum TramerQueryType { get; set; }
        public string TramerQuery { get; set; }
        public decimal Price { get; set; }
        public string Response { get; set; }
        public bool IsExistQuery { get; set; }

        public DateTime? OldQueryDate { get; set; }
        public string OldResponse { get; set; }
    }
}
