﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyBlogWebSiteUI.Models
{
    public class CategoryWithBlogsCountModel
    {
        public int BlogsCount { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
