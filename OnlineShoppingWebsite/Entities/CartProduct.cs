namespace OnlineShoppingWebsite.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartProduct")]
    public partial class CartProduct
    {
        public int ID { get; set; }

        public int ProductID { get; set; }
    }
}
