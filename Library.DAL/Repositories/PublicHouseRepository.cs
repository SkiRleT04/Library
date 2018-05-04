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
    public class PublicHouseRepository:GenericRepository<PublicHouse>
    {
        public PublicHouseRepository(string connection) : base(connection) { }


        public override PublicHouse Create(PublicHouse publicHouse)
        {
            int[] listBooksId = publicHouse.Books.Select(x => x.BookId).ToArray();

            if (listBooksId.Length > 0)
            {
                publicHouse.Books = _db.Books.Where(p => listBooksId.Contains(p.BookId)).ToList();
            }
            _db.PublicHouses.Add(publicHouse);
            _db.SaveChanges();
            return publicHouse;
        }

        public override void Update(PublicHouse publicHouse)
        {
            PublicHouse publicHouseUpdate = _db.PublicHouses.Include(p => p.Books).FirstOrDefault(x => x.PublicHouseId == publicHouse.PublicHouseId);

            publicHouseUpdate.Country = publicHouse.Country;
            publicHouseUpdate.PublicHouseName = publicHouse.PublicHouseName;
            publicHouseUpdate.Books.Clear();


            int[] listBooksId = publicHouse.Books.Select(x => x.BookId).ToArray();

            if (listBooksId.Length > 0)
            {
                publicHouseUpdate.Books = _db.Books.Where(p => listBooksId.Contains(p.BookId)).ToList();
            }
            _db.PublicHouses.AddOrUpdate(publicHouse);
            _db.SaveChanges();
        }


        public IEnumerable<PublicHouse> GetRange(int[] listId)
        {
            List<PublicHouse> result = _db.PublicHouses.Include(x=>x.Books).Where(x => listId.Contains(x.PublicHouseId)).ToList();
            return result;
        }
    }
}
