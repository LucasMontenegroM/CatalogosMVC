using CatalogosMVC.Business.Models;

namespace CatalogosMVC.Business.Services.Interfaces;

public interface IListService
{
    public Task<List<ListModel>> ListAllOwnedByUser(int idUser);

    public Task<bool> AddList(ListModel list);
}
