﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Application.ViewModels.Common;
using QDMarketPlace.Application.ViewModels.Product;
using QDMarketPlace.Data.Entities;
using QDMarketPlace.Data.Enums;
using QDMarketPlace.Infrastructure.Interfaces;
using QDMarketPlace.Utilities.Constants;
using QDMarketPlace.Utilities.Dtos;
using QDMarketPlace.Utilities.Helpers;

namespace QDMarketPlace.Application.Implementation
{
    public class ProductService : IProductService
    {
        private IRepository<Product, int> _productRepository;
        private IRepository<Tag, string> _tagRepository;
        private IRepository<ProductTag, int> _productTagRepository;
        private IRepository<ProductQuantity, int> _productQuantityRepository;
        private IRepository<ProductImage, int> _productImageRepository;
        private IRepository<ProductKey, int> _wholePriceRepository;
        private IRepository<Bill, int> _billRepository;
        private IRepository<BillDetail, int> _billDetailRepository;
        private IRepository<ProductKey, int> _productKeyRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ProductService(IRepository<Product, int> productRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<ProductQuantity, int> productQuantityRepository,
            IRepository<ProductImage, int> productImageRepository,
            IRepository<ProductKey, int> wholePriceRepository,
            IRepository<Bill,int> billRepository,
            IRepository<BillDetail,int> billDetailRepository,
            IRepository<ProductKey, int> productKeyRepository,
        IUnitOfWork unitOfWork,
        IRepository<ProductTag, int> productTagRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productQuantityRepository = productQuantityRepository;
            _productTagRepository = productTagRepository;
            _wholePriceRepository = wholePriceRepository;
            _productImageRepository = productImageRepository;
            _billRepository = billRepository;
            _billDetailRepository = billDetailRepository;
            _productKeyRepository = productKeyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProductViewModel Add(ProductViewModel productVm)
        {
            List<ProductTag> productTags = new List<ProductTag>();
            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                var product = _mapper.Map<ProductViewModel, Product>(productVm);
                foreach (var productTag in productTags)
                {
                    product.ProductTags.Add(productTag);
                }
                product.DateCreated = DateTime.Now;
                _productRepository.Add(product);
            }
            return productVm;
        }

        public void AddQuantity(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productQuantityRepository.RemoveMultiple(_productQuantityRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var quantity in quantities)
            {
                _productQuantityRepository.Add(new ProductQuantity()
                {
                    ProductId = productId,
                    ColorId = quantity.ColorId,
                    SizeId = quantity.SizeId,
                    Quantity = quantity.Quantity
                });
            }
        }

        public void Delete(int id)
        {
            var product = _productRepository.FindSingle(x => x.Id == id);
            product.IsDeleted = true;
            _productRepository.Update(product);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductViewModel> GetAll()
        {
            return _mapper.ProjectTo<ProductViewModel>(
                _productRepository.FindAll(x => x.ProductCategory))
                .ToList();
        }
        public List<ProductViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _mapper.ProjectTo<ProductViewModel>(
                    _productRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.Name)).ToList();
            else
                return _mapper.ProjectTo<ProductViewModel>(
                    _productRepository.FindAll().OrderBy(x => x.Name)
                    )
                    .ToList();
        }
        

