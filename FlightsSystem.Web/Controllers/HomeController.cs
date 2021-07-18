using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsSystem.Core.Login;
using FlightsSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using FlightsSystem.Web.Infrastructure;

namespace FlightsSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _context;
        private string generatedToken = null;

        public HomeController(IConfiguration config,
                              ITokenService tokenService,
                              IUserRepository userRepository,
                              IHttpContextAccessor context)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login(string username,string password,string role)
        {
            var userModel = new UserModel()
            {
                UserName = username,
                Password = password,
                Role = role
            };

            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }
            IActionResult response = Unauthorized();
            var validUser = GetUser(userModel);

            if (validUser != null)
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"], _config["Jwt:Issuer"], validUser);
                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    HttpContext.Session.SetJson("user", userModel);
                    if (_tokenService.IsTokenValid(_config["Jwt:Key"], _config["Jwt:Issuer"], generatedToken))
                        return Content(generatedToken);
                    else RedirectToAction("Error");
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }

            return RedirectToAction("Error");
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("user");

            return RedirectToPage("/");
        }

        private UserModel GetUser(UserModel userModel)
        {
            return _userRepository.GetUser(userModel);
        }

        public IActionResult Error()
        {
            ViewBag.Message = "An error occured...";
            return View();
        }
    }
}
