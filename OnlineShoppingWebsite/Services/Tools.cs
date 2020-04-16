using OnlineShoppingWebsite.Areas.WebArea.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace OnlineShoppingWebsite.Services
{
    public class Tools
    {
        public static void SaveAdminInSession(AdminVM adminModel)
        {
            HttpContext.Current.Session["ID"] = adminModel.ID;
            HttpContext.Current.Session["Name"] = adminModel.Name;
            HttpContext.Current.Session["SurName"] = adminModel.SurName;
            HttpContext.Current.Session["Email"] = adminModel.EMail;
        }

        public static void SetCookies(AdminVM adminModel)
        {

            FormsAuthentication.SetAuthCookie(adminModel.EMail, false);
          
        }


        public static AdminVM GetUserFromSession()
        {

            AdminVM adminModel = new AdminVM();

            if (HttpContext.Current.Session["ID"] != null)
            {
                adminModel.ID = (int)HttpContext.Current.Session["ID"];
                adminModel.Name = (string)HttpContext.Current.Session["Name"];
                adminModel.SurName = (string)HttpContext.Current.Session["SurName"];
                adminModel.EMail = (string)HttpContext.Current.Session["Email"];

                return adminModel;
            }

            else
            {
                return null;
            }
        }

        public static void DeleteUserFromSession()
        {
            HttpContext.Current.Session["ID"] = null;
        }

    }
}