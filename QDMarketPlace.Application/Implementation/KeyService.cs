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
        private IRepository<ProductKey, int> _productKeyRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public KeyService(IRepository<Key,int> keyRepository,IMapper mapper,IUnitOfWork unitOfWork,
            IRepository<Product,int> productRepository,IRepository<ProductTag,int>producTagRepository,
            IRepository<ProductKey,int> productKeyRepository)
        {
            _keyRepository = keyRepository;
            _productRepository = productRepository;
            _productTagRepository = producTagRepository;
            _productKeyRepository = productKeyRepository;
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
            var keys = _productKeyRepository.FindAll();
            

            var query = from p in products
                        join k in keys
                        on p.Id equals k.ProductId
                        where p.Id == productId
                        select new {
                            value = k.Key,
                            status = k.Status,
                            id = k.Id
                        };
            string kq = "";
            foreach(var item in query)
            {
                if(item.status)
                {
                    kq = item.value;
                    RemoveKey(item.id);
                    break;
                }    
            }    
            return kq;
        }
        //Set Status key to false
        public void RemoveKey(int keyId)
        {
            var key = _productKeyRepository.FindById(keyId);
            key.Status = false;
            _productKeyRepository.Update(key);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
