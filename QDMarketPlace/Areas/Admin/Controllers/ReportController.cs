using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
