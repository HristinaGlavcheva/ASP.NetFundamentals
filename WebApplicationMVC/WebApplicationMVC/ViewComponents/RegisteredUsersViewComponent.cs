using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationMVC.Data;
using WebApplicationMVC.ViewModels.ViewComponents;

namespace WebApplicationMVC.ViewComponents
{
    public class RegisteredUsersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;

        public RegisteredUsersViewComponent(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IViewComponentResult Invoke(string title)
        {
            var viewModel = new RegisteredUsersViewModel
            {
                Title = title,
                Users = this.dbContext.Users.Count()
            };

            return this.View(viewModel);
        }
    }
}
