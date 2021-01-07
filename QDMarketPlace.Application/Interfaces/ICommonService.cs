using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Application.ViewModels.Common;

namespace QDMarketPlace.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        List<SlideViewModel> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
