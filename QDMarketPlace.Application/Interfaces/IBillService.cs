﻿using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Application.ViewModels.Product;
using QDMarketPlace.Data.Enums;
using QDMarketPlace.Utilities.Dtos;

namespace QDMarketPlace.Application.Interfaces
{
    public interface IBillService
    {
        void Create(BillViewModel billVm);
        void Update(BillViewModel billVm);

        PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        BillViewModel GetDetail(int billId);

        BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm);

        void DeleteDetail(int productId, int billId, int colorId, int sizeId);

        void UpdateStatus(int orderId, BillStatus status);

        List<BillDetailViewModel> GetBillDetails(int billId);

        List<ColorViewModel> GetColors();

        List<SizeViewModel> GetSizes();

        ColorViewModel GetColor(int id);

        SizeViewModel GetSize(int id);

        void Save();

        int CountBill();

        List<int> CountInMonth();

        List<BillDetailViewModel> TotalMoney();
    }
}
