using Library.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationContext(string conectionString) : base(conectionString) { Configuration.ProxyCreationEnabled = false; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Magazine> Magazines { get; set; }
        public virtual DbSet<Brochure> Brochures { get; set; }
        public virtual DbSet<PublicHouse> PublicHouses { get; set; }
        
    }
}
