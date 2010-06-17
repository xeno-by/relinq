using System.Linq;

namespace Relinq.Playground.Domain
{
    public interface IDataContext
    {
        IQueryable<Company> Companies { get; }
        IQueryable<Company> Companies2 { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<User> Users { get; }
    }
}
