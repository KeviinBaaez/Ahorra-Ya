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
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void RemoveById(int id)
        {
            _repository.RemoveById(id);
        }

        public T Save(T entity)
        {
            return _repository.Save(entity);
        }
    }
}
