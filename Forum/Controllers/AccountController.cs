﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.ViewModel;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;


        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login(bool isError)
        {
            return View(new LoginViewModel { IsError = isError });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            User CreatedUser = userService.CreateUser(viewModel.Username, viewModel.Email, viewModel.Password);
            await Authenticate(CreatedUser);
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            User user = userService.LogIn(viewModel.Username, viewModel.Password);
            await Authenticate(user);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Verify(int id, string verificationString)
        {
            User user = userService.Verify(id, verificationString);
            await Authenticate(user);
            return RedirectToAction("Index", "User");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimTypes.Name, user.Username)
            };

            AddRoles(claims, user);
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync( CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private void AddRoles(List<Claim> claims, User user)
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }


        public IActionResult MakeModerator(int id)
        {
            userService.MakeModerator(id);
            return RedirectToAction("Details", "User", new { id });
        }

        public IActionResult RemoveModerator(int id)
        {
            userService.RemoveModerator(id);
            return RedirectToAction("Details", "User", new { id });
        }


        public IActionResult Ban(int id)
        {
            userService.Ban(id);
            return RedirectToAction("Details", "User", new { id });
        }


        public IActionResult Unban(int id)
        {
            userService.Unban(id);
            return RedirectToAction("Details", "User", new { id });
        }
    }
}