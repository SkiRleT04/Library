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
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(string connection) : base(connection) { }
        public AuthorRepository() : base() { }

        public IEnumerable<Author> GetRange(int[] listId)
        {
            List<Author> result = _db.Authors.Include(x => x.Books).Where(x => listId.Contains(x.AuthorId)).ToList();
            return result;
        }

        public override Author Create(Author author)
        {
            int[] listBooksId = author.Books.Select(x => x.BookId).ToArray();
            if (listBooksId.Length > 0)
            {
                author.Books = _db.Books.Where(p => listBooksId.Contains(p.BookId)).ToList();
            }
            _db.Authors.Add(author);
            _db.SaveChanges();
            return author;
        }

        public override void Update(Author author)
        {

            Author authorUpdate = _db.Authors.Include(p => p.Books).FirstOrDefault(x => x.AuthorId == author.AuthorId);

            authorUpdate.DateOfBirth = author.DateOfBirth;
            authorUpdate.FirstName = author.FirstName;
            authorUpdate.LastName = author.LastName;


            authorUpdate.Books.Clear();

            int[] listBooksId = author.Books.Select(x => x.BookId).ToArray();
            if (listBooksId.Length > 0)
            {
                authorUpdate.Books = _db.Books.Where(p => listBooksId.Contains(p.BookId)).ToList();
            }
            _db.Authors.AddOrUpdate(authorUpdate);
            _db.SaveChanges();
        }
        
    }
}
