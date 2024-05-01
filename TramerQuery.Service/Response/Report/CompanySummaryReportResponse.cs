namespace TramerQuery.Service.Response.Report
{
    public class CompanySummaryReportResponse
    {
        public string CompanyName { get; set; }
        public int TotalQueryCount { get; set; }
        public decimal TotalQueryPrice { get; set; }
        public int TotalNewQueryCount { get; set; }
        public int TotalRepeatQueryCount { get; set; }
    }
}
