using AhorraYa.Abstractions;

namespace AhorraYa.Application.Interfaces
{
    public interface IApplication<T> : IDbOperation<T>
    {
    }
}
