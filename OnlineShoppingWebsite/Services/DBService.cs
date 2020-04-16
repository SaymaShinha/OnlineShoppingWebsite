using OnlineShoppingWebsite.Areas.WebArea.Models;
using OnlineShoppingWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShoppingWebsite.Services
{
    public class DBService
    {
        public static String Sign_Up(SignInVM signInModel)
        {
            DataContext db = new DataContext();

            User User = db.Users.Where(x => x.EMail == signInModel.EMail).FirstOrDefault();
            if (User != null)
            {
                return ("Not Valid");
            }

            else
            {
                byte[] passwordbyte = System.Text.Encoding.ASCII.GetBytes(signInModel.Password);
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] passhash = md5.ComputeHash(passwordbyte);
                string finalPass = "";
                for (int i = 0; i < passhash.Length; i++)
                {
                    finalPass += passhash[i];

                }

                User user = new User()
                {
                    Name = signInModel.Name,
                    SurName = signInModel.SurName,
                    EMail = signInModel.EMail,
                    Password = signInModel.Password
                };

                db.Users.Add(user);
                db.SaveChanges();
                return ("Added");
            }
        }

        public static bool log_in(LogInVM logInModel)
        {
            DataContext db = new DataContext();

            byte[] passwordbyte = System.Text.Encoding.ASCII.GetBytes(logInModel.Password);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] passhash = md5.ComputeHash(passwordbyte);
            string finalPass = "";
            for (int i = 0; i < passhash.Length; i++)
            {
                finalPass += passhash[i];
            }
            User login = db.Users.Where(x => x.EMail == logInModel.EMail && x.Password == logInModel.Password).FirstOrDefault();
            if (login != null)
            {
                //save imformation on session
                AdminVM adminmodel = new AdminVM();
                adminmodel.ID = login.ID;
                adminmodel.EMail = login.EMail;
                adminmodel.Name = login.Name;
                adminmodel.SurName = login.SurName;

                Tools.SetCookies(adminmodel);
                Tools.SaveAdminInSession(adminmodel);
                

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}