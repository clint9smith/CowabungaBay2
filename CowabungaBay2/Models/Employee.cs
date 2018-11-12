
namespace CowabungaBay2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Employee
    {
        
        public int ID { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        public byte[] I9 { get; set; }
        [Display(Name = "Picture ID")]
        public byte[] PictureID { get; set; }
        [Display(Name = "I9")]
        public string I9Location { get; set; }
        [Display(Name = "Picture ID")]
        public string PictureIDLocation { get; set; }

    }
}
