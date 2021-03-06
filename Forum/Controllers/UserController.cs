﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Forum.Models;
using Forum.Services;
using Forum.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IWebHostEnvironment environment;

        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            this.userService = userService;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Index()
        {
            string username = User.Identity.Name;

            User user = userService.GetByUsername(username);

            return View(new UserViewModel(user));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create()
        {
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public IActionResult Upload(IFormFile Image)
        {
            var filePath = Path.Combine(environment.WebRootPath, "img", Path.GetRandomFileName());
            filePath = Path.ChangeExtension(filePath, Path.GetExtension(Image.FileName));

            using (var stream = System.IO.File.Create(filePath))
            {
                Image.CopyTo(stream);
            }

            User user = userService.GetByUsername(User.Identity.Name);
            user.Profile.ImagePath = "/img/" + Path.GetFileName(filePath);
            userService.UpdateProfile(user.Profile);

            return RedirectToAction("Index", "User");

        }

        [HttpPost]
        [Authorize]
      
        public IActionResult UpdateProfile(UserViewModel userView)
        {
            User user = userService.GetByUsername(User.Identity.Name);
            user.Profile.Name = userView.Name;
            user.Profile.About = userView.About;
            userService.Update(user);

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public IActionResult UpdateUsername(UserViewModel userView)
        {
            userService.ChangeUsername(User.Identity.Name, userView.Username);

            return RedirectToAction("Logout", "Account");
        }

        public IActionResult Details(int id)
        {
            User user = userService.Read(id);
            return View(new UserViewModel(user));
        }



    }
}
