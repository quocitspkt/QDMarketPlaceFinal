using QDMarketPlace.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QDMarketPlace.Data.Entities
{
    [Table("Keys")]
    public class Key:DomainEntity<int>
    {
        
        public int ProductId { get; set; }
        public string Value { get; set; }
        public bool Status { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
