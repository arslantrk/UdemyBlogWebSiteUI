using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyBlogWebSiteUI.Models;

namespace UdemyBlogWebSiteUI.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetByIdAsync(int id);
        Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id);
        Task AddAsync(BlogAddModel blogAddModel);
    }
}
