using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QDMarketPlace.Extensions;
using QDMarketPlace.Application.Dapper.Interfaces;
using QDMarketPlace.Application.Interfaces;
using System.Web.Mvc;
using QDMarketPlace.Areas.Admin.Models;
using Newtonsoft.Json;

namespace QDMarketPlace.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IReportService _reportService;
        private IProductService _productService;
        private readonly IUserService _userService;
        private readonly IBillService _billService;

        public HomeController(IReportService reportService, IProductService productService, IUserService userService, IBillService billService)
        {
            _reportService = reportService;
            _productService = productService;
            _userService = userService;
            _billService = billService;
        }

        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");
            ViewBag.CountUser = _userService.CountUser();

            ViewBag.CountProduct = _productService.CountProduct();
            ViewBag.CountProductAmount = _productService.CountProductAmount();

            ViewBag.CountBill = _billService.CountBill();
            ViewBag.DataPoints = JsonConvert.SerializeObject(CountInMonth());
            ViewBag.CountBillInMonth = CountInMonth();

            ViewBag.TotalMoney = _billService.TotalMoney();
            ViewBag.QuantityTotal = Total()[0];
            ViewBag.Money = Total()[1];

            return View();
        }

        public async Task<IActionResult> GetRevenue(string fromDate, string toDate)
        {
            return new OkObjectResult(await _reportService.GetReportAsync(fromDate, toDate));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        //public Microsoft.AspNetCore.Mvc.JsonResult GetCountInMont()
        //{
        //    var model = _billService.CountInMonth();

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

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
            foreach(var item in lst)
            {
                sumQuantity += item.Quantity;
                sumTotal += (int)(item.Quantity * item.Price);
            }
            List<int> lstTotal = new List<int>();
            lstTotal.Add(sumQuantity);
            lstTotal.Add(sumTotal);
            return lstTotal;
        }
    }
   
}