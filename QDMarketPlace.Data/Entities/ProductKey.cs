using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using QDMarketPlace.Infrastructure.SharedKernel;

namespace QDMarketPlace.Data.Entities
{
    [Table("ProductKeys")]
    public class ProductKey : DomainEntity<int>
    {
        
        public int ProductId { get; set; }

        public string Key { get; set; }

        public bool Status { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
