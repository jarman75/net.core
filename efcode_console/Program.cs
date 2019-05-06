using System;
using System.Linq;
using efcode_console.Context;
using Microsoft.EntityFrameworkCore;

namespace efcode_console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var key = new ConsoleKeyInfo();
            do 
            {
                Console.WriteLine("l (list), u (change url) and e (exit)");
            
                key = Console.ReadKey();

                switch (key.KeyChar){
                    case 'l':
                        ListAll();
                        break;
                    case 'u':
                        ChangeUrl();
                        break;
                    case 'e':
                        return;
                }                 
                
                
            } while (key.KeyChar != 'e');

        }

        private static void ChangeUrl()
        {
            Console.Write("Refactoring: Improving the Design of Existing Code WebUrl > ");
            var newWebUrl = Console.ReadLine();

            using (var db = new LibraryContext()){

                var book = db.Books.Include(x => x.Author)
                .Single(x => x.Title == "Refactoring: Improving the Design of Existing Code");

                book.Author.Weburl = newWebUrl;
                db.SaveChanges();

                Console.WriteLine("... SavedChanges called.");
                
            }

            ListAll();
        }

        public static void ListAll(){
            using (var db = new LibraryContext())
            {
                foreach( var book in db.Books.AsNoTracking().Include(a => a.Author))
                {
                    var weburl = book.Author.Weburl == null ? 
                    " - no web URL given - "
                    : book.Author.Weburl;

                    Console.WriteLine($"{book.Title} by {book.Author.Name}");
                    Console.WriteLine("Published On: " + $"{book.PublishedOn:dd-MMM-yyyy}" + $". {weburl}");
                }
            }
        }
    }
}
