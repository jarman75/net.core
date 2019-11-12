using System;
using Domain.ValueObjects;

namespace User.DataAccess
{
    public class DataUser : Domain.Users.User 
    {        
        
        public DataUser()
        {
        }

        public DataUser(ShortName name, Email email, Password password)
        {
             Id = Guid.NewGuid();
             Name = name;
             Password = password;                           
        }
    }
}
