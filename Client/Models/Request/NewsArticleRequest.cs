using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.Request
{
    public class NewsArticleRequest
    {
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = null!;
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public short? CategoryId { get; set; }
        public bool NewsStatus { get; set; }
    }
}
