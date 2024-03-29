﻿using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Application.ViewModels.Common;
using QDMarketPlace.Application.ViewModels.Product;
using QDMarketPlace.Utilities.Dtos;

namespace QDMarketPlace.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        List<ProductViewModel> GetAll();
        List<ProductViewModel> GetAll(string keyword);
        List<PurchaseHistoryViewModel> GetPurchaseHistory(Guid id);

        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);
        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize,string sortBy);
        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        ProductViewModel GetById(int id);
        int GetAmount(int productId);

        void ImportExcel(string filePath, int categoryId);


        void Save();

        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);
        void AddImages(int productId, string[] images);

        List<ProductImageViewModel> GetImages(int productId);

        void AddWholePrice(int productId, List<ProductKeyViewModel> WholePrices);

        List<ProductKeyViewModel> GetWholePrices(int productId);

        List<ProductViewModel> GetLastest(int top);

        List<ProductViewModel> GetHotProduct(int top);
        List<ProductViewModel> GetRelatedProducts(int id, int top);

        List<ProductViewModel> GetUpsellProducts(int top);

        List<TagViewModel> GetProductTags(int productId);

        bool CheckAvailability(int productId, int size, int color);

        int CountProduct();
        int CountProductAmount();
        void SetUnitProduct(int productId,int quantity);


    }
}
