using CatalogosMVC.Business.Models;

namespace CatalogosMVC.Business.Services.Interfaces;

public interface IUserService
{
    public Task<bool> Add(UserModel userModel);

    public Task<List<UserModel>> ListAll();

    public Task<UserModel> GetById(int id);

    public Task<bool> Update(UserModel userModel);

    public Task<bool> Delete(UserModel userModel);
}
