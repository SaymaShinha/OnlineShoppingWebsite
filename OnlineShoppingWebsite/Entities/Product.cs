namespace OnlineShoppingWebsite.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int ID { get; set; }

        [Column("Admin ID")]
        public int Admin_ID { get; set; }

        [Column("Product Name")]
        [Required]
        [StringLength(150)]
        public string Product_Name { get; set; }

        [Column("Product Department")]
        [Required]
        [StringLength(150)]
        public string Product_Department { get; set; }

        [Column("Product Price")]
        public int Product_Price { get; set; }

        [Column("Product On Stock")]
        public int Product_On_Stock { get; set; }

        [Column("Product Description")]
        [Required]
        [StringLength(1000)]
        public string Product_Description { get; set; }

        public bool IsActivate { get; set; }

        [Column("Product Date")]
        public DateTime Product_Date { get; set; }
    }
}
