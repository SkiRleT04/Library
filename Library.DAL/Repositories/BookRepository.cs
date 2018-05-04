using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class BookRepository:GenericRepository<Book>
    {
        public BookRepository(string connection) : base(connection) { }
        public BookRepository() : base() { }

        public IEnumerable<Book> GetRange(int[] listId)
        {
            List<Book> result = _db.Books
                .Include(x => x.PublicHouses)
                .Include(x => x.Authors)
                .Where(x => listId.Contains(x.BookId))
                .ToList();
            return result; 
        }

        public override Book Create(Book book)
        {
            int[] listPublicHoueseId = book.PublicHouses.Select(x => x.PublicHouseId).ToArray();
            int[] listAuthorsId = book.Authors.Select(x => x.AuthorId).ToArray();

            if (listAuthorsId.Length > 0)
            {
                book.Authors = _db.Authors.Where(p => listAuthorsId.Contains(p.AuthorId)).ToList();
            }

            if (listPublicHoueseId.Length > 0)
            {
                book.PublicHouses = _db.PublicHouses.Where(p => listPublicHoueseId.Contains(p.PublicHouseId)).ToList();
            }
            _db.Books.Add(book);
            _db.SaveChanges();
            return book;
        }

        public override void Update(Book book)
        {

            Book bookUpdate = _db.Books
                .Include(p => p.PublicHouses)
                .Include(p => p.Authors)
                .FirstOrDefault(x => x.BookId == book.BookId);

            bookUpdate.Name = book.Name;
            bookUpdate.YearOfPublishing = book.YearOfPublishing;
            bookUpdate.PublicHouses.Clear();
            bookUpdate.Authors.Clear();
            int[] listPublicHoueseId = book.PublicHouses.Select(x => x.PublicHouseId).ToArray();
            int[] listAuthorsId = book.Authors.Select(x => x.AuthorId).ToArray();

            if (listAuthorsId.Length > 0)
            {
                bookUpdate.Authors = _db.Authors.Where(p => listAuthorsId.Contains(p.AuthorId)).ToList();
            }

            if (listPublicHoueseId.Length > 0)
            {
                bookUpdate.PublicHouses = _db.PublicHouses.Where(p => listPublicHoueseId.Contains(p.PublicHouseId)).ToList();
            }
            _db.Books.AddOrUpdate(bookUpdate);
            _db.SaveChanges();
        }
    }
}
