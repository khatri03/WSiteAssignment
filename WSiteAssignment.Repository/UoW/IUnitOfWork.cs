using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSiteAssignment.Repository.Abstraction;

namespace WSiteAssignment.Repository.UoW
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get;  }

        bool SaveAll();

        Task<bool> SaveAllAsync();
    }
}
