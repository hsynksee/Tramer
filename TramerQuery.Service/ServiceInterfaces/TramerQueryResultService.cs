using AutoMapper;
using Newtonsoft.Json;
using SharedKernel.Abstractions;
using System.Net;
using System.Text;
using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;
using TramerQuery.Data.Enums;
using TramerQuery.Service.Request.TramerQueryResult;
using TramerQuery.Service.Request.UserTramerQuery;
using TramerQuery.Service.Response.TramerQueryResult;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Service.ServiceInterfaces
{
    public class TramerQueryResultService : BaseAppService, ITramerQueryResultService
    {
        
        public TramerQueryResultService(IMapper mapper, IRepositoryWrapper repository, IAppSettings appSettings) : base(mapper, repository, appSettings)
        {
        }

        public async Task<BaseResponse<TramerQueryResultResponse>> GetTramerResult(TramerQueryTypeEnum queryType, string queryParameter, bool newQuery)
        {
            bool isNewQuery = false;
            TramerQueryResponse tramerResult = new TramerQueryResponse();

            var dbQuery = await _repository.TramerQueryResultRepository.GetByQueryParameter(queryParameter);
            if (dbQuery != null && !newQuery)
            {
                var dbTramer = new TramerQueryResponse { Id = dbQuery.Id, TramerQueryDate = dbQuery.QueryDate };
                dbTramer.Response = JsonConvert.DeserializeObject<QueryResponse>(dbQuery.Response);
                tramerResult = dbTramer;
            }
            else
            {
                string chassisNumber = TramerQueryTypeEnum.ChassisNumber == queryType ? queryParameter : null;
                string plateNumber = TramerQueryTypeEnum.PlateNumber == queryType ? queryParameter : null;

                var tramer = await this.GetTramerQuery(chassisNumber, plateNumber);

                if (tramer != null && tramer.Data != null)
                {
                    var tramerQuery = new TramerQueryResult().SetBaseInformation(
                        queryType,
                        queryParameter,
                        JsonConvert.SerializeObject(tramer),
                        tramer.Data.StatusCode,
                        tramer.Data.StatusDesc,
                        tramer.Data.Content == null ? false : true);

                    await _repository.TramerQueryResultRepository.Create(tramerQuery);
                    await _repository.Save();

                    var dbTramer = new TramerQueryResponse { Id = tramerQuery.Id, TramerQueryDate = tramerQuery.QueryDate };
                    dbTramer.Response = tramer;

                    tramerResult = dbTramer;

                    isNewQuery = true;

                    if (dbQuery != null && newQuery)
                    {
                        dbQuery.SetPassive();
                        _repository.TramerQueryResultRepository.Update(dbQuery);
                        await _repository.Save();
                    }
                }
                else
                    return new BaseResponse<TramerQueryResultResponse>();
            }

            var result = _mapper.Map<TramerQueryResultResponse>(tramerResult);
            result.IsNewQuery = isNewQuery;

            if (result != null)
            {
                var companyPrice = (await _repository.CompanyRepository.FindById(_appSettings.CurrentUser.CompanyId)).QueryPrice;
                var userTramerQueryCreateRequest = new UserTramerQueryCreateRequest
                {
                    UserId = _appSettings.CurrentUser.Id,
                    TramerQueryResultId = result.Id,
                    OldTramerQueryResultId = dbQuery == null ? null : (dbQuery.Id == result.Id ? null : dbQuery.Id),
                    Price = companyPrice,
                    IsExistQuery = dbQuery == null ? false : ((dbQuery != null && isNewQuery) ? true : false),
                };

                await this.CreateUserTramerQuery(userTramerQueryCreateRequest);
            }

            return new BaseResponse<TramerQueryResultResponse>(result);
        }

        private async Task<QueryResponse> GetTramerQuery(string chassisNumber, string plateNumber)
        {
            var appConfig = _appSettings.ApplicationConfigurations;

            HttpResponseMessage result;
            QueryResponse tramerResult = new QueryResponse();

            var request = new TramerQueryResultResquest();

            request.Auth = new TramerQueryAuthResquest { ApiKey = appConfig.ApiKey, ApiUser = appConfig.ApiUsername };
            request.Query = new TramerQueryDetailResquest { ChassisNumber = chassisNumber, Plate = plateNumber, RefId = "XXX", RefType = "SMARTIQ" };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                result = await client.PostAsync(appConfig.ApiUrl, content);

                var response = await result.Content.ReadAsStringAsync();

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    tramerResult = JsonConvert.DeserializeObject<QueryResponse>(response);
                }
            }

            return tramerResult;
        }

        private async Task<bool> CreateUserTramerQuery(UserTramerQueryCreateRequest request)
        {
            var userTramerQuery = new UserTramerQuery().SetBaseInformation(
                request.UserId,
                request.TramerQueryResultId,
                request.OldTramerQueryResultId,
                request.IsExistQuery,
                request.Price);

            await _repository.UserTramerQueryRepository.Create(userTramerQuery);
            var result = await _repository.Save() > 0;

            return result;
        }
    }
}
