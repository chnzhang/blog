using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SkyBlog.Areas.Blog.Controllers
{
    public class NewsController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.Index = IndexUrl;
            return View();
        }
    }
}