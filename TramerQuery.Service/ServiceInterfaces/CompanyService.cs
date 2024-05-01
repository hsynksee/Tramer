using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Abstractions;
using SharedKernel.Enum;
using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;
using TramerQuery.Service.Request.Company;
using TramerQuery.Service.Response.Company;
using TramerQuery.Service.Response.Report;
using TramerQuery.Service.Response.UserTramerQuery;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Service.ServiceInterfaces
{
    public class CompanyService : BaseAppService, ICompanyService
    {
        public CompanyService(IMapper mapper, IRepositoryWrapper repository, IAppSettings appSettings) : base(mapper, repository, appSettings)
        {
        }

        public async Task<BaseResponse<int>> CreateCompany([FromBody] CompanyCreateRequest request)
        {
            var company = await _repository.CompanyRepository.FindByTaxNumber(request.TaxNumber);
            if (company != null) return new BaseResponse<int>(0, "Aynı Verig No./ T.C ile kayıtlı firma mevcuttur..");

            var requestCompanyCreate = new Company().SetBaseInformation(
                request.Name,
                request.IsActive,
                request.Phone,
                request.TaxNumber,
                request.TaxOffice,
                request.QueryPrice
                );
            await _repository.CompanyRepository.Create(requestCompanyCreate);
            await _repository.Save();

            return new BaseResponse<int>(requestCompanyCreate.Id);
        }

        public async Task<BaseResponse<bool>> DeleteCompany(int id)
        {
            var deleteCompanies = await _repository.CompanyRepository.FindById(id);
            if (deleteCompanies == null) return new BaseResponse<bool>(false, "Silinecek şirket ayarı İzin bulunamadı..");

            await _repository.CompanyRepository.Delete(deleteCompanies.Id);
            var result = await _repository.Save() > 0;

            return new BaseResponse<bool>(result);
        }

        public async Task<BaseResponse<List<CompanyResponse>>> GetCompanies(bool? isActive = null)
        {
            var companies = _mapper.Map<List<CompanyResponse>>(await _repository.CompanyRepository.GetAll());

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.Personnel)
                return new BaseResponse<List<CompanyResponse>>(null);

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin)
                companies = companies.Where(w => w.Id == _appSettings.CurrentUser.CompanyId).ToList();

            if (isActive != null)
                companies = companies.Where(w => w.IsActive == isActive).ToList();

            return new BaseResponse<List<CompanyResponse>>(companies);
        }

        public async Task<BaseResponse<CompanyResponse>> GetCompanyById(int id)
        {
            var company = _mapper.Map<CompanyResponse>(await _repository.CompanyRepository.FindById(id));

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin && _appSettings.CurrentUser.CompanyId != id)
                return new BaseResponse<CompanyResponse>(null);

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.Personnel)
                return new BaseResponse<CompanyResponse>(null);

            return new BaseResponse<CompanyResponse>(company);
        }

        public async Task<BaseResponse<bool>> UpdateCompanyStatus(int id)
        {
            var company = await _repository.CompanyRepository.FindById(id);
            var workUserInCompany = await _repository.UserRepository.FindByCompanyId(id);

            if (company == null)
                return new BaseResponse<bool>(false, "Şirket bulunmamaktadır..");

            company.SetActive(!company.IsActive);
            _repository.CompanyRepository.Update(company);
            await _repository.Save();
            string message = "";
            if (!company.IsActive)
            {
                foreach (var item in workUserInCompany)
                    item.SetInActive();

                await _repository.Save();
                message = "Şirket pasif yapılmıştır.";
            }
            else message = "Şirket aktif yapılmıştır.";

            return new BaseResponse<bool>(true, message);
        }

        public async Task<BaseResponse<int>> UpdateCompany([FromBody] CompanyUpdateRequest request)
        {
            var updateCompany = await _repository.CompanyRepository.FindById(request.Id);
            if (updateCompany == null)
            {
                return new BaseResponse<int>(0, "Şirket bulunamadı.");
            }

            updateCompany.SetBaseInformation(request.Name,
                request.IsActive,
                request.Phone,
                request.TaxNumber,
                request.TaxOffice,
                request.QueryPrice);
            _repository.CompanyRepository.Update(updateCompany);
            var result = await _repository.Save() > 0;

            if (result)
            {
                return new BaseResponse<int>(updateCompany.Id);
            }
            else
            {
                return new BaseResponse<int>(0, "Şirket güncellenirken bir hata oluştu.");
            }
        }

        #region Report
        public async Task<BaseResponse<List<CompanySummaryReportResponse>>> CompanySummaryReport(DateTime startDate, DateTime endDate, int? companyId)
        {
            var allCompanies = _mapper.Map<List<UserTramerQueryResponse>>(await _repository.UserTramerQueryRepository.GetAllByDate(startDate, endDate));

            var filteredCompanies = allCompanies.GroupBy(g => g.CompanyId).ToList();
            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin)
                filteredCompanies = filteredCompanies.Where(w => w.Key == _appSettings.CurrentUser.CompanyId).ToList();
            else if (companyId.HasValue)
                filteredCompanies = filteredCompanies.Where(w => w.Key == companyId.Value).ToList();

            var userTramerQueryResponse = new List<CompanySummaryReportResponse>();

            foreach (var group in filteredCompanies)
            {
                userTramerQueryResponse.Add(new CompanySummaryReportResponse
                {
                    CompanyName = group.FirstOrDefault().CompanyName,
                    TotalQueryCount = group.Count(),
                    TotalQueryPrice = group.Sum(s => s.Price),
                    TotalNewQueryCount = group.Where(w => w.IsExistQuery).Count(),
                    TotalRepeatQueryCount = group.Where(w => !w.IsExistQuery).Count()
                });
            }

            return new BaseResponse<List<CompanySummaryReportResponse>>(userTramerQueryResponse.OrderBy(o => o.CompanyName).ToList());
        }

        public async Task<BaseResponse<List<CompanyUserSummaryReportResponse>>> CompanyUserSummaryReport(DateTime startDate, DateTime endDate, int? companyId)
        {
            var allCompanies = _mapper.Map<List<CompanyUserTramerQueryResponse>>(await _repository.UserTramerQueryRepository.GetAllByDate(startDate, endDate));

            var filteredCompanies = allCompanies.GroupBy(g => g.UserId).ToList();
            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin)
                filteredCompanies = filteredCompanies.Where(w => w.Any(a => a.CompanyId == _appSettings.CurrentUser.CompanyId)).ToList();
            else if (companyId.HasValue)
                filteredCompanies = filteredCompanies.Where(w => w.Any(a => a.CompanyId == companyId.Value)).ToList();

            var userTramerQueryResponse = new List<CompanyUserSummaryReportResponse>();

            foreach (var group in filteredCompanies)
            {
                var user = group.First();
                userTramerQueryResponse.Add(new CompanyUserSummaryReportResponse
                {
                    CompanyName = group.FirstOrDefault().CompanyName,
                    TotalQueryCount = group.Count(),
                    UserName = user.UserName,
                    TotalQueryPrice = group.Sum(s => s.Price),
                    TotalNewQueryCount = group.Where(w => w.IsExistQuery).Count(),
                    TotalRepeatQueryCount = group.Where(w => !w.IsExistQuery).Count()
                });
            }

            return new BaseResponse<List<CompanyUserSummaryReportResponse>>(userTramerQueryResponse.OrderBy(o => o.CompanyName).ThenBy(t => t.UserName).ToList());
        }

        public async Task<BaseResponse<List<CompanyUserDetailReportResponse>>> GetCompanyUserDetailReport(DateTime startDate, DateTime endDate, int? companyId)
        {
            var allCompanies = await _repository.UserTramerQueryRepository.GetAllByDate(startDate, endDate);

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin)
                allCompanies = allCompanies.Where(w => w.User.CompanyId == _appSettings.CurrentUser.CompanyId).ToList();
            else if (companyId.HasValue)
                allCompanies = allCompanies.Where(w => w.User.CompanyId == companyId.Value).ToList();

            var userTramerQueryResponse = _mapper.Map<List<CompanyUserDetailReportResponse>>(allCompanies);

            return new BaseResponse<List<CompanyUserDetailReportResponse>>(userTramerQueryResponse.OrderBy(o => o.QueryDate).ToList());
        }

        #endregion
    }
}
