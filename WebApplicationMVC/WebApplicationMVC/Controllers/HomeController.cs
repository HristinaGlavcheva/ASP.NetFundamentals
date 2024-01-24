using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationMVC.Data;
using WebApplicationMVC.Models;
using WebApplicationMVC.Services;
using WebApplicationMVC.ViewModels.Home;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IShortStringService shortStringService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IShortStringService shortStringService)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.shortStringService = shortStringService;
        }

        public IActionResult Index(int id)
        {
            var viewModel = new IndexViewModel
            {
                Id = id,
                Name = "Anonymous",
                //Name = "<script>alert('hacked!!!')</script>",
                Username = this.User.Identity.Name,
                UsersCount = this.dbContext.Users.Count(),
                ProcessorsCount = Environment.ProcessorCount,
                Year = DateTime.UtcNow.Year,
                Description = "Курсът \"ASP.NET Core\" ще ви научи как се изграждат съвременни уеб приложения с архитектурата Model-View-Controller, използвайки HTML5, бази данни, Entity Framework Core и други технологии. Изучава се технологичната платформа ASP.NET Core, нейните компоненти и архитектура, създаването на MVC уеб приложения, дефинирането на модели, изгледи и частични изгледи с Razor view engine, дефиниране на контролери, работа с бази данни и интеграция с Entity Framework Core, LINQ и SQL Server. Курсът включа и по-сложни теми като работа с потребители, роли и сесии, използване на AJAX, кеширане, сигурност на уеб приложенията, уеб сокети и работа с библиотеки от MVC контроли. Обучението включва практически лабораторни упражнения (лабове) и работилници за изграждане на цялостни, пълнофункционални ASP.NET Core уеб приложения. Ще работим и по проект за изграждане на уеб приложение, като курсът завършва с практически изпит по уеб разработка с ASP.NET Core."
            };

            this.ViewData["Name"] = "Tina";
            this.ViewData["Year"] = 2022;
            this.ViewData["UsersCount"] = this.ViewBag.UsersCount;

            this.ViewBag.Name = "Tincheto";
            this.ViewBag.Year = this.ViewData["Year"];
            this.ViewBag.UsersCount = this.dbContext.Users.Count() + 1;
            this.ViewBag.Calc = new Func<int>(() => 3);

            return View(viewModel);
        }

        public IActionResult AjaxDemo()
        {
            return this.View();
        }

        public IActionResult AjaxDemoData()
        {
            return this.Json(new[]
            {
                new{Name = "Niki2", Date=DateTime.UtcNow.ToString("O")},
                new{Name = "Stoyan2", Date=DateTime.UtcNow.AddDays(1).ToString("O")},
                new{Name = "Pesho2", Date=DateTime.UtcNow.AddDays(2).ToString("O")},
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
