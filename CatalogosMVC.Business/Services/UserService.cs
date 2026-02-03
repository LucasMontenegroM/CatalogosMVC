using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IListRepository _listRepository;
    public UserService(IUserRepository userRepository, IListRepository listRepository)
    {
        _userRepository = userRepository;
        _listRepository = listRepository;
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
        if (entity != null)
        {
            await _userRepository.Add(entity);
            await _userRepository.Commit();

            return true;
        }
        return false;        
    }

    public async Task<UserModel> GetById(int id)
    {
        var entity = await _userRepository.GetById(id);
        if (entity != null)
        {
            var model = UserModel.Map(entity);
            return model;
        }
        return null;
    }

    public async Task<bool> UpdateUser(UserModel user)
    {
        var entity = await _userRepository.GetById(user.Id);
       
        if(entity != null)
        {
            entity.UpdateName(user.Name);

            await _userRepository.Commit();

            return true;
        }
        return false;
    }

    public async Task<bool> Delete(UserModel user)
    {
        var entity = await _userRepository.GetById(user.Id);
        if (entity != null)
        {
            List<ListEntity> listsOfUser = await _listRepository.ListAllOwnedByUser(user.Id);
            foreach(var list in  listsOfUser)
            {
                _listRepository.Delete(list);
            }
            _userRepository.Delete(entity);
            await _userRepository.Commit();
            return true;
        }
        return false;
    }

}
