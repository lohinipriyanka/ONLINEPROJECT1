using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace onlineshopping.Models
{
    public class Logins
    { 
        [Required (AllowEmptyStrings =false, ErrorMessage ="UserID is Required")]
        public int UserID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "User_Name is Required")]
        public int User_Name{ get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "User_Password is Required")]
        public int User_Password { get; set; }
    }
}