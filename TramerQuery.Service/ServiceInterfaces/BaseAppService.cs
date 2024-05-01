using AutoMapper;
using SharedKernel.Abstractions;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TramerQuery.Data.Abstractions;

namespace TramerQuery.Service.ServiceInterfaces
{
    public abstract class BaseAppService
    {
        protected readonly IMapper _mapper;
        protected readonly IRepositoryWrapper _repository;
        protected readonly IAppSettings _appSettings;

        protected CurrentUser currentUser;
        public BaseAppService(IMapper mapper,
                              IRepositoryWrapper repository,
                              IAppSettings appSettings)
        {
            _mapper = mapper;
            _repository = repository;
            _appSettings = appSettings;
        }
    }
}
