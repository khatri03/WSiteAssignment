using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSiteAssignment.Models.DbEntities;

namespace WSiteAssignment.Repository.Abstraction
{
    public interface IEmployeeRepository: IRepository<Employee, int>
    {

    }
}
