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
      
        new Claim("Edit Product","Edit Product"),
        new Claim("Delete Product","Delete Product"),
        new Claim("Create Product", "Create Product"),
        new Claim("Edit Seller","Edit Seller"),
        new Claim("Delete Seller","Delete Seller"),
        new Claim("Edit Stock","Edit Stock"),
        new Claim("Delete Stock","Delete Stock")
        
    };
    }
}
