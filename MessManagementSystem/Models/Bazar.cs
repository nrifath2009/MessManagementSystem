using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class Bazar
    {
        public int BazarId { get; set; }
        [Display(Name = "Bazar Date")]
        [Required(ErrorMessage = "Bazar Date Required")]
        public DateTime BazarDate { get; set; }
         [Display(Name = "Member Name")]
         [Required(ErrorMessage = "Select a Member Name")]
         public int MemberId { get; set; }
         [Display(Name = "Bazar Amount")]
         [Required(ErrorMessage = "Bazar Amount Required")]
        public double BazarAmount { get; set; }

        public virtual Member Member { set; get; }



    }
}