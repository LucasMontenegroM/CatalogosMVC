using CatalogosMVC.Data.Context;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CatalogosMVC.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext _db;

    public UserRepository(UserContext context)
    {
        _db = context;
    }

    public async Task<List<UserEntity>> ListAll()
    {
       return await _db.Users.AsNoTracking().ToListAsync();

    }

    public async Task Add(UserEntity user)
    {
        await _db.Users.AddAsync(user);
    }

    public async Task Commit()
    {
       await _db.SaveChangesAsync();
    }

    public async Task<UserEntity> GetById(int id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public void Delete(UserEntity user)
    {
        _db.Users.Remove(user);
    }
}
