using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IBookRepository
    {
        Book[] GetAllByTitleOrAuthor(string titlePart);

        Book[] GetAllByIsbn(string isbn);

        Book GetById(int id);

        Book[] GetAllByIds(IEnumerable<int> bookIds);
    }
}
