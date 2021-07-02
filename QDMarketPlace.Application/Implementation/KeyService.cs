using AutoMapper;
using System.Linq;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Data.Entities;
using QDMarketPlace.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Application.Implementation
{
    public class KeyService : IKeyService
    {
        private IRepository<Key, int> _keyRepository;
        private IRepository<Product, int> _productRepository;
        private IRepository<ProductTag, int> _productTagRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public KeyService(IRepository<Key,int> keyRepository,IMapper mapper,IUnitOfWork unitOfWork,
            IRepository<Product,int> productRepository,IRepository<ProductTag,int>producTagRepository)
        {
            _keyRepository = keyRepository;
            _productRepository = productRepository;
            _productTagRepository = producTagRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string GetById(int productId)
        {
            var products = _productRepository.FindAll();
            var keys = _keyRepository.FindAll();
            var productTags = _productTagRepository.FindAll();

            var query = from p in products
                        join k in keys
                        on p.Id equals k.ProductId
                        where p.Id == productId
                        select new {
                            value = k.Value,
                            status = k.Status,
                        };
            string kq = "";
            foreach(var item in query)
            {
                if(item.status)
                {
                    kq = item.value;
                    break;
                }    
            }    
            return kq;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
