using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class ProductCreateViewModel : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price_buy { get; set; }
        public double Price_sell { get; set; }
        public int Quantity { get; set; }
        public string Measure { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        public IFormFile Photo{ get; set; }

        [ForeignKey("Categories")]
        public int CategoryID { get; set; }
        public virtual Categories Categories { get; set; }
    }
}
