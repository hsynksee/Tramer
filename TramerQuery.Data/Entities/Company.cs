using SharedKernel.Abstractions;

namespace TramerQuery.Data.Entities
{
    public class Company : AuditableBaseEntity
    {
        public string Name { get; protected set; }
        public bool IsActive { get; protected set; }
        public string Phone { get; protected set; }
        public string TaxNumber { get; protected set; }
        public string TaxOffice { get; protected set; }
        public decimal QueryPrice { get; protected set; }

        #region Virtuals
        public virtual ICollection<User> Users { get; set; }
        #endregion

        public Company SetBaseInformation(string name, bool isActive,string phone, string taxNumber, string taxOffice, decimal queryPrice)
        {
            Name = name;
            IsActive = isActive;
            Phone = phone;
            TaxNumber = taxNumber;
            TaxOffice = taxOffice;
            QueryPrice = queryPrice;
            return this;
        }

        public Company SetActive(bool active)
        {
            IsActive = active;

            return this;
        }
    }
}
