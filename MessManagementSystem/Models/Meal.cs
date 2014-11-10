using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        [Display(Name = "Meal Date")]
        [Required(ErrorMessage = "Meal Date Required")]
        public DateTime MealDate { get; set; }
        [Display(Name = "Member Name")]
        [Required(ErrorMessage = "Select a Member Name")]
        public int MemberId { get; set; }
        [Display(Name = "No of Meal")]
        [Required(ErrorMessage = "Bazar Amount Required")]
        public double NoOfMeal { get; set; }

        public virtual Member Member { set; get; }
    }
}