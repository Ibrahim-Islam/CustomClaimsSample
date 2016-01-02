using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Claims.Models
{
    public class CustomInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Add Role Groups
            
            var home = new ApplicationRoleGroup
            {
                Name = "Home"
            };

            context.ApplicationRoleGroup.Add(home);

            #endregion


            #region Add Roles

            context.Roles.Add(new ApplicationRole
            {
                Name = "Home_About",
                Description = "Access to About",
                RoleGroup = home
            });

            context.Roles.Add(new ApplicationRole
            {
                Name = "Home_Contact",
                Description = "Access to Contact",
                RoleGroup = home
            });

            context.Roles.Add(new ApplicationRole
            {
                Name = "Home_Permission",
                Description = "Access to Permission",
                RoleGroup = home
            });

            #endregion


            #region Add users

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var admin = new ApplicationUser
            {
                UserName = "admin",
                PasswordHash = userManager.PasswordHasher.HashPassword("123456")
            };

            var user = new ApplicationUser
            {
                UserName = "user",
                PasswordHash = userManager.PasswordHasher.HashPassword("123456")
            };

            userManager.Create(admin);
            userManager.Create(user);

            userManager.AddClaim(admin.Id, new Claim(ClaimTypes.Role, "Home_About"));
            userManager.AddClaim(admin.Id, new Claim(ClaimTypes.Role, "Home_Contact"));
            userManager.AddClaim(admin.Id, new Claim(ClaimTypes.Role, "Home_Permission"));

            userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Home_About"));
            //for demo to use if permission view changes with current user logged in
            userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Home_Permission"));

            #endregion


            context.SaveChanges();

            base.Seed(context);
        }
    }
}