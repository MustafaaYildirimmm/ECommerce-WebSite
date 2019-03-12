using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceSample.Models.VM
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [
            EmailAddress(ErrorMessage = "Please enter E-email type"),
            Required(ErrorMessage = "Please Enter Your E-mail Address"),
            DisplayName("E-Mail")
            ]
        public string Email { get; set; }
        [
            Required(ErrorMessage = "Please Enter Your Password"),
            DisplayName("Password")
            ]
        public string Password { get; set; }
    }
}