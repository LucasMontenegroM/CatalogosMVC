using CatalogosMVC.Business.Models;
using Microsoft.AspNetCore.Http;

namespace CatalogosMVC.Business.Services.Interfaces;

public interface IBookService
{
    public Task<List<BookModel>> ListAllOwnedByUser(int idUser);

    public Task<BookModel> GetById(int id);

    public Task<bool> GetCorrespondingUser(int userId);

    public Task<bool> Add(BookModel bookModel, int userId, IFormFile image);

    public Task<bool> Update(BookModel bookModel, IFormFile picture);

    public Task<bool> Delete(BookModel bookModel);
}
