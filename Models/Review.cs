using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class Review : BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }
        public string Reviews { get; set; }

        public int ProductID { get; set; }
        
    }
}
