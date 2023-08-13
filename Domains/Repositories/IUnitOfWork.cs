namespace HealthCareApplication.Domains.Repositories;

public interface IUnitOfWork
{
    public Task<bool> CompleteAsync();
}
