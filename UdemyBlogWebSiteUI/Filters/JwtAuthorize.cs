using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UdemyBlogWebSiteUI.Extensions;
using UdemyBlogWebSiteUI.Models;

namespace UdemyBlogWebSiteUI.Filters
{
    public class JwtAuthorize : ActionFilterAttribute//filters olabilmesi için .
    {
        public override void OnActionExecuted(ActionExecutedContext context)//ilgili action çalışmadan önceki çalışıcak method.
        {//kontrol ediceğim işlemler
            var token = context.HttpContext.Session.GetString("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new RedirectToActionResult("SignIn","Account",null);
            }
            else
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = httpClient.GetAsync("http://localhost:51921/api/Auth/ActiveUser").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var activeUser = JsonConvert.DeserializeObject<AppUserViewModel>
                        (responseMessage.Content.ReadAsStringAsync().Result);

                    context.HttpContext.Session.SetObject("activeUser", activeUser);
                }
                else
                {
                    context.Result = new RedirectToActionResult("SignIn", "Account", null);
                }
            }
        }
    }
}
