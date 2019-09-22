using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WSiteAssignment.Data;
using WSiteAssignment.Models.DbEntities;
using WSiteAssignment.Repository.Abstraction;

namespace WSiteAssignment.Repository.Concrete
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        protected DataContext _context { get; }

        public Repository(DataContext context)
        {
            this._context = context;
        }

        public virtual T Get(TKey Id)
        {
            return this._context.Set<T>().Find(Id);
        }
        public async virtual Task<T> GetAsync(TKey Id)
        {
            var entity = await this._context.Set<T>().FindAsync(Id);
            return entity;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return this._context.Set<T>();
        }
        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            var result = this._context.Set<T>().ToAsyncEnumerable();
            return await this._context.Set<T>().ToListAsync();
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this._context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            this._context.Set<T>().Add(entity);
        }
        public virtual void AddRange(IEnumerable<T> entities)
        {
            this._context.Set<T>().AddRange(entities);
        }

        public virtual void Remove(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            var existing = this._context.Set<T>().Find(entity.Id);
            if(existing != null)
            {
                this._context.Entry(existing).State = EntityState.Detached;
                this._context.Set<T>().Attach(entity);
                var entry = this._context.Entry(entity);
                entry.State = EntityState.Modified;
            }
        }
    }
}
