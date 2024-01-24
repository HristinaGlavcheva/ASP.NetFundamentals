using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationMVC.Data;

namespace WebApplicationMVC.Controllers
{
    public class DataViewModel
    {
        public int UsersCount { get; set; }

        public string ParameterValue { get; set; }
    }

    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ArticlesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        [HttpPost]
        [ActionName("ById")]
        public IActionResult ByIdWithName(int year, int month, int day, int id, string name, [FromHeader]string cookie)
        {
            this.ViewBag.Test = 1;
            this.ViewData["test"] = 123;
            var viewModel = new DataViewModel();
            viewModel.UsersCount = this.dbContext.Users.Count();
            viewModel.ParameterValue = $"{year}-{month}-{day}";
            return this.View(viewModel);
        }
    }
}
