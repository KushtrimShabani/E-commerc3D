using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class Cart : BaseEntity
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Shipping { get; set; }
        public string City { get; set; }
        public DateTime When { get; set; }
        public int Price { get; set; }

        public  List<CartListcs> CartListcsID = new List<CartListcs>();
    }
}
