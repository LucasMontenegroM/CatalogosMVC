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

    public async Task<ListModel> GetById(int id)
    {
        var entity = await _listRepository.GetById(id);
        if (entity != null)
        {
            var model = ListModel.Map(entity);
            return model;
        }
        return null;
    }
    public async Task<List<ListModel>> ListAllOwnedByUser(int idUser)
    {
        var entity = await _listRepository.ListAllOwnedByUser(idUser);
        if (entity != null)
        {
            return entity.Select(item => {
                return ListModel.Map(item);
            }).ToList();
        }
        else return null;
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

    public async Task<bool> Update(ListModel list)
    {
        if (list != null)
        {
            ListEntity entity = await _listRepository.GetById(list.Id);

            entity.Update(list.Name, list.Image);

            await _listRepository.Commit();

            return true;
        }
        return false;
        
    }

    public async Task<bool> Delete(ListModel list)
    {
        if (list != null) {
            var entity = await _listRepository.GetById(list.Id);
            _listRepository.Delete(entity);
            await _listRepository.Commit();
            return true;
        }
        return false;

    }
}
