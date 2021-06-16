using E_commerc3D.Areas.AdminAreas.Models;
using E_commerc3D.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerc3D.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Order> Order { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            string user = httpContextAccessor.HttpContext.User.Identity.Name;
            AddTimestamps(user);
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps(string owner)
        {
            foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreateBy = owner;
                        entry.Entity.CreateData = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdateBy = owner;
                        entry.Entity.UpdateData = DateTime.Now;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var foreigKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreigKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
