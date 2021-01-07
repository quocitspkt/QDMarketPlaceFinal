using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Application.ViewModels.Blog;
using QDMarketPlace.Application.ViewModels.Common;
using QDMarketPlace.Data.Entities;
using QDMarketPlace.Infrastructure.Interfaces;
using QDMarketPlace.Utilities.Constants;
using QDMarketPlace.Utilities.Dtos;
using QDMarketPlace.Utilities.Helpers;

namespace QDMarketPlace.Application.Implementation
{
    public class CommonService : ICommonService
    {
        private IRepository<Footer, string> _footerRepository;
        private IRepository<SystemConfig, string> _systemConfigRepository;
        private IUnitOfWork _unitOfWork;
        private IRepository<Slide, int> _slideRepository;
        private readonly IMapper _mapper;

        public CommonService(IRepository<Footer, string> footerRepository,
            IRepository<SystemConfig, string> systemConfigRepository,
            IUnitOfWork unitOfWork, IMapper mapper,
            IRepository<Slide, int> slideRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
            _slideRepository = slideRepository;
            _mapper = mapper;
        }

        public FooterViewModel GetFooter()
        {
            return _mapper.Map<Footer, FooterViewModel>(_footerRepository.FindSingle(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public List<SlideViewModel> GetSlides(string groupAlias)
        {
            return _mapper.ProjectTo<SlideViewModel>(
                _slideRepository.FindAll(x => x.Status && x.GroupAlias == groupAlias))
                .ToList();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return _mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindSingle(x => x.Id == code));
        }
    }
}