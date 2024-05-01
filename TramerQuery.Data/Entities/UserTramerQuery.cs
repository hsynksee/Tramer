using SharedKernel.Abstractions;
using TramerQuery.Data.Enums;

namespace TramerQuery.Data.Entities
{
    public class UserTramerQuery : AuditableBaseEntity
    {
        public int UserId { get; protected set; }
        public int TramerQueryResultId { get; protected set; }
        public int? OldTramerQueryResultId { get; protected set; }
        public bool IsExistQuery { get; protected set; }
        public decimal Price { get; protected set; }

        #region Virtuals
        public virtual User User { get; protected set; }
        public virtual TramerQueryResult TramerQueryResult { get; protected set; }
        public virtual TramerQueryResult OldTramerQueryResult { get; protected set; }
        #endregion

        public UserTramerQuery SetBaseInformation(int userId, int tramerQueryResultId, int? oldTramerQueryResultId, bool isExistQuery, decimal price)
        {
            UserId = userId;
            TramerQueryResultId = tramerQueryResultId;
            OldTramerQueryResultId = oldTramerQueryResultId;
            IsExistQuery = isExistQuery;
            Price = price;

            return this;
        }
    }
}
