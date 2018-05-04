using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class PublicHouse
    {
        public int PublicHouseId { get; set; }
        public string PublicHouseName { get; set; }
        public string Country { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}
