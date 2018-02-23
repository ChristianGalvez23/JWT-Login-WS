using System;
using System.ComponentModel.DataAnnotations;

namespace login_webapi.Models {
    public class LoginModel {
        [Required, EmailAddress (ErrorMessage = "Invalid e-mail format. Correct: myemail@server.com"), MaxLength (60)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}