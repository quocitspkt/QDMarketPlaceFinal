using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using QDMarketPlace.Data.Enums;

namespace QDMarketPlace.Application.ViewModels.Blog
{
    public class PageViewModel
    {
        public int Id { set; get; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }
        public DateTime DateCreated { get; set; }
        public Status Status { set; get; }
    }
}
