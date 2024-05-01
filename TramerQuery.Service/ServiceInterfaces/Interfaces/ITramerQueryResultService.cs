using SharedKernel.Abstractions;
using TramerQuery.Data.Enums;
using TramerQuery.Service.Response.TramerQueryResult;

namespace TramerQuery.Service.ServiceInterfaces.Interfaces
{
    public interface ITramerQueryResultService
    {
        Task<BaseResponse<TramerQueryResultResponse>> GetTramerResult(TramerQueryTypeEnum queryType, string queryParameter, bool newQuery);
    }
}
