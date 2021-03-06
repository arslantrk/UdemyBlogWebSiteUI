﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UdemyBlogWebSiteUI.ApiServices.Interfaces;
using UdemyBlogWebSiteUI.Extensions;
using UdemyBlogWebSiteUI.Models;

namespace UdemyBlogWebSiteUI.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
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
            var responceMessage =  await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
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
                //var bytes = new byte[];
                //var bytes = await System.IO.File.ReadAllBytesAsync(blogAddModel.Image.FileName);
                //var bytes = await System.IO.File.ReadAllBytesAsync(blogAddModel.Image.FileName);
                using (var ms = new MemoryStream())
                {
                    await blogAddModel.Image.CopyToAsync(ms);
                    var bytes = ms.ToArray();
                    // string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data

                    ByteArrayContent byteContent = new ByteArrayContent(bytes);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue
                        (blogAddModel.Image.ContentType);

                    formData.Add(byteContent, nameof(BlogAddModel.Image), blogAddModel.Image.FileName);
                }
            }

            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            blogAddModel.AppUserId = user.Id;

            formData.Add(new StringContent
                (blogAddModel.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            formData.Add(new StringContent
                (blogAddModel.ShortDescription), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent
                (blogAddModel.Description), nameof(BlogAddModel.Description));
            formData.Add(new StringContent
                (blogAddModel.Title), nameof(BlogAddModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);

        }
    }
}
