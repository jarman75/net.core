using System.Text.RegularExpressions;
using System;


namespace user.component.Entities
{
   public class User {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public Role Role {get; set;}
        public string Password {get; private set;}

        public virtual void ResetPassword() {
            Password = string.Empty;
        }

        public virtual void SetPassword(string password) {
            
            var hasMinimum8Chars = new Regex(@".{8,15}");
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            

            
            if (hasMinimum8Chars.IsMatch(password)) throw new ArgumentOutOfRangeException("Password must include between 8 and 15 chars.");
            if (hasNumber.IsMatch(password)) throw new ArgumentOutOfRangeException("Password must include at least 1 number.");
            if (hasUpperChar.IsMatch(password)) throw new ArgumentOutOfRangeException("Password must include at least 1 upperChar.");

            Password = password;
        }
        public virtual void Save() {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Required value.",nameof(Name));
            if (string.IsNullOrWhiteSpace(Email)) throw new ArgumentException("Required value.",nameof(Email));
            if (Role == null) throw new ArgumentNullException(nameof(Role));
            if (string.IsNullOrWhiteSpace(Role.Name)) throw new ArgumentException("Required value.",nameof(Role));
        }
    }

}