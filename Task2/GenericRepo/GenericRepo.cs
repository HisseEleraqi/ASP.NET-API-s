using System.Linq.Expressions;
using System.Reflection;
using Task2.IRepo;
using Task2.Models;

namespace Task2.GenericRepo
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        private readonly ITIContext _db;
        public GenericRepo(ITIContext db)
        {
            _db = db;
        }

        public List<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public List<TEntity> GetByPage(int page, int pageSize)
        {
            return _db.Set<TEntity>().Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<TEntity> Search<TEntity>(string propertyName, string value) where TEntity : class
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, propertyName);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var valueExpression = Expression.Constant(value, typeof(string));
            var containsExpression = Expression.Call(property, containsMethod, valueExpression);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);

            return _db.Set<TEntity>().Where(lambda).ToList();
        }








    }

}

