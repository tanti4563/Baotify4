using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class RegisterModel
    {
        public String FullName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Repassword { get; set; }
        public String Phone { get; set; }
    }
}