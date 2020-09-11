using Bogus;
using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using System.Linq;

namespace ITeam.DotnetCore.FakeServices
{
    public class FakeCustomerService : FakeEntityService<Customer>, ICustomerService, IAuthorizationService
    {
        public FakeCustomerService(Faker<Customer> customerFaker)
        {
            entities = customerFaker.Generate(100);
        }

        public bool TryAuthenticate(string username, string hashedPassword, out Customer customer)
        {
            customer = entities.SingleOrDefault(c => c.UserName == username && c.HashedPassword == hashedPassword);

            return customer != null;
        }
    }
}
