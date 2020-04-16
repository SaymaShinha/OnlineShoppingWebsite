namespace OnlineShoppingWebsite.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImage
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public Guid Guid { get; set; }

        [Required]
        [StringLength(500)]
        public string Image_Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Extension { get; set; }
    }
}
