using OnlineShoppingWebsite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingWebsite.Areas.WebArea.Models
{
    public class ProductVM
    {

        public int ID { get; set; }

        public int Admin_ID { get; set; }

        public string Product_Name { get; set; }

        public string Product_Department { get; set; }

        public int Product_Price { get; set; }

        public int Product_On_Stock { get; set; }

        public string Product_Description { get; set; }

        public bool IsActivate { get; set; }

        public DateTime Product_Date { get; set; }


        public string MainImagePath { get; set; }

        public List<ProductImage> ImageList { get; set; }

    }


}
