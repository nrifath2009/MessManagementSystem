using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessManagementSystem.ViewModel
{
    public class ShowBillViewModel
    {
        public string MemberName { get; set; }
        public double TotalBazar { get; set; }
        public double TotalMeal { get; set; }
        public double Bill { get; set; }
        public string Status { set; get; }
    }
}