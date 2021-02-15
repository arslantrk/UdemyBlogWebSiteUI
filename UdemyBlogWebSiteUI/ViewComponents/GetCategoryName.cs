using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogWebSiteUI.ApiServices.Interfaces;

namespace UdemyBlogWebSiteUI.ViewComponents
{
    public class GetCategoryName : ViewComponent
    {
        private readonly ICategoryApiService _categoryApiService;
        public GetCategoryName(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public IViewComponentResult Invoke (int categoryId)
        {//Asenkronik methodu kullanamıcaz oyüzden resultunu alıcaz.
            return View(_categoryApiService.GetByIdAsync(categoryId).Result);
        }
    }
}
