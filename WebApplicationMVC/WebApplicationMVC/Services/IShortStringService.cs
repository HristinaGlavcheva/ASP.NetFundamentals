using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC.Services
{
    public interface IShortStringService
    {
        public string GetShort(string str, int maxLength = 10);    
    }
}
