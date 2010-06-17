using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;
using Relinq.Infrastructure.Server.DataContexts;
using Relinq.Playground.Domain;
using ObjectMeet.Testing.Readable;

namespace Relinq.Playground.DataContexts
{
    public class TestServerDataContext : ServerDataContext, IDataContext
    {
        public TestServerDataContext()
        {
            var companies = new List<Company>();
            companies.Add(new Company { Name = "C2", Description = "Dummy", FoundationDate = new DateTime(2008, 11, 7), Guid = new Guid("4ba1a9c5-9c72-4420-bde5-f3f498cff494") });
            companies.Add(new Company { Name = "C1", Description = "Dummy", FoundationDate = new DateTime(2008, 11, 1) });
            companies.Add(new Company { Name = "C2", Description = "Dummy", FoundationDate = new DateTime(2008, 11, 5) });
            companies[0].Employees.Add(new Employee { FirstName = "Vassily", LastName = "Pupkeen" });
            companies[0].Employees.Add(new Employee { FirstName = "Piotr", LastName = "Tapkin" });
            companies[1].Employees.Add(companies[0].Employees[1]);
            Companies = companies.AsQueryable();

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var readabler = new Readabler(10);
            Companies2 = readabler.Range(10, 30, () => new Company
            {
                Name = readabler.Name(),
                Description = readabler.Description(),
                FoundationDate = readabler.Date(),
                Guid = readabler.Guid(),
            }).AsQueryable();

            Users = readabler.Range(30, 50, () => new User
            {
                BirthDate = readabler.Date(new DateTime(1940, 1, 1), new DateTime(DateTime.Now.Year - 18, 12, 31)),
                FirstName = readabler.Name(),
                LastName = readabler.Name(),
                Id = readabler.Guid(),
                Comment = readabler.Description(),
                IsMale = readabler.Boolean(),
                CompanyId = Companies2.ElementAt(readabler.Int(0, Companies2.Count())).Guid,
           }).AsQueryable();
        }

        public int Dummy { get { return 2; } }
        public IQueryable<Company> Companies { get; private set; }
        public IQueryable<Company> Companies2 { get; private set; }
        public IQueryable<Employee> Employees { get; private set; }
        public IQueryable<User> Users { get; private set; }
    }
}