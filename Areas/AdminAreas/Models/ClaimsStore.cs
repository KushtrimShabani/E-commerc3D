using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_commerc3D.Areas.AdminAreas.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
    {
      
        new Claim("Edit Products","Edit Products"),
        new Claim("Delete Products","Delete Products"),
        new Claim("Create Products", "Create Products"),
        new Claim("Edit Categories","Edit Categories"),
        new Claim("Delete Categories","Delete Categories"),
        new Claim("Edit Order","Edit Order"),
        new Claim("Delete Order","Delete Order")
        
    };
    }
}
