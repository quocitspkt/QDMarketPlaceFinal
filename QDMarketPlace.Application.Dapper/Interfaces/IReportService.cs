using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QDMarketPlace.Application.Dapper.ViewModels;

namespace QDMarketPlace.Application.Dapper.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<RevenueReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}
