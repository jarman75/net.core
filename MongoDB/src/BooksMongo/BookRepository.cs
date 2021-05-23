using System.Diagnostics;
using BooksCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BooksMongo
{
    public class BookRepository : IBooksRepository
    {

        private readonly IMongoCollection<BookModel> _books;

        public BookRepository(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<BookModel>(settings.BooksCollectionName);
        }

        public Book Create(Book book)
        {
            var data = new BookModel(book);
            _books.InsertOne(data);            
            return data.ToBook();
            
        }

        public List<Book> Get()
        {
            var data = _books.Find(book => true).ToList();

            return data.Select(s => s.ToBook()).ToList();
        }

        public Book Get(string id)
        {
            return _books.Find(book => book.Id == id).FirstOrDefault()?.ToBook();
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(string Id)
        {
            _books.DeleteOne(book => book.Id == Id);
        }

        public void Update(string id, Book bookIn)
        {
            _books.ReplaceOne(book => book.Id == id, new BookModel(bookIn));
        }
    }
}
