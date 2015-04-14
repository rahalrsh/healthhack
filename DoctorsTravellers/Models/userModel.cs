using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorsTravellers.Models
{

    // class to store user info at Sing Up and Login
    public class userModel
    {
        [Required]
        public string name {get; set;}

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}