using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string UserID { get; set; }

        public string ContactName { get; set; }
        public int Quantity { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public string AddressShipping { get; set; }
        public string AddressBilling { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        // public static List<Product> productID = new List<Product>();
        public virtual Product product { get; set; }
    }
}
