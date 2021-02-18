using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyBlogWebSiteUI.Models
{
    public class BlogAddModel
    {
        public int AppUserId { get; set; }
        [Required(ErrorMessage ="Başlık alanı gereklidir")]
        [Display(Name = "Başlık :")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Kısa açıklama alanı gereklidir")]
        [Display(Name = "Kısa Açıklama :")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Açıklama alanı gereklidir")]
        [Display(Name = "Açıklama :")]
        public string ShortDescription { get; set; }
        [Display(Name ="Resim Seçiniz :")]
        public IFormFile Image { get; set; }
    }
}
