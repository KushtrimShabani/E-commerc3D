using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class ProductDetails
    {
        public Product ProductsDetailsViewModel { get; set; }
        public CartListcs CartListViewModel { get; set; }
        public IEnumerable<E_commerc3D.Models.Review> ReviewIndexViewModel { get; set; }


        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }
        public string Reviews { get; set; }

        public int ProductID { get ; set; }


        public int Pid { get; set; }

        public int Quantity { get; set; }
    




    }
}
