namespace TramerQuery.Service.Response.Company
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public decimal QueryPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
