namespace PostService.Models
{
    public class Post
    {
        public string PostId {get; set;}
        public string Title {get; set;}
        public string Content {get; set;}
        public string UserId {get; set;}
        public User User {get; set;}
        
    }
}