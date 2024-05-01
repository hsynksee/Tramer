using Microsoft.AspNetCore.Mvc;
using TramerQuery.Api.Infrastructure.Abstractions;
using TramerQuery.Data.Enums;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Api.Controllers
{
    public class TramerQueryResultController : BaseController
    {
        private ITramerQueryResultService _tramerQueryResultService;

        public TramerQueryResultController(ITramerQueryResultService tramerQueryResultService)
        {
            _tramerQueryResultService = tramerQueryResultService;
        }

        /// <summary>
        /// Tramer Sorgulama
        /// </summary>
        /// <param name="queryType">TramerQueryTypeEnum [ChassisNumber, PlateNumber]</param>
        /// <param name="queryParameter">Şasi veya Plaka</param>
        /// <param name="newQuery">SBM sorgulama yeniden yapılsın mı</param>
        /// <returns></returns>
        [HttpGet("GetTramerResult")]
        public async Task<IActionResult> GetTramerResult(TramerQueryTypeEnum queryType, string queryParameter, bool newQuery)
        {
            return Ok(await _tramerQueryResultService.GetTramerResult(queryType, queryParameter, newQuery));
        }
    }
}
