using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class User : IValidatableObject
    {
        public User(string id)
        {
            Id = id;
        }

        
        public string Id {get; set;}   
        
        public string Name {get; set;}
        
        public string Mail {get; set;}
        public string OtherData {get; set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();            

            if (string.IsNullOrWhiteSpace(Id))            
                results.Add(new ValidationResult("User id is required", new[] { "Id" }));

            if (string.IsNullOrWhiteSpace(Name))
                results.Add(new ValidationResult("User name is required", new[] { "Name" }));



            if (string.IsNullOrWhiteSpace(Mail))
            {
                results.Add(new ValidationResult("User mail is required", new[] { "Mail" }));
            }
            else
            {
                System.Net.Mail.MailAddress.TryCreate(Mail, out System.Net.Mail.MailAddress? mail);
                if (mail is null) results.Add(new ValidationResult("Invalid user mail", new[] { "Mail" }));
            }            

            return results;
        }

    }
}