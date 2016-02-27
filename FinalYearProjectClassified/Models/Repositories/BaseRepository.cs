using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models.Repositories
{
    public interface IDbEntity
    {
        int Id { get; }
    }

    /// <summary>
    /// Generic Abstract Repository
    /// </summary>
    /// <typeparam name="T">T is the Model class</typeparam>
    public abstract class BaseRepository<T> 
        where T : class, IDbEntity
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        protected abstract DbSet<T> Entities { get; }

        public IQueryable<T> Find()
        {
            return this.Entities;
        }

        public T FindById(int id)
        {
            return this.Find()
                .FirstOrDefault(x => x.Id == id);
        }

        public virtual T Save(T entity)
        {
            if (entity.Id <= 0)
            {
                this.Entities.Add(entity);
            }
            else
            {
                this._context.Entry(entity).State = EntityState.Modified;
            }

            this._context.SaveChanges();

            return entity;
        }
    }
}