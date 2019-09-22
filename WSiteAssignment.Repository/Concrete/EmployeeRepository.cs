using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSiteAssignment.Data;
using WSiteAssignment.Models.DbEntities;
using WSiteAssignment.Repository.Abstraction;

namespace WSiteAssignment.Repository.Concrete
{
    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context) { }
    }
}
