using System;

namespace login_webapi.Entities {
    public class User {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BornDate { get; set; }
        public bool Male { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime SignUpDate { get; set; }
    }
}