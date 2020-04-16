namespace OnlineShoppingWebsite.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string SurName { get; set; }

        [Required]
        [StringLength(150)]
        public string EMail { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
