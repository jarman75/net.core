using BooksCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BooksMongo
{
    public interface IBookstoreDatabaseSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class BookstoreDatabaseSettings : IBookstoreDatabaseSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }

    public class BookModel
    {

        private readonly Book _book;

        public BookModel()
        {
        }

        public BookModel(Book book)
        {
            _book = book;
            Id = book.Id;
            BookName = book.BookName;
            Price = book.Price;
            Category = book.Category;
            Author = book.Author;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }

        public Book ToBook()
        {
            return new Book { Id = this.Id, Author = this.Author, BookName = this.BookName, Category = this.Category, Price = this.Price };
        }
    }
}
