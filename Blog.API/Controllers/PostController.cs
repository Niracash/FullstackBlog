﻿using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
