using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC.ViewModels.Home
{
    public class IndexViewModel
    {
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Range(2000, 2100)]
        public int Year { get; set; }

        public int UsersCount { get; set; }

        public int ProcessorsCount { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int Id { get; set; }

    }
}
