using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<UserEntity>> ListAll();

    public Task<UserEntity> GetById(int id);

    public void Add(UserEntity userEntity);

    public void Delete(UserEntity userEntity);

    public Task Commit();
}
