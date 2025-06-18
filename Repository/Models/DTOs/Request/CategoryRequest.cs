using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.DTOs.Request
{
    public class CategoryRequest
    {
        public string CategoryName { get; set; } = null!;
        public string CategoryDesciption { get; set; } = null!;
        public short? ParentCategoryId { get; set; } 
        public bool? IsActive { get; set; }
    }
}
