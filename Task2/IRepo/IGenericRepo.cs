using Microsoft.AspNetCore.Mvc;
using Task2.DTO;

namespace Task2.IRepo
{
    public interface IGenericRepo<TEntity>
    {
        public List<TEntity> GetAll();


        public TEntity GetById(int id);

        public void Add(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);
       

             public void Save();


            public List<TEntity> GetByPage(int page, int pageSize);


         public List<TEntity> Search<TEntity>(string propertyName, string value) where TEntity : class;



    }
}
