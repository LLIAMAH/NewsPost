using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPost.Data.Entities;

namespace NewsPost.Areas.Admin.Models
{
    [Area("Admin")]
    public class UserData
    {
        [Key]
        public string Id => this.User.Id;

        public ApplicationUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }

        [Display(Name = "Roles")]
        public string RolesAsString
        {
            get
            {
                var roles = this.Roles.Select(o => o.Name).ToList();
                return string.Join(',', roles);
            }
        }

        public UserData()
        {
            this.Roles = new List<IdentityRole>();
        }

        public UserData(ApplicationUser user) : this()
        {
            this.User = user;
        }

        public void AddRole(IdentityRole role)
        {
            if (role != null)
                this.Roles.Add(role);
        }
    }
}
