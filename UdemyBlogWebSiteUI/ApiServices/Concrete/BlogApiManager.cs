using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UdemyBlogWebSiteUI.ApiServices.Interfaces;
using UdemyBlogWebSiteUI.Models;

namespace UdemyBlogWebSiteUI.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContextAccessor _httpContextAccessor;
        public BlogApiManager(HttpClient httpClient, HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:51921/api/blogs/");
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{ id }");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseMessage.Content.ReadAsStringAsync());
                //burdan gelicek string json datayı BlogListModel Çeviriyoruz.
            }
            return null;
        }
        public async Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id)
        {
            var responceMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
            if (responceMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responceMessage.Content.ReadAsStringAsync());

            }
            return null;
        }
        public async Task AddAsync(BlogAddModel blogAddModel)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (blogAddModel.Image != null)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(blogAddModel.Image.FileName);
                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue
                    (blogAddModel.Image.ContentType);

                formData.Add(byteContent, nameof(BlogAddModel.Image), blogAddModel.Image.FileName);
                formData.Add(new StringContent
                    (blogAddModel.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
                formData.Add(new StringContent
                    (blogAddModel.ShortDescription), nameof(BlogAddModel.ShortDescription));

            }
        }
    }
}
