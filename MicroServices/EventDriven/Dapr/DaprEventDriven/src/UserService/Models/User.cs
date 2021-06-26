namespace UserService.Models
{
    public class User
    {
        public User(string id)
        {
            Id = id;
        }

        public string Id {get; set;}   
        public string Name {get; set;}
        public string Mail {get; set;}
        public string OtherData {get; set;}
        
    }
}