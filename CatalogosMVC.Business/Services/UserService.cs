using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserModel>> ListAll()
    {
        var usersEntity = await _userRepository.ListAll();

        return usersEntity.Select(user => {
            return UserModel.Map(user);
        }).ToList();
        
    }
    public async Task<bool> Add(UserModel user)
    {
        UserEntity entity = new UserEntity(user.Name);
        _userRepository.Add(entity);

        return true;
    }

}
