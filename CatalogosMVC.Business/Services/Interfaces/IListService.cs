using CatalogosMVC.Business.Models;
using Microsoft.AspNetCore.Http;

namespace CatalogosMVC.Business.Services.Interfaces;

public interface IListService
{
    public Task<List<ListModel>> ListAllOwnedByUser(int idUser);

    public Task<ListModel> GetById(int id);

    public Task<bool> AddList(ListModel list, int userId, IFormFile image);

    public Task<bool> Update(ListModel list, IFormFile picture);

    public Task<bool> Delete(ListModel list);
}
