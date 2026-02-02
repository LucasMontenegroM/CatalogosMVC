using CatalogosMVC.Data.Context;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogosMVC.Data.Repositories;

public class ListRepository : IListRepository
{
    private readonly CatalogueContext _db;

    public ListRepository(CatalogueContext context)
    {
        _db = context;
    }

    public async Task<List<ListEntity>> ListAllOwnedByUser(int userId)
    {
        return await _db.List.AsNoTracking().Where(u => u.UserId == userId).ToListAsync();
    }

    public async Task Add(ListEntity list)
    {
        await _db.List.AddAsync(list);
    }

    public async Task<ListEntity> GetById(int id)
    {
        return await _db.List.FirstOrDefaultAsync(l => l.Id == id);
    }

    public void Delete(ListEntity user)
    {
       _db.List.Remove(user);
    }

    public async Task Commit()
    {
        await _db.SaveChangesAsync();
    }

}
