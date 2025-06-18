﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.Models;

public partial class Category
{
    public short CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDesciption { get; set; } = null!;

    public short? ParentCategoryId { get; set; }

    public bool? IsActive { get; set; }
    [JsonIgnore]
    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();
    [JsonIgnore]
    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();

    public virtual Category? ParentCategory { get; set; }
}
