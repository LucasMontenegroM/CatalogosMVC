using CatalogosMVC.Data.Context;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogosMVC.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly CatalogueContext _db;

    public BookRepository(CatalogueContext context)
    {
        _db = context;
    }

    public async Task<List<BookEntity>> ListAllOwnedByUser(int userId)
    {
        return await _db.Books.AsNoTracking().Where(u => u.UserId == userId).ToListAsync();
    }

    public async Task Add(BookEntity bookEntity)
    {
        await _db.Books.AddAsync(bookEntity);
    }

    public async Task<BookEntity> GetById(int id)
    {
        return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
    }

    public void Delete(BookEntity bookEntity)
    {
       _db.Books.Remove(bookEntity);
    }

    public async Task Commit()
    {
        await _db.SaveChangesAsync();
    }

}
