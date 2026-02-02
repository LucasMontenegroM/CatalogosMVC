using CatalogosMVC.Business.Models;

namespace CatalogosMVC.Business.Services.Interfaces;

public interface IListService
{
    public Task<List<ListModel>> ListAllOwnedByUser(int idUser);

    public Task<ListModel> GetById(int id);

    public Task<bool> AddList(ListModel list, int userId);

    public Task<bool> Update(ListModel list);

    public Task<bool> Delete(ListModel list);
}
