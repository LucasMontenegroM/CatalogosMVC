using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Repositories.Interfaces;
using CatalogosMVC.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task<bool> AddList(ListModel list, int userId , IFormFile image)
    {
        var imageName = DateTime.Now.ToString("ddMMyyyyHHMMssfff" + image.Name + Path.GetExtension(image.FileName));

        var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);

        using (var save = new FileStream(path, FileMode.Create))
        {
            await image.CopyToAsync(save);
        };

        if (list != null) {
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
        if (list != null)
        {
            ListEntity entity = await _listRepository.GetById(list.Id);

            if (list.Image != null)
            {
                var newFileName = DateTime.Now.ToString("yyyyMMddmmssfff" + picture.Name + Path.GetExtension(picture.FileName));

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", newFileName);

                using (var save = new FileStream(path, FileMode.Create))
                {
                    await picture.CopyToAsync(save);
                };

                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", entity.Image);
                System.IO.File.Delete(oldImagePath);

                entity.UpdateImage(newFileName);
            }

            if(list.Name != null)
            {
                entity.UpdateName(list.Name);
            }

            await _listRepository.Commit();
            return true;
        }

        return false;        
    }

    public async Task<bool> Delete(ListModel list)
    {
        if (list != null) {
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
