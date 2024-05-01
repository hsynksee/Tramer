using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enum;
using TramerQuery.Api.Infrastructure.Abstractions;
using TramerQuery.Api.Infrastructure.Attributes;
using TramerQuery.Service.Request.Company;
using TramerQuery.Service.ServiceInterfaces;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Api.Controllers
{
    public class CompanyController : BaseController
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Firma Ekleme
        /// </summary>
        /// <param name="request">CompanyCreateRequest</param>
        /// <returns></returns>
        [HttpPost("CreateCompany")]
        [HasRole(UserRoleEnum.SystemAdmin)]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateRequest request) => Ok(await _companyService.CreateCompany(request));

        /// <summary>
        /// Firma Güncelleme
        /// </summary>
        /// <param name="request">CompanyUpdateRequest</param>
        /// <returns></returns>
        [HttpPut("UpdateCompany")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateRequest request) => Ok(await _companyService.UpdateCompany(request));

        /// <summary>
        /// Firma Listeleme
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCompanies")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetCompanies(bool? isActive = null) => Ok(await _companyService.GetCompanies(isActive));

        /// <summary>
        /// Firma Bilgisini Getirme
        /// </summary>
        /// <param name="id">Firma Id</param>
        /// <returns></returns>
        [HttpGet("GetCompanyById")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetCompanyById(int id) => Ok(await _companyService.GetCompanyById(id));

        /// <summary>
        /// Firma Aktif/Pasif Yapma (Pasif yaparken bütün kullanıcılarını da pasif yapıyor)
        /// </summary>
        /// <param name="id">Firma Id</param>
        /// <returns></returns>
        [HttpPut("UpdateCompanyStatus")]
        [HasRole(UserRoleEnum.SystemAdmin)]
        public async Task<IActionResult> UpdateCompanyStatus(int id) => Ok(await _companyService.UpdateCompanyStatus(id));

        #region Report
        /// <summary>
        /// Firma Rapor Getirme
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="companyId">Firma Id (Boş geçilebilir)</param>
        /// <returns></returns>
        [HttpGet("GetCompanySummaryReport")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetCompanySummaryReport(DateTime startDate, DateTime endDate, int? companyId = null) => Ok(await _companyService.CompanySummaryReport(startDate, endDate, companyId));

        /// <summary>
        /// Firma Kullanıcı Rapor Getirme
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="companyId">Firma Id (Boş geçilebilir)</param>
        /// <returns></returns>
        [HttpGet("GetCompanyUserSummaryReport")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetCompanyUserSummaryReport(DateTime startDate, DateTime endDate, int? companyId = null) => Ok(await _companyService.CompanyUserSummaryReport(startDate, endDate, companyId));

        /// <summary>
        /// Firma Kullanıcı Detay Rapor Getirme
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="companyId">Firma Id (Boş geçilebilir)</param>
        /// <returns></returns>
        [HttpGet("GetCompanyUserDetailReport")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetCompanyUserDetailReport(DateTime startDate, DateTime endDate, int? companyId = null) => Ok(await _companyService.GetCompanyUserDetailReport(startDate, endDate, companyId));
        #endregion
    }
}
