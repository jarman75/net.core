using System;
using Xunit;
using librarybusiness;

namespace librarytest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var book = new Book();
            book.BookId = 1;
            book.Title = "Nuevo libro";
        }
    }
}
