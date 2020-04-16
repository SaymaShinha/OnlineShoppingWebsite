using OnlineShoppingWebsite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using Newtonsoft.Json;
using OnlineShoppingWebsite.Areas.WebArea.Models;
using OnlineShoppingWebsite.Services;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Web.Security;

namespace OnlineShoppingWebsite.Areas.WebArea.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        // GET: WebArea/Admin
        public ActionResult Index()
        {
            AdminVM adminModel = Tools.GetUserFromSession();

            if (adminModel != null)
            {
                DataContext db = new DataContext();
                return View(adminModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddNewProduct()
        {
                return View(new ProductVM());      
        }

        public String GetProductList()
        {
            DataContext db = new DataContext();

            AdminVM adminModel = Tools.GetUserFromSession();

            if(adminModel != null)
            {
                List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Admin_ID == adminModel.ID)
                                        select new ProductVM
                                        {
                                            ID = product.ID,
                                            Product_Name = product.Product_Name,
                                            Admin_ID = product.Admin_ID,
                                            Product_Date = product.Product_Date,
                                            Product_Department = product.Product_Department,
                                            Product_Description = product.Product_Description,
                                            Product_On_Stock = product.Product_On_Stock,
                                            Product_Price = product.Product_Price,
                                            ImageList = db.ProductImages.Where(x => x.ProductID == product.ID).ToList()
                                        }).ToList();

                return JsonConvert.SerializeObject(list);
            }

            else
            {
                return "0";
            }
        }

        public void DeleteProduct(int id)
        {
            AdminVM adminModel = Tools.GetUserFromSession();
            if (adminModel != null)
            {
                DataContext db = new DataContext();

                Entities.Product parameter = db.Products.Where(x => x.ID == id).FirstOrDefault();
                parameter.IsActivate = false;
                //db.CartProducts.Remove(db.CartProducts.Where(x => x.ProductID == id).FirstOrDefault());
                db.SaveChanges();
            }
            else
            {
                return;
            }
        }

  

        //public int UpdateProduct(int id, string name, string department, int price, int stock, string description)
        //{
        //    DataContext db = new DataContext();
        //    AdminVM adminModel = Tools.GetUserFromSession();

        //    if(adminModel != null)
        //    {
        //        Product newproduct = new Product()
        //        {
        //            ID = id,
        //            Admin_ID = adminModel.ID,
        //            Product_Name = name,
        //            Product_Department = department,
        //            Product_Price = price,
        //            Product_On_Stock = stock,
        //            Product_Description = description,
        //            Product_Date = DateTime.Now,
        //            IsActivate = true
        //        };

        //        db.SaveChanges();
        //        return id;
        //    }

        //    else
        //    {
        //        return 0;
        //    }
        //}

        [HttpPost]
        public ActionResult AddProductImage(int id, IEnumerable<HttpPostedFileBase> files)
        {
            DataContext db = new DataContext();

            foreach (HttpPostedFileBase file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                ProductImage Image = new ProductImage()
                {
                    Image_Name = fileName,
                    Extension = Path.GetExtension(fileName),
                    Guid = Guid.NewGuid(),
                    ProductID = id
                };

                db.ProductImages.Add(Image);
                db.SaveChanges();
                var path = Path.Combine(Server.MapPath("~/Image/ProductImages"), Image.Guid + Image.Extension);
                file.SaveAs(path);

            }

            return RedirectToAction("Index", "Admin");
        }

         public ActionResult UpdateProductImage(int id, IEnumerable<HttpPostedFileBase> files)
        {
            DataContext db = new DataContext();
            db.ProductImages.Remove(db.ProductImages.Where(x => x.ProductID == id).FirstOrDefault());

            if(files != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    ProductImage Image = new ProductImage()
                    {
                        Image_Name = fileName,
                        Extension = Path.GetExtension(fileName),
                        Guid = Guid.NewGuid(),
                        ProductID = id
                    };

                    db.ProductImages.Add(Image);
                    db.SaveChanges();
                    var path = Path.Combine(Server.MapPath("~/Image/ProductImages"), Image.Guid + Image.Extension);
                    file.SaveAs(path);

                }
            }

            return RedirectToAction("Index", "Admin");
        }

        public int AddProduct(string name, string department, int price, int stock, string description)
        {
            DataContext db = new DataContext();
            AdminVM adminModel = Tools.GetUserFromSession();

            if(adminModel != null)
            {
                    Product newproduct = new Product()
                    {
                        Admin_ID = adminModel.ID,
                        Product_Name = name,
                        Product_Department = department,
                        Product_Price = price,
                        Product_On_Stock = stock,
                        Product_Description = description,
                        Product_Date = DateTime.Now,
                        IsActivate = true
                    };

                    db.Products.Add(newproduct);
                    db.SaveChanges();
                    return newproduct.ID;
            }

            else
            {
                return 0;
            }

        }

        public void Update_Product(int id, string name, string department, int price, int stock, string description)
        {
            DataContext db = new DataContext();
            AdminVM adminModel = Tools.GetUserFromSession();

            if (adminModel != null)
            {
                Product newproduct = db.Products.Where(x => x.ID == id).FirstOrDefault();

                newproduct.Product_Name = name;
                newproduct.Product_Department = department;
                newproduct.Product_Price = price;
                newproduct.Product_On_Stock = stock;
                newproduct.Product_Description = description;
                newproduct.Product_Date = DateTime.Now;

                db.SaveChanges();
            }
        }

        public ActionResult UpdateProduct(int id)
        {
            DataContext db = new DataContext();

            ProductVM productVM = (from product in db.Products.Where(x => x.IsActivate == true && x.ID == id)
                                   select new ProductVM
                                   {
                                       ID = product.ID,
                                       Product_Name = product.Product_Name,
                                       Admin_ID = product.Admin_ID,
                                       Product_Date = product.Product_Date,
                                       Product_Department = product.Product_Department,
                                       Product_Description = product.Product_Description,
                                       Product_On_Stock = product.Product_On_Stock,
                                       Product_Price = product.Product_Price,
                                       ImageList = db.ProductImages.Where(x => x.ProductID == id).ToList()
                                   }).FirstOrDefault();
            return View(productVM);
        }

        public bool IsLogIn()
        {
            AdminVM adminModel = Tools.GetUserFromSession();

            if(adminModel != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.SetAuthCookie(login.UserName.ToString(), false);
            FormsAuthentication.SignOut();

            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();


            // delete user from session
            Tools.DeleteUserFromSession();
            return RedirectToAction("Index", "Home");

        }
    }
}
