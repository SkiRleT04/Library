using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class BrochureRepository:GenericRepository<Brochure>
    {
        public BrochureRepository(string connection) : base(connection) { }

        public BrochureRepository() : base() { }

        public IEnumerable<Brochure> GetRange(int[] listId)
        {
            List<Brochure> result = _db.Brochures.Where(x => listId.Contains(x.BrochureId)).ToList();
            return result;
        }
    }
}
