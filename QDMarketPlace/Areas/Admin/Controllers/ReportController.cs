using Microsoft.AspNetCore.Mvc;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IBillService _billService;
        public ReportController(IBillService billService)
        {
            _billService = billService;
        }
        public IActionResult Index()
        {

            ViewBag.CountBillInMonth = CountInMonth();

           // ViewBag.Total = Total();
            return View();
        }

        public List<DataPoint> CountInMonth()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            List<int> lst = _billService.CountInMonth();
            for (int i = 0; i < 12; i++)
            {
                dataPoints.Add(new DataPoint((i + 1).ToString(), lst[i]));

            }

            return dataPoints;
        }

        public List<int> Total()
        {
            var lst = _billService.TotalMoney();
            int sumQuantity = 0, sumTotal = 0;
            foreach (var item in lst)
            {
                sumQuantity += item.Quantity;
                sumTotal += (int)(item.Quantity * item.Price);
            }
            List<int> lstTotal = new List<int>();
            //lstTotal.Add(lst);
            lstTotal.Add(sumQuantity);
            lstTotal.Add(sumTotal);
            return lstTotal;
        }
    }
}
