using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class MagazineRepository:GenericRepository<Magazine>
    {
        public MagazineRepository(string connection) : base(connection) { }

        public MagazineRepository() : base() { }

        public IEnumerable<Magazine> GetRange(int[] listId)
        {
            List<Magazine> result = _db.Magazines.Where(x => listId.Contains(x.MagazineId)).ToList();
            return result;
        }
    }
}
