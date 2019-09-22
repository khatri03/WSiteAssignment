using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSiteAssignment.Data;
using WSiteAssignment.Repository.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using WSiteAssignment.Repository.Concrete;

namespace WSiteAssignment.Repository.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IEmployeeRepository Employees { get; private set; }

        public UnitOfWork(DataContext context)
        {
            this._context = context;
            this.Employees = new EmployeeRepository(this._context);
        }

        public bool SaveAll()
        {
            return this._context.SaveChanges() > 0;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await this._context.SaveChangesAsync() > 0;
        }
    }
}
