using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Claims.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationRole: IdentityRole
    {
        public virtual ApplicationRoleGroup RoleGroup { get; set; }
        public virtual string Description { get; set; }
    }

    public class ApplicationRoleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroup { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new CustomInitializer());
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");

            modelBuilder.Entity<ApplicationRoleGroup>()
                .HasMany<ApplicationRole>(r => r.Roles)
                .WithRequired(r => r.RoleGroup);

            base.OnModelCreating(modelBuilder);
        }
    }
}