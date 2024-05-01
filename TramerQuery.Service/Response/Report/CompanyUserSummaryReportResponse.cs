namespace TramerQuery.Service.Response.Report
{
    public class CompanyUserSummaryReportResponse
    {
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public int TotalQueryCount { get; set; }
        public decimal TotalQueryPrice { get; set; }
        public int TotalNewQueryCount { get; set; }
        public int TotalRepeatQueryCount { get; set; }
    }
}
