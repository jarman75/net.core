using System.Collections.Generic;

namespace BooksCore
{
    public interface IBooksRepository {
        List<Book> Get();
        Book Get(string id);
        Book Create(Book book);
        void Update(string id, Book bookIn);
        void Remove(Book bookIn);
        void Remove(string Id);

    }
}
