using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CatalogosMVC.Business.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    private readonly IUserRepository _userRepository;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public BookService(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
    {
        _bookRepository = bookRepository;

        _webHostEnvironment = webHostEnvironment;

        _userRepository = userRepository;
    }

    private async Task<string> GetImageInfo(IFormFile image)
    {
        var extension = Path.GetExtension(image.FileName);

        string imageName = $"{DateTime.Now:yyyyMMddmmssfff}{extension}";

        var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

        using (var save = new FileStream(path, FileMode.Create))
        {
            await image.CopyToAsync(save);
        }

        return imageName;
    }

    public async Task<BookModel> GetById(int id)
    {
        var entity = await _bookRepository.GetById(id);

        if (entity != null)
        {
            var model = BookModel.Map(entity);

            return model;
        }

        return null;
    }
    public async Task<List<BookModel>> ListAllOwnedByUser(int idUser)
    {
        var entity = await _bookRepository.ListAllOwnedByUser(idUser);

        if (entity != null)
        {
            return entity.Select(item => {

                return BookModel.Map(item);

            }).ToList();
        }
        else return null;
    }

    public async Task<bool> Add(BookModel bookModel, int userId, IFormFile image)
    {
        if (bookModel.Name != null && userId != 0 && image != null)
        {
            var imageName = await GetImageInfo(image);

            var bookEntity = new BookEntity
                (
                    userId,
                    bookModel.Name,
                    imageName,
                    bookModel.ReadingStatus
                );

            await _bookRepository.Add(bookEntity);

            await _bookRepository.Commit();

            return true;

        }

        return false;
    }

    public async Task<bool> Update(BookModel bookModel, IFormFile picture)
    {
        BookEntity bookEntity = await _bookRepository.GetById(bookModel.Id);

        if (bookEntity != null)
        {
            if (bookModel.Name != null)
            {
                bookEntity.UpdateName(bookModel.Name);
            }

            if (picture != null)
            {
                var extension = Path.GetExtension(picture.FileName);

                var newFileName = await GetImageInfo(picture);

                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", bookEntity.Image);

                System.IO.File.Delete(oldImagePath);
                
                bookEntity.UpdateImage(newFileName);

            }

            await _bookRepository.Commit();
            return true;
        }

        return false;
    }

    public async Task<bool> Delete(BookModel book)
    {
        if (book != null)
        {
            var entity = await _bookRepository.GetById(book.Id);

            string ImageFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", entity.Image);

            System.IO.File.Delete(ImageFullPath);

            _bookRepository.Delete(entity);

            await _bookRepository.Commit();

            return true;
        }

        return false;
    }

    public async Task<bool> GetCorrespondingUser(int userId)
    {
        var user = await _userRepository.GetById(userId);

         if(user == null)
        {
            return false;
        }

        return true;
    }
}
