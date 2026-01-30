using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Data.Repositories.Interfaces;

public interface IListRepository
{
    public Task<List<ListEntity>> ListAllOwnedByUser(int userId);

    public Task Add(ListEntity list);

    public Task Commit();
}
