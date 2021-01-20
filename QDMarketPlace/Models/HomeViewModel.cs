using System.Collections.Generic;
using QDMarketPlace.Application.ViewModels.Blog;
using QDMarketPlace.Application.ViewModels.Common;
using QDMarketPlace.Application.ViewModels.Product;

namespace QDMarketPlace.Models
{
    public class HomeViewModel
    {
        public List<BlogViewModel> LastestBlogs { get; set; }
        public List<SlideViewModel> HomeSlides { get; set; }
        public List<ProductViewModel> HotProducts { get; set; }
        public List<ProductViewModel> TopSellProducts { get; set; }

        public List<ProductCategoryViewModel> HomeCategories { set; get; }
        public List<ProductViewModel> TopGames { get; set; }
        public List<ProductViewModel> TopMicorsofts { get; set; }
        public List<ProductViewModel> TopSecurities { get; set; }


        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}
