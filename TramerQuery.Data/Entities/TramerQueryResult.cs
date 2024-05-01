using SharedKernel.Abstractions;
using TramerQuery.Data.Enums;

namespace TramerQuery.Data.Entities
{
    public class TramerQueryResult : AuditableBaseEntity
    {
        public TramerQueryTypeEnum QueryType { get; protected set; }
        public string QueryParameter { get; protected set; }
        public string Response { get; protected set; }
        public DateTime QueryDate { get; protected set; }
        public string StatusCode { get; protected set; }
        public string StatusDesc { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public bool IsActive { get; protected set; }

        //public virtual ICollection<UserTramerQuery> UserTramerQueries { get; protected set; }
        //public virtual ICollection<UserTramerQuery> OldUserTramerQueries { get; protected set; }

        public TramerQueryResult SetBaseInformation(TramerQueryTypeEnum queryType, string queryParameter, string response, string statusCode, string statusDesc, bool isSuccess)
        {
            QueryType = queryType;
            QueryParameter = queryParameter;
            Response = response;
            QueryDate = DateTime.Now;
            StatusCode = statusCode;
            StatusDesc = statusDesc;
            IsSuccess = isSuccess;
            IsActive = true;
            return this;
        }

        public TramerQueryResult SetPassive()
        {
            IsActive = false;

            return this;
        }
    }
}
