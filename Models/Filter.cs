using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserManager.Models
{
    [Table("Filter")]
    public partial class Filter
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        public bool HasChild { get; set; }
        [StringLength(50)]
        public string FilteredColumn { get; set; }
        public long? Father { get; set; }
        public long? Order { get; set; }

        [NotMapped]
        public IEnumerable<Filter> Children { get; set;}
    }
}