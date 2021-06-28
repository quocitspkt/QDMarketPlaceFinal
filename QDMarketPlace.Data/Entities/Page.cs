using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using QDMarketPlace.Data.Enums;
using QDMarketPlace.Data.Interfaces;
using QDMarketPlace.Infrastructure.SharedKernel;

namespace QDMarketPlace.Data.Entities
{
    [Table("Pages")]
    public class Page : DomainEntity<int>,ISwitchable
    {
        public Page() { }

        public Page(int id, string name, string alias, 
            string content, Status status,DateTime dateCreated)
        {
            Id = id;
            Name = name;
            Alias = alias;
            Content = content;
            Status = status;
            DateCreated = dateCreated;
        }
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
