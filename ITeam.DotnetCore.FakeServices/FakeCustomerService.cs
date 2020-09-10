using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;

namespace ITeam.DotnetCore.FakeServices
{
    public class FakeCustomerService : FakeEntityService<Customer>, ICustomerService
    {

    }
}
