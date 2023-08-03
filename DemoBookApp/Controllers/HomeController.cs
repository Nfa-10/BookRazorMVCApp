﻿using DemoBookApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using UserModel = DemoBookApp.Models.UserModel;

namespace DemoBookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDbContext _context;


        public HomeController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Create()
        {

            return View();

        }
        public IActionResult UserNotFound()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("userId, emailOrUsername, password")] UserModel user)
        {
            var listOfUsers = _context.UserList.SingleOrDefault(m => m.emailOrUsername == user.emailOrUsername);
            if (listOfUsers == null)
            {
                ViewBag.Message = "Invalid Credentials ";
                return View(user);
            }
            else
            {
                
                bool isPasswordValid = BC.Verify(user.password,listOfUsers.password);
                if (isPasswordValid)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.emailOrUsername) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props)
                        .Wait();

                    return RedirectToAction("Index", "Book");
                }
                else return RedirectToAction(nameof(UserNotFound));
            }
            
        }
        //Create a User Account
        [HttpPost]
        public async Task<IActionResult> Create([Bind("userId, emailOrUsername, password")] UserModel user)
        {
            if (ModelState.IsValid)
            {
                user.userId = Guid.NewGuid();
                user.password = BC.HashPassword(user.password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        [HttpPost]
        //Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }



    }
}