using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingWebsite.Areas.WebArea.Models
{
    public class SignInVM
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}