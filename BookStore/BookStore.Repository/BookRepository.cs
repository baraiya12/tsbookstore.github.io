using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        BookStoreContext _context = new BookStoreContext();
        public ListResponse<Book> GetBooks(int pageIndex, int pageSize, string keyword)
        {

            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalReocrds = query.Count();
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Book>()
            {
                Results = books,
                TotalRecords = totalReocrds,
            };
        }

        public Book GetBook(int id)
        {
            var book = _context.Books.Where(c => c.Id == id).FirstOrDefault();
            return book;
        }

        public Book AddBook(Book book)
        {
            var entry =  _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            Book book = _context.Books.Where(c => c.Id == id).FirstOrDefault();
            if (book == null)
                return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;            
        }

        public Book UpdateBook(Book book)
        {
            var entity = _context.Books.Update(book);
            _context.SaveChanges();
            return entity.Entity;
        }
    }
}
