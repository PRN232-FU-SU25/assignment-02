using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.DTOs.Request
{
    public class NewsArticleRequest
    {
        [Required(ErrorMessage = "News title is required")]
        [StringLength(150, ErrorMessage = "News title cannot exceed 150 characters")]
        public string NewsTitle { get; set; } = null!;

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(300, ErrorMessage = "Headline cannot exceed 300 characters")]
        public string Headline { get; set; } = null!;

        [Required(ErrorMessage = "News content is required")]
        public string NewsContent { get; set; } = null!;

        [StringLength(100, ErrorMessage = "News source cannot exceed 100 characters")]
        public string? NewsSource { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, short.MaxValue, ErrorMessage = "Category ID must be a positive number")]
        public short? CategoryId { get; set; }

        [Required(ErrorMessage = "News status is required")]
        public bool? NewsStatus { get; set; }
    }
}