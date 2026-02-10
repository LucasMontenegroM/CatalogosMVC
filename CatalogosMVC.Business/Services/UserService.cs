using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly IBookRepository _bookRepository;

    public UserService(IUserRepository userRepository, IBookRepository bookRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }

    //change this later
    public async Task<List<UserModel>> ListAll()
    {
        var usersEntity = await _userRepository.ListAll();

        return usersEntity.Select(user => {
            return UserModel.Map(user);
        }).ToList();
        
    }
    public async Task<bool> Add(UserModel userModel)
    {
        UserEntity userEntity = new UserEntity(userModel.Name);

        if (userEntity != null && !string.IsNullOrWhiteSpace(userModel.Name))
        {
            _userRepository.Add(userEntity);

            await _userRepository.Commit();

            return true;
        }
        return false;        
    }

    public async Task<UserModel> GetById(int id)
    {
        if(id <= 0)
        {
            return null;
        }

        var userEntity = await _userRepository.GetById(id);
        if (userEntity != null)
        {
            var model = UserModel.Map(userEntity);
            return model;
        }
        return null;
    }

    public async Task<bool> Update(UserModel userModel)
    {
        var userEntity = await _userRepository.GetById(userModel.Id);

        if (userEntity != null && !string.IsNullOrWhiteSpace(userModel.Name))
        {
            userEntity.UpdateName(userModel.Name);

            await _userRepository.Commit();

            return true;
        }
        return false;
    }

    public async Task<bool> Delete(UserModel userModel)
    {
        var userEntity = await _userRepository.GetById(userModel.Id);

        if (userEntity != null)
        {
            List<BookEntity> listOfUsers = await _bookRepository.ListAllOwnedByUser(userModel.Id);

            foreach(var bookEntity in listOfUsers)
            {
                _bookRepository.Delete(bookEntity);
            }

            _userRepository.Delete(userEntity);

            await _userRepository.Commit();

            return true;
        }

        return false;
    }

}
