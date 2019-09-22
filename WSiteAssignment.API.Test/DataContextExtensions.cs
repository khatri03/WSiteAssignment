using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSiteAssignment.Data;

namespace WSiteAssignment.API.Test
{
    internal static class DataContextExtensions
    {
        public const int RECORDS_COUNT = 10;
        public static void Seed(this DataContext context)
        {
            if (context.Employees == null || context.Employees.ToList().Count > 0)
                return;
            for(int i = 1; i <= RECORDS_COUNT; i++)
            {
                context.Employees.Add(new Models.DbEntities.Employee()
                {
                    Id = i,
                    Email = string.Format("test{0}@test{0}.com", i),
                    LastName = string.Format("Last Name {0}", i),
                    FirstName = string.Format("First Name {0}", i)
                });
            }
            context.SaveChanges();
        }
    }
}
