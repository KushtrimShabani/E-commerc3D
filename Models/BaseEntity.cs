using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class BaseEntity
    {
        [DisplayName("Created By")]
        public string CreateBy { get; set; }
        [DisplayName("Created Data")]
        public DateTime CreateData { get; set; }
        [DisplayName("Updated By")]
        public string UpdateBy { get; set; }
        [DisplayName("Updated Data")]
        public DateTime UpdateData { get; set; }
    }
}
