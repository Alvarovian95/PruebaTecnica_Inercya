using System.Configuration;
using System.Data.Entity;

namespace Customers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString)
        {
        }
        public DbSet<Customer> Customers { get; set; }

    }
}
