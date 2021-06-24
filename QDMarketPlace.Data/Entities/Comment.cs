using QDMarketPlace.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QDMarketPlace.Data.Entities
{
    [Table("Comments")]
    public class Comment:DomainEntity<int>
    {
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
