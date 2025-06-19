using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Response
{
    public class CategoryResponse
    {
        public short CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategoryDesciption { get; set; } = null!;
        public bool? IsActive { get; set; }
        public short? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
    }
}
