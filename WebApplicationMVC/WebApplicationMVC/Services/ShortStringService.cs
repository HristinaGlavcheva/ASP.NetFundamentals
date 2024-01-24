using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC.Services
{
    public class ShortStringService : IShortStringService
    {
        public string GetShort(string str, int maxLength = 10)
        {
            if(str == null)
            {
                return str;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }
            
            return str.Substring(0, maxLength) + "...";
        }
    }
}