        public PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            int totalRow = query.Count();
            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = _mapper.ProjectTo<ProductViewModel>(query).ToList();
            
            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            
            return paginationSet;
        }
        public PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize,string sortBy)
        {
            var query = _productRepository.FindAll(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);
            query = query.Where(x => x.Status == Status.Active);
            int totalRow = query.Count();
            switch (sortBy)
            {
                case "DateCreated":
                    {
                        query = query.OrderByDescending(x => x.DateCreated)
                            .Skip((page - 1) * pageSize).Take(pageSize);
                        break;
                    }
                case "Price":
                    {
                        query = query.OrderByDescending(x => x.Price)
                            .Skip((page - 1) * pageSize).Take(pageSize);
                        break;
                    }
                case "Name":
                    {
                        query = query.OrderByDescending(x => x.Name)
                        .Skip((page - 1) * pageSize).Take(pageSize);
                        break;
                    }
                default:
                    {
                        query = query.OrderByDescending(x => x.DateCreated)
                            .Skip((page - 1) * pageSize).Take(pageSize);
                        break;
                    }
                    
            }    

            var data = _mapper.ProjectTo<ProductViewModel>(query).ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public ProductViewModel GetById(int id)
        {
            return _mapper.Map<Product, ProductViewModel>(_productRepository.FindById(id));
        }

        public List<ProductQuantityViewModel> GetQuantities(int productId)
        {
            return _mapper.ProjectTo<ProductQuantityViewModel>(
                _productQuantityRepository.FindAll(x => x.ProductId == productId))
                .ToList();
        }

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                    product.CategoryId = categoryId;

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

                    product.PromotionPrice = promotionPrice;
                    product.Content = workSheet.Cells[i, 6].Value.ToString();
                    product.SeoKeywords = workSheet.Cells[i, 7].Value.ToString();

                    product.SeoDescription = workSheet.Cells[i, 8].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    _productRepository.Add(product);
                }
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel productVm)
        {
            List<ProductTag> productTags = new List<ProductTag>();

            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag();
                        tag.Id = tagId;
                        tag.Name = t;
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x => x.Id == productVm.Id).ToList());
                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
            }

            var product = _mapper.Map<ProductViewModel, Product>(productVm);
            foreach (var productTag in productTags)
            {
                product.ProductTags.Add(productTag);
            }
            product.DateCreated = DateTime.Now;
            _productRepository.Update(product);
        }

        public List<ProductImageViewModel> GetImages(int productId)
        {
            return _mapper.ProjectTo<ProductImageViewModel>(
                _productImageRepository.FindAll(x => x.ProductId == productId)
                ).ToList();
        }

        public void AddImages(int productId, string[] images)
        {
            _productImageRepository.RemoveMultiple(_productImageRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var image in images)
            {
                _productImageRepository.Add(new ProductImage()
                {
                    Path = image,
                    ProductId = productId,
                    Caption = string.Empty
                });
            }
        }

        public void AddWholePrice(int productId, List<ProductKeyViewModel> WholePrices)
        {
            //Get list key with status is true
            var productKeys = _wholePriceRepository.FindAll(x => x.ProductId == productId);
            var query = from p in productKeys
                        where p.Status == true
                        select p;
            foreach(var itemProductKey in query.ToList())
            {
                var count = 0;
                foreach (var itemWholePrice in WholePrices)
                {
                    
                    if (itemWholePrice.Key.Contains(itemProductKey.Key))
                    {
                        break;
                    }
                    if (!itemWholePrice.Key.Contains(itemProductKey.Key))
                    {
                        count = count + 1;
                    }
                    if(count == WholePrices.Count())
                    {
                        var key = _productKeyRepository.FindById(itemProductKey.Id);
                        key.Status = false;
                        _productKeyRepository.Update(key);
                        _unitOfWork.Commit();
                    }    
                }
            }
            var productKeyLast = _wholePriceRepository.FindAll(x => x.ProductId == productId);
            var queryLast = from p in productKeyLast
                        where p.Status == true
                        select p;
            //Delete list key
            _wholePriceRepository.RemoveMultiple(queryLast.ToList());
            foreach (var wholePrice in WholePrices)
            {
                _wholePriceRepository.Add(new ProductKey()
                {
                    ProductId = productId,
                    Key = wholePrice.Key,
                    Status = true,
                });
                _unitOfWork.Commit();
            }
        }

        public List<ProductKeyViewModel> GetWholePrices(int productId)
        {
            var productKeys = _wholePriceRepository.FindAll(x => x.ProductId == productId);

            var query = from p in productKeys
                        where p.Status == true
                        select p;

            //return _mapper.ProjectTo<ProductKeyViewModel>(
            //    _wholePriceRepository.FindAll(x => x.ProductId == productId)
            //    ).ToList();
            return _mapper.ProjectTo<ProductKeyViewModel>(query).ToList();
        }

        public List<ProductViewModel> GetLastest(int top)
        {
            return _mapper.ProjectTo<ProductViewModel>(
                _productRepository.FindAll(x => x.Status == Status.Active)
                .OrderByDescending(x => x.DateCreated)
                )
                .ToList();
        }

        public List<ProductViewModel> GetHotProduct(int top)
        {
            return _mapper.ProjectTo<ProductViewModel>(
                _productRepository.FindAll(x => x.Status == Status.Active && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top))

                .ToList();
        }

        public List<ProductViewModel> GetRelatedProducts(int id, int top)
        {
            var product = _productRepository.FindById(id);
            return _mapper.ProjectTo<ProductViewModel>(
                _productRepository.FindAll(x => x.Status == Status.Active
                && x.Id != id && x.CategoryId == product.CategoryId)
            .OrderByDescending(x => x.DateCreated)
            .Take(top))
            .ToList();
        }

        public List<ProductViewModel> GetUpsellProducts(int top)
        {
            return _mapper.ProjectTo<ProductViewModel>(
                _productRepository.FindAll(x => x.PromotionPrice != null)
               .OrderByDescending(x => x.DateModified)
               .Take(top)).ToList();
        }

        public List<TagViewModel> GetProductTags(int productId)
        {
            var tags = _tagRepository.FindAll();
            var productTags = _productTagRepository.FindAll();

            var query = from t in tags
                        join pt in productTags
                        on t.Id equals pt.TagId
                        where pt.ProductId == productId
                        select new TagViewModel()
                        {
                            Id = t.Id,
                            Name = t.Name
                        };
            return query.ToList();
        }

        public bool CheckAvailability(int productId, int size, int color)
        {
            var quantity = _productQuantityRepository.FindSingle(x => x.ColorId == color && x.SizeId == size && x.ProductId == productId);
            if (quantity == null)
                return false;
            return quantity.Quantity > 0;
        }

        public int GetAmount(int productId)
        {
            var quantity = _productRepository.FindSingle(x => x.Id == productId);
            return int.Parse(quantity.Unit);
        }

        public List<PurchaseHistoryViewModel> GetPurchaseHistory(Guid id)
        {
            var bills = _billRepository.FindAll();
            var billDetails = _billDetailRepository.FindAll();
            var products = _productRepository.FindAll();

            var query = from b in bills
                        join bd in billDetails
                        on b.Id equals bd.BillId
                        join pd in products
                        on bd.ProductId equals pd.Id
                        where b.CustomerId == id
                        select new PurchaseHistoryViewModel()
                        {
                            ProductName = pd.Name,
                            Image = pd.Image,
                            Price = pd.Price,
                            Quantity = bd.Quantity,
                            DateCreated = b.DateCreated
                        };
            return query.ToList();
        }

        public int CountProduct()
        {
            int count = 0;

            List<ProductViewModel> lst = _mapper.ProjectTo<ProductViewModel>(
                _productRepository.FindAll(x => x.ProductCategory))
                .ToList();
            count = lst.Count();
            return count;
        }

        public int CountProductAmount()
        {
            List<ProductViewModel> quantity = _mapper.ProjectTo < ProductViewModel >(_productRepository.FindAll()).ToList();
            int sum = 0;
            foreach ( ProductViewModel item in quantity)
            {
                sum += int.Parse(item.Unit);

            }
            return sum;
        }

        public void SetUnitProduct(int productId,int quantity)
        {
            var product = _productRepository.FindById(productId);
            product.Unit = (int.Parse(product.Unit) - quantity).ToString();
            _productRepository.Update(product);
            
        }
    }
}