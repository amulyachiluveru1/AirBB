using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AirBB.Models.DataLayer
{
    public class Repository<T> where T : class
    {
        protected AirBnBContext context { get; }
        private DbSet<T> table;

        public Repository(AirBnBContext ctx)
        {
            context = ctx;
            table = context.Set<T>();
        }
        public IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.AsEnumerable().ToList();
        }
        public T? Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }
        public T Get(int id)
        {
            return table.Find(id);
        }
        public T Get(string id)
        {
            return table.Find(id);
        }
        public void Insert(T entity)
        {
            table.Add(entity);
        }
        public void Update(T entity)
        {
            table.Update(entity);
        }
        public void Delete(T entity)
        {
            table.Remove(entity);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public int Count => table.Count();
        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = table;

            // Includes
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            // Where
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }

            // OrderBy
            if (options.HasOrderBy)
            {
                query = options.OrderByDirection == "desc"
                    ? query.OrderByDescending(options.OrderBy)
                    : query.OrderBy(options.OrderBy);
            }

            // Paging
            if (options.HasPaging)
            {
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                             .Take(options.PageSize);
            }

            return query;
        }
    }
}
