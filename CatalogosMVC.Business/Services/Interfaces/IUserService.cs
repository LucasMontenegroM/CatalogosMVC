using CatalogosMVC.Business.Models;
using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Business.Services.Interfaces;

public interface IUserService
{
    public Task<bool> Add(UserModel user);
    public Task<List<UserModel>> ListAll();
}
