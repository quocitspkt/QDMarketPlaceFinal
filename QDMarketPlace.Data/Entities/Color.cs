﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using QDMarketPlace.Infrastructure.SharedKernel;

namespace QDMarketPlace.Data.Entities
{
    [Table("Colors")]
    public class Color : DomainEntity<int>
    {

        [StringLength(250)]
        public string NameColors
        {
            get; set;
        }

        [StringLength(250)]
        public string Code { get; set; }
    }
}
