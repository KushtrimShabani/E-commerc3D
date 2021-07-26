using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class CartListViewModel
    {
        public CartListcs CartListViewM { get; set; }
        public IEnumerable<E_commerc3D.Models.Product> ProductIndexViewM { get; set; }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Shipping { get; set; }
        public string City { get; set; }
        public string Idc { get; set; }
        public int Quantityc { get; set; }
        public int ProductIDc { get; set; }
        public DateTime When { get; set; }
        public int Price { get; set; }

        public  List<CartListcs> CartListcsID = new List<CartListcs>();

    }
}
