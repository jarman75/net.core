using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostService.Models
{
    public class Post : IValidatableObject
    {
        public Post(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public string Title {get; set;}
        public string Content {get; set;}
        public string UserId {get; set;}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Id))
                results.Add(new ValidationResult("Post id is required", new[] { "Id" }));

            if (string.IsNullOrWhiteSpace(Title))
                results.Add(new ValidationResult("Post title is required", new[] { "Title" }));

            if (string.IsNullOrWhiteSpace(Content))            
                results.Add(new ValidationResult("Post content is required", new[] { "Content" }));

            if (string.IsNullOrWhiteSpace(UserId))
                results.Add(new ValidationResult("Post user id is required", new[] { "UserId" }));



            return results;
        }
    }
}