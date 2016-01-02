using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claims.Models
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ApplicationRoleGroup RoleGroup { get; set; }
        public bool IsChecked { get; set; }
    }
}