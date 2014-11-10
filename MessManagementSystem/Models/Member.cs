using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessManagementSystem.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [Display(Name = "Joining Date")]
        [Required(ErrorMessage = "Joining Date Required")]
        public DateTime JoiningDate { get; set; }
        [Display(Name = "Member Name")]
        [Required(ErrorMessage = "Member Name Required")]
        public string Name { get; set; }
         [Display(Name = "Email Address")]
         [Required(ErrorMessage = "Email Address Required")]
        [Remote("EmailExists","Member",ErrorMessage = "Email Already Exists")]
        public string Email { get; set; }
         [Display(Name = "Contact No")]
         [Required(ErrorMessage = "Contact No Required")]
         [Remote("ContactNoExists", "Member", ErrorMessage = "Contact No Already Exists")]
        public string ContactNo { get; set; }

        public virtual ICollection<Bazar> Bazars { set; get; }
        public virtual ICollection<Meal> Meals { set; get; }





    }
}