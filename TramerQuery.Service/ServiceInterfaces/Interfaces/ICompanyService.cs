using Microsoft.AspNetCore.Mvc;
using SharedKernel.Abstractions;
using TramerQuery.Service.Request.Company;
using TramerQuery.Service.Response.Company;
using TramerQuery.Service.Response.Report;

namespace TramerQuery.Service.ServiceInterfaces.Interfaces
{
    public interface ICompanyService
    {
        Task<BaseResponse<List<CompanyResponse>>> GetCompanies(bool? isActive = null);
        Task<BaseResponse<CompanyResponse>> GetCompanyById(int id);
        Task<BaseResponse<bool>> UpdateCompanyStatus(int id);
        Task<BaseResponse<int>> CreateCompany([FromBody] CompanyCreateRequest request);
        Task<BaseResponse<int>> UpdateCompany([FromBody] CompanyUpdateRequest request);
        Task<BaseResponse<bool>> DeleteCompany(int id);

        #region Report
        Task<BaseResponse<List<CompanySummaryReportResponse>>> CompanySummaryReport(DateTime startDate, DateTime endDate, int? companyId);
        Task<BaseResponse<List<CompanyUserSummaryReportResponse>>> CompanyUserSummaryReport(DateTime startDate, DateTime endDate, int? companyId);
        Task<BaseResponse<List<CompanyUserDetailReportResponse>>> GetCompanyUserDetailReport(DateTime startDate, DateTime endDate, int? companyId);

        #endregion
    }
}
