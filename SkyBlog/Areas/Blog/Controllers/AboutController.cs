﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SkyBlog.Areas.Blog.Controllers
{
    public class AboutController : BaseController
    {
        public IActionResult Index()
        {

            return View(userBlogTemplate+"About/Index.cshtml");
        }
    }
}