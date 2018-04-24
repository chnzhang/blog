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
                return (string)RouteData.Values["index"];
            }
        }
    }
}