using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SkyBlog.Areas.Blog.Controllers
{
    [Area("blog")]
    public class BaseController : Controller
    {
        protected string IndexUrl
        {
            get
            {
                if (RouteData.Values["index"] != null)
                {
                    TempData["Index"] = (string)RouteData.Values["index"];
                    return (string)RouteData.Values["index"];
                }
                else
                {
                    TempData["Index"] = "zgz";
                    return "zgz";
                }
            }
        }
    }
}