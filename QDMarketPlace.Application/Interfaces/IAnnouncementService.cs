using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Application.ViewModels.System;
using QDMarketPlace.Data.Entities;
using QDMarketPlace.Infrastructure.Interfaces;
using QDMarketPlace.Utilities.Dtos;

namespace QDMarketPlace.Application.Interfaces
{
    public interface IAnnouncementService
    {
        PagedResult<AnnouncementViewModel> GetAllUnReadPaging(Guid userId, int pageIndex, int pageSize);

        bool MarkAsRead(Guid userId, string id);
    }
}
