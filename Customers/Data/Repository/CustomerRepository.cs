using Customers.Data.Repository.IRepository;
using System.Data.Entity;

namespace Customers.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {        
        public void Add(Customer customer)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }
    }
}
