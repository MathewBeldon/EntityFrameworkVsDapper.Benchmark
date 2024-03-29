﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkVsDapper.Benchmark.Core.Entities.Common
{
    [Index(nameof(Id))]
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? ModifiedDateUtc { get; set; }
    }
}
