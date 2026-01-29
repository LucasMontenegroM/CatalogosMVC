using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<UserEntity>> ListAll();

    public Task<UserEntity> GetById(int id);

    public Task Add(UserEntity user);

    public void Delete(UserEntity user);

    public Task Commit();
}
