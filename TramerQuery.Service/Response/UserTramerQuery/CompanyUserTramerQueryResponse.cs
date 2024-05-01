namespace TramerQuery.Service.Response.UserTramerQuery
{
    public class CompanyUserTramerQueryResponse
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public bool IsExistQuery { get; set; }
        public decimal Price { get; set; }
    }
}
