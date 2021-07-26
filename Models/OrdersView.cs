using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class OrdersView
    {
        public Order OrderCreateViewM { get; set; }
        public IEnumerable<E_commerc3D.Models.Product> ProductIndex { get; set; }
        public Cart CartViewM { get; set; }

        public int Id { get; set; }
        public string CartId { get; set; }
        public string UserID { get; set; }

        public string ContactName { get; set; }
        public int Quantity { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string AddressShipping { get; set; }
        public string AddressBilling { get; set; }
        public string Address { get; set; }

        public string OrderNotes { get; set; }
    }
}
    