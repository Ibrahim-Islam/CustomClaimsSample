using Claims.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Claims.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [ClaimsAuthorize(Claim = "Home_About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [ClaimsAuthorize(Claim = "Home_Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [ClaimsAuthorize(Claim = "Home_Permission")]
        public ActionResult Permission()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
            var principal = HttpContext.User.Identity as ClaimsIdentity;
            var userid = principal.GetUserId();

            var vm = new List<ApplicationRoleViewModel>();

            using (var context = new ApplicationDbContext())
            {
                var allroles = context.Roles.ToList();
                
                var userClaims = userManager.GetClaims(userid).Select(c => new ApplicationRole
                {
                    Name = c.Value
                });

                foreach (var role in allroles)
                {
                    var appRole = role as ApplicationRole;

                    var temp = new ApplicationRoleViewModel
                    {
                        Id = appRole.Id,
                        Description = appRole.Description,
                        Name = appRole.Name,
                        RoleGroup = appRole.RoleGroup
                    };

                    temp.IsChecked = userClaims.Any(a => a.Name == appRole.Name) ?  true : false;

                    vm.Add(temp);
                }

                var groupedvm = vm.GroupBy(k => k.RoleGroup).ToList();

                return View(groupedvm);
            }
        }


        [HttpPost]
        public void SavePermission(IEnumerable<string> selected)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
            var principal = HttpContext.User.Identity as ClaimsIdentity;
            var userid = principal.GetUserId();

            using (var context = new ApplicationDbContext())
            {
                var allroles = context.Roles.ToList();
                var userClaims = userManager.GetClaims(userid).ToList();

                //remove all existing user claims
                foreach (var claim in userClaims)
                {
                    userManager.RemoveClaim(userid, claim);
                }

                //add new selected claims
                foreach (var roleid in selected)
                {
                    var role = allroles.Single(r => r.Id == roleid);
                    userManager.AddClaim(userid, new Claim(ClaimTypes.Role, role.Name));
                }
            }
        }
    }
}