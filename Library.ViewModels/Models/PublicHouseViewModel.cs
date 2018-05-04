using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.Models
{
    public class PublicHouseViewModel
    {
        public int PublicHouseId { get; set; }
        public string PublicHouseName { get; set; }
        public string Country { get; set; }
        public List<BookViewModel> Books { get; set; }

    }
}
