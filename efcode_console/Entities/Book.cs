namespace efcode_console.Entities
{
    public class Book 
    {
        public int BookId {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public string PublishedOn {get; set;}
        public int AuthorId {get; set;}
        public Author Author {get; set;}
    }   
}
