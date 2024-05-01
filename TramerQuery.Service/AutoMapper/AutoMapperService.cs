using AutoMapper;
using SharedKernel.Models;
using TramerQuery.Data.Entities;
using TramerQuery.Service.Response.Company;
using TramerQuery.Service.Response.Report;
using TramerQuery.Service.Response.TramerQueryResult;
using TramerQuery.Service.Response.User;
using TramerQuery.Service.Response.UserTramerQuery;

namespace TramerQuery.Service.AutoMapper
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            #region User
            CreateMap<User, UserResponse>()
                .ForMember(u => u.CompanyName, opt => opt.MapFrom(x => x.Company.Name));

            CreateMap<User, CurrentUser>()
                .ForMember(u => u.Name, opt => opt.MapFrom(x => x.Name + " " + x.Surname));
            #endregion

            #region Company
            CreateMap<Company, CompanyResponse>();
            #endregion

            #region TramerQuery
            CreateMap<TramerQueryResponse, TramerQueryResultResponse>()
                .ForMember(u => u.ChassisNumber, opt => opt.MapFrom(x => x.Response.Data.Content.ChassisNumber))
                .ForMember(u => u.Vehicle, opt => opt.MapFrom(x => x.Response.Data.Content.Vehicle))
                .ForMember(u => u.TotalDamageCount, opt => opt.MapFrom(x => x.Response.Data.Content.Damages.Count()))
                .ForMember(u => u.TotalDamagePrice, opt => opt.MapFrom(x => x.Response.Data.Content.Damages.Sum(s => s.DamagePrice)))
                .ForMember(u => u.Damages, opt => opt.MapFrom(x => x.Response.Data.Content.Damages));

            CreateMap<TramerQueryResponseDamage, DamageResponse>();
            #endregion

            #region UserTramerQuery
            CreateMap<UserTramerQuery, UserTramerQueryResponse>()
                .ForMember(u => u.CompanyId, opt => opt.MapFrom(x => x.User.CompanyId))
                .ForMember(u => u.CompanyName, opt => opt.MapFrom(x => x.User.Company.Name));

            CreateMap<UserTramerQuery, CompanyUserTramerQueryResponse>()
               .ForMember(u => u.CompanyId, opt => opt.MapFrom(x => x.User.CompanyId))
               .ForMember(u => u.CompanyName, opt => opt.MapFrom(x => x.User.Company.Name))
               .ForMember(u => u.UserId, opt => opt.MapFrom(x => x.User.Id))
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.User.Name + " " + x.User.Surname));
            #endregion

            #region Report
            CreateMap<UserTramerQuery, CompanyUserDetailReportResponse>()
                .ForMember(u => u.CompanyName, opt => opt.MapFrom(x => x.User.Company.Name))
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.User.Name + " " + x.User.Surname))
                .ForMember(u => u.QueryDate, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(u => u.TramerQueryType, opt => opt.MapFrom(x => x.TramerQueryResult.QueryType))
                .ForMember(u => u.TramerQuery, opt => opt.MapFrom(x => x.TramerQueryResult.QueryParameter))
                .ForMember(u => u.Response, opt => opt.MapFrom(x => x.TramerQueryResult.Response))
                .ForMember(u => u.OldQueryDate, opt => opt.MapFrom(x => x.OldTramerQueryResult.QueryDate))
                .ForMember(u => u.OldResponse, opt => opt.MapFrom(x => x.OldTramerQueryResult.Response));
            #endregion
        }

    }
}
