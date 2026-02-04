using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CatalogosMVC.Business.Services;

public class ListService : IListService
{
    private readonly IListRepository _listRepository;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public ListService(IListRepository listRepository, IWebHostEnvironment webHostEnvironment)
    {
        _listRepository = listRepository;

        _webHostEnvironment = webHostEnvironment;
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

    public async Task<bool> AddList(ListModel list, int userId, IFormFile image)
    { 
        if (list.Name != null && userId != 0 && image != null)
        {
            var extension = Path.GetExtension(image.FileName);

            string imageName = $"{DateTime.Now:yyyyMMddmmssfff}{extension}";

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

            using (var save = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(save);
            };
           
            var entity = new ListEntity
                (
                    userId,
                    list.Name,
                    imageName
                );

            await _listRepository.Add(entity);

            await _listRepository.Commit();

            return true;
            
        }
        
        return false;
    }

    public async Task<bool> Update(ListModel list, IFormFile picture)
    {
        ListEntity entity = await _listRepository.GetById(list.Id);

        if (entity != null)
        {
            if (list.Name != null)
            {
                entity.UpdateName(list.Name);
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

            await _listRepository.Commit();
            return true;
        }

        return false;        
    }

    public async Task<bool> Delete(ListModel list)
    {
        if (list != null) 
        {
            var entity = await _listRepository.GetById(list.Id);

            string ImageFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", entity.Image);

            System.IO.File.Delete(ImageFullPath);

            _listRepository.Delete(entity);

            await _listRepository.Commit();

            return true;
        }

        return false;
    }
}
