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

        List<string> allowedExtensions = [".jpeg", ".png", ".webp"];

        if (allowedExtensions.Contains(extension))
        {
            string imageName = $"{DateTime.Now:yyyyMMddmmssfff}{extension}";

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

            using (var save = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(save);
            }

            return imageName;
        }

        return null;        
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
            //change
            return entity.Select(item => {

                return BookModel.Map(item);

            }).ToList();
        }
        else return null;
    }

    public async Task<bool> Add(BookModel bookModel, int userId, IFormFile image)
    {
        if (!string.IsNullOrWhiteSpace(bookModel.Name) && userId != 0 && image != null)
        {
            var imageName = await GetImageInfo(image);

            if (imageName == null) 
            {
                return false;
            }

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
            if (!string.IsNullOrWhiteSpace(bookModel.Name))
            {
                bookEntity.UpdateName(bookModel.Name);
            }

            if (picture != null)
            {
                var newFileName = await GetImageInfo(picture);

                bookEntity.UpdateImage(newFileName);

                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", bookEntity.Image);

                System.IO.File.Delete(oldImagePath);
            }

            await _bookRepository.Commit();

            return true;
        }

        return false;
    }

    public async Task<bool> Delete(BookModel bookModel)
    {
        if (bookModel != null)
        {
            var bookEntity = await _bookRepository.GetById(bookModel.Id);

            string ImageFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", bookEntity.Image);

            System.IO.File.Delete(ImageFullPath);

            _bookRepository.Delete(bookEntity);

            await _bookRepository.Commit();

            return true;
        }

        return false;
    }

    public async Task<bool> GetCorrespondingUser(int userId)
    {
        var userEntity = await _userRepository.GetById(userId);

        if(userEntity == null)
        {
            return false;
        }

        return true;
    }
}
