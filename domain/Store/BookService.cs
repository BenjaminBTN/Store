using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class BookService
    {
        private readonly IBookRepository repository;

        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }

        public Book[] GetAllByQuery(string query)
        {
            if(Book.IsIsbn(query))
                return repository.GetAllByIsbn(query);

            return repository.GetAllByTitleOrAuthor(query);
        }
    }
}
