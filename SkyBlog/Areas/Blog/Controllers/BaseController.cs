using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SkyBlog.Areas.Blog.Controllers
{
    [Area("blog")]
    public class BaseController : Controller
    {
        public string IndexUrl { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (RouteData.Values["index"] != null)
            {
                ViewBag.Index = (string)RouteData.Values["index"];
                IndexUrl = (string)RouteData.Values["index"];
            }
            else
            {
                ViewBag.Index = "zgz";
                IndexUrl = "zgz";
            }
        }


    }
}