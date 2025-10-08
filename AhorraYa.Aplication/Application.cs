using AhorraYa.Repository.Interfaces;
using AhorraYa.Application.Interfaces;

namespace AhorraYa.Application
{
    public class Application<T> : IApplication<T> 
    {
        private IGenericRepository<T> _repository;
        public Application(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public T Save(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
