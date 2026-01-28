using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<UserEntity>> ListAll();
    public Task<UserEntity> Add();
}
