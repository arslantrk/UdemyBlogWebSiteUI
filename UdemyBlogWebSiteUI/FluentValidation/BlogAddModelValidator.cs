//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UdemyBlogWebSiteUI.Models;

//namespace UdemyBlogWebSiteUI.FluentValidation
//{
//    public class BlogAddModelValidator : AbstractValidator<BlogAddModel>
//    {
//        public BlogAddModelValidator()
//        {
//            RuleFor(I => I.Title).NotEmpty().WithMessage("Başlık alanı boş geçilemez");
//            RuleFor(I => I.ShortDescription).NotEmpty().WithMessage("Kısa Açıklama alanı gereklidir");
//            RuleFor(I => I.Description).NotEmpty().WithMessage("Açıklama alanı gereklidir");
//        }
//    }
//}
