using System.Linq;
using Relinq.Infrastructure.Client.Beans;
using Relinq.Infrastructure.Client.DataContexts;
using Relinq.Playground.Domain;

namespace Relinq.Playground.DataContexts
{
    public class TestClientDataContext : ClientDataContext, IDataContext
    {
        public TestClientDataContext()
        {
            Companies = (Bean<Company>)GetBean(typeof(Company));
            ((Bean<Company>)Companies).Name = "Companies";

            Companies2 = (Bean<Company>)GetBean(typeof(Company));
            ((Bean<Company>)Companies2).Name = "Companies2";

            Employees = (Bean<Employee>)GetBean(typeof(Employee));
            ((Bean<Employee>)Employees).Name = "Employees";

            Users = (Bean<User>)GetBean(typeof(User));
            ((Bean<User>)Users).Name = "Employees";
        }

        public IQueryable<Company> Companies { get; private set; }
        public IQueryable<Company> Companies2 { get; private set; }
        public IQueryable<Employee> Employees { get; private set; }
        public IQueryable<User> Users { get; private set; }
    }
}