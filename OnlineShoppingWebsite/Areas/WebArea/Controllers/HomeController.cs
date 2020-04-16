using Newtonsoft.Json;
using OnlineShoppingWebsite.Areas.WebArea.Models;
using OnlineShoppingWebsite.Entities;
using OnlineShoppingWebsite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShoppingWebsite.Areas.WebArea.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        // GET: WebArea/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInVM signInModel)
        {
            String s = DBService.Sign_Up(signInModel);
            if(s.Equals("Not Valid"))
            {
                return View();
            }

            return Redirect("~/WebArea/Home/LogIn");
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInVM logInModel)
        {
            try
            {
                bool s = DBService.log_in(logInModel);

                if(s)
                {
                    return Redirect("~/WebArea/Admin/Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public String GetAllProductList()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0)
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

            if (list != null)
            {
                return JsonConvert.SerializeObject(list);
            }
            else
            {
                return "";
            }
        }

        [HttpGet]
        public ActionResult Product(int id)
        {
            Entities.Product product = db.Products.FirstOrDefault(x => x.IsActivate == true && x.ID == id && x.Product_On_Stock > 0);

            ProductVM productVM = new ProductVM();

            productVM.ID = product.ID;
            productVM.Admin_ID = product.Admin_ID;
            productVM.Product_Name = product.Product_Name;
            productVM.Product_Date = product.Product_Date;
            productVM.Product_Department = product.Product_Department;
            productVM.Product_Description = product.Product_Description;
            productVM.Product_Price = product.Product_Price;
            productVM.Product_On_Stock = product.Product_On_Stock;
            productVM.ImageList = db.ProductImages.Where(x => x.ProductID == product.ID).ToList();


            if (productVM != null)
            {
                return View(productVM);
            }
            else
            {
                return View();
            }
        }

        public int AddProductInCart(int ID)
        {
            DataContext db = new DataContext();
            if(ID != 0)
            {
                CartProduct cartproduct = new CartProduct()
                {
                    ProductID = ID,
                };

                Product prod = db.Products.Where(x => x.ID == ID).FirstOrDefault();
                if(prod.Product_On_Stock > 0)
                {
                    prod.Product_On_Stock = prod.Product_On_Stock - 1;
                    db.CartProducts.Add(cartproduct);
                    db.SaveChanges();
                }

                return db.CartProducts.Count();
            }

            else
            {
                return db.CartProducts.Count();
            }
        }

        public ActionResult Cart()
        {
            return View();
        }

        public string GetCartProductList()
        {
            List<ProductVM> list = (from cart in db.CartProducts
                                   from product in db.Products.Where(x => x.IsActivate == true && x.ID == cart.ProductID )
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

            if (list != null)
            {
                return JsonConvert.SerializeObject(list);
            }

            else
            {
                return "";
            }
        }

        public ActionResult DeleteCartProduct(int id)
        {
            CartProduct cartProduct = db.CartProducts.Where(x => x.ProductID == id).FirstOrDefault();
            db.CartProducts.Remove(cartProduct);

            Product prod = db.Products.Where(x => x.ID == id).FirstOrDefault();
            prod.Product_On_Stock = prod.Product_On_Stock + 1;

            db.SaveChanges();

            return RedirectToAction("Cart", "Home");
        }

        public ActionResult TodaysDeal()
        {
            List<Product> product = db.Products.Where(x => x.IsActivate == true).ToList();
            var productVMs = new List<ProductVM>();

            foreach( var item in product)
            {
                if (item.Product_Date.Date == DateTime.Today.Date)
                {

                    ProductVM productVM = new ProductVM
                    {
                        ID = item.ID,
                        Product_Name = item.Product_Name,
                        Admin_ID = item.Admin_ID,
                        Product_Date = item.Product_Date,
                        Product_Department = item.Product_Department,
                        Product_Description = item.Product_Description,
                        Product_On_Stock = item.Product_On_Stock,
                        Product_Price = item.Product_Price,
                        ImageList = db.ProductImages.Where(x => x.ProductID == item.ID).ToList()
                    };
                    productVMs.Add(productVM);
                }
            }

            return View(productVMs);
        }

        public ActionResult Search(string id)
        {
            string searchText = id;
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && 
                                    ( x.Product_Name.Contains(searchText) || x.Product_Department.Contains(searchText) 
                                    || x.Product_Description.Contains(searchText)))       
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

            
                return View(list);
           

        }

        public ActionResult Prime_Video()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0 
                                    && x.Product_Department == "Prime Video")
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

            return View(list);
        }

        public ActionResult Music_CDs_Crafts()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Music, CDs & Crafts")
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

            return View(list);
        }

        public ActionResult Digital_Music()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Digital Music")
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

            return View(list);
        }

        public ActionResult Kindle_Store()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Kindle Store")
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

            return View(list);
        }

        public ActionResult Arts_Crafts()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Arts & Crafts")
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

            return View(list);
        }

        public ActionResult Automotive()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Automotive")
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

            return View(list);
        }

        public ActionResult Baby()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Baby")
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

            return View(list);
        }

        public ActionResult Beauty_Personal()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Beauty & Personal")
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

            return View(list);
        }

        public ActionResult Books()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Books")
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

            return View(list);
        }

        public ActionResult Computers()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Computers")
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

            return View(list);
        }

        public ActionResult Electronics()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Electronics")
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

            return View(list);
        }

        public ActionResult Womens_Fashion()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Women's Fashion")
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

            return View(list);
        }

        public ActionResult Mens_Fashion()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Men's Fashion")
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

            return View(list);
        }

        public ActionResult Girls_Fashion()
        {
            List<ProductVM> list = (from product in db.Products.Where(x => x.IsActivate == true && x.Product_On_Stock > 0
                                    && x.Product_Department == "Girl's Fashion")
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

            return View(list);
        }

    }
}