namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new []
        {
            new Book(1, "ISBN 0201038013", "D. Knuth", "The Art of Computer Programming", "The Art of Computer Programming (TAOCP) is a comprehensive monograph written by the computer scientist Donald Knuth presenting programming algorithms and their analysis. Volumes 1–5 are intended to represent the central core of computer programming for sequential machines.", 
                     7.19m), 
            new Book(2, "ISBN 0201485672", "M. Fowler", "Refactoring", "Martin Fowler’s guide to reworking bad code into well-structured code", 
                     12.45m), 
            new Book(3, "ISBN 0131101633", "B. Kernighan, D. Ritchie", "C Programming Language", "The book was central to the development and popularization of C and is still widely read and used today. Because the book was co-authored by the original language designer, and because the first edition of the book served for many years as the de facto standard for the language, the book was regarded by many to be the authoritative reference on C.", 
                     14.98m)
        };

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Title.Contains(query) || 
                                       book.Author.Contains(query))
                        .ToArray();
        }

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn)
                        .ToArray();
        }

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }
    }
}
