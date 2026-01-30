using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Business.Services;

public class ListService : IListService
{
    private readonly IListRepository _listRepository;

    public ListService(IListRepository listRepository)
    {
        _listRepository = listRepository;
    }

    public async Task<List<ListModel>> ListAllOwnedByUser(int idUser)
    {
        var entity = await _listRepository.ListAllOwnedByUser(idUser);

        return entity.Select(item =>{
            return ListModel.Map(item);
        }).ToList();
    }

    public async Task<bool> AddList(ListModel list, int userId)
    {
        if (list != null) {
            var entity = new ListEntity
                (
                    userId,
                    list.Name,
                    list.Image
                );

            await _listRepository.Add(entity);
            await _listRepository.Commit();
            return true;
        }
        return false;
    }
}
