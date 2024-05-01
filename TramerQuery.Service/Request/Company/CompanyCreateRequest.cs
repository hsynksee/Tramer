namespace TramerQuery.Service.Request.Company
{
    public class CompanyCreateRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public decimal QueryPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
