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
            var extension = Path.GetExtension(image.FileName);

            string imageName = $"{DateTime.Now:yyyyMMddmmssfff}{extension}";

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

            using (var save = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(save);
            };

            var entity = new BookEntity
                (
                    userId,
                    bookModel.Name,
                    imageName
                );

            await _bookRepository.Add(entity);

            await _bookRepository.Commit();

            return true;

        }

        return false;
    }

    public async Task<bool> Update(BookModel book, IFormFile picture)
    {
        BookEntity entity = await _bookRepository.GetById(book.Id);

        if (entity != null)
        {
            if (book.Name != null)
            {
                entity.UpdateName(book.Name);
            }

            if (picture != null)
            {
                var extension = Path.GetExtension(picture.FileName);

                var newFileName = $"{DateTime.Now:yyyyMMddmmssfff}{extension}";

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", newFileName);

                using (var save = new FileStream(path, FileMode.Create))
                {
                    await picture.CopyToAsync(save);
                };

                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", entity.Image);

                entity.UpdateImage(newFileName);

                System.IO.File.Delete(oldImagePath);
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
