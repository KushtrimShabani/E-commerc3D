using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class ProductCategories
    {
        public IEnumerable<E_commerc3D.Models.Product> ProductIndexViewModel { get; set; }
        public IEnumerable<E_commerc3D.Models.Review> ReviewIndexViewModel { get; set; }
        public IEnumerable<E_commerc3D.Models.Categories> CategoriesIndexViewModel { get; set; }
    }
}
