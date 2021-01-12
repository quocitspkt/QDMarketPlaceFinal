using Microsoft.AspNetCore.Mvc;
using QDMarketPlace.Application.Interfaces;
using QDMarketPlace.Application.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Areas.Admin.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly INotification _notification;
        private readonly IUserService _userService;
        public NotificationController(INotification notification, IUserService userService)
        {
            _notification = notification;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Send(string subject, string body)
        {
            List<AppUserViewModel>  lstUser =await _userService.GetAllAsync();
            for (int i = 0; i < lstUser.Count; i++)
            {
                _notification.SendNotification(subject, body,lstUser[i].Email.Trim().ToString());
            }
            
            string str = "Thành công";

            return new ObjectResult(str);
        }

    }
}
