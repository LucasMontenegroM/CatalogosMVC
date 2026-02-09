using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Data.Repositories.Interfaces;

public interface IBookRepository
{
    public Task<List<BookEntity>> ListAllOwnedByUser(int userId);

    public Task Add(BookEntity bookEntity);

    public Task<BookEntity> GetById(int id);

    public void Delete(BookEntity user);

    public Task Commit();
}
