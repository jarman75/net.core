using Microsoft.AspNetCore.Identity;
using User.Domain.ValueObjects;

namespace User.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(ShortName name, Email email, Password password)
        {
            this.UserName = name.ToString();
            this.Email.ToString();                        
        }
    }
}
