using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CowabungaBay2.Models
{
    public class UserInput
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public byte[] I9 { get; set; }
        public byte[] PictureID { get; set; }

    }
}