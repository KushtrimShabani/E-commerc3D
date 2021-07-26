using E_commerc3D.Areas.AdminAreas.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Models
{
    public class Message :BaseEntity
    {
       
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public int UserID { get; set; }
        public virtual  ApplicationUser AppUser { get; set; }

    }
}
