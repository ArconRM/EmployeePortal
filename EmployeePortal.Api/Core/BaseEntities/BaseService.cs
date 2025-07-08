using EmployeePortal.Api.Core.Interfaces;

namespace EmployeePortal.Api.Core.BaseEntities;

public class BaseService<T>: IService<T> where T: class
{
    private readonly IRepository<T> _repository;

    public BaseService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T> CreateAsync(T entity, CancellationToken token)
    {
        return await _repository.CreateAsync(entity, token);
    }

    public async Task<T> GetAsync(Guid uuid, CancellationToken token)
    {
        return await _repository.GetAsync(uuid, token);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token)
    {
        return await _repository.GetAllAsync(token);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken token)
    {
        return await _repository.UpdateAsync(entity, token);
    }

    public async Task DeleteAsync(Guid uuid, CancellationToken token)
    {
        await _repository.DeleteAsync(uuid, token);
    }
}