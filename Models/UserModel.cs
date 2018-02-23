using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace login_webapi.Models {
    public class UserModel {
        public UserModel () {
            this.SignUpDate = DateTime.Now;
            this.Id = Guid.NewGuid ();
        }
        public Guid Id { get; set; }

        [Required, MinLength (3, ErrorMessage = "Name too short."), MaxLength (100, ErrorMessage = "Full Name must not exceed 100 characters.")]
        public string FullName { get; set; }

        [Required]
        public DateTime BornDate { get; set; }
        public bool Male { get; set; }

        [Required, MinLength (6, ErrorMessage = "Password must have 6 alphanumeric at least."), MaxLength (15, ErrorMessage = "Password must not exceed 15 alphanumerics.")]
        public string Password { get; set; }

        [Required, EmailAddress (ErrorMessage = "Invalid e-mail format. Correct: myemail@server.com"), MaxLength (60)]
        public string Email { get; set; }
        public DateTime SignUpDate { get; set; }
    }
}