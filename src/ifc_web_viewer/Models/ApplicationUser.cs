using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ifc_web_viewer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(30)]
        [Display(Name = "名前")]
        public string UserShimei { get; set; }

        [MaxLength(30)]
        [Display(Name = "名前（かな）")]
        public string UserShimeiKana { get; set; }
    }
}
