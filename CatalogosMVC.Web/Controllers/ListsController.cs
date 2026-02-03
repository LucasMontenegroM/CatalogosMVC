using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CatalogosMVC.Web.Controllers;

public class ListsController : Controller
{

    private readonly IListService _listService;

    public ListsController(IListService listService)
    {
        _listService = listService;
    }

    public async Task<IActionResult> CatalogueIndex(int userId)
    {
        ViewBag.UserId = userId;

        var ownedIds = await _listService.ListAllOwnedByUser(userId);
        
        return View(ownedIds);
    }   

    [HttpGet]

    public IActionResult CreateList(int userId)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateList(ListModel list, int userId, IFormFile image)
    {

        await _listService.AddList(list, userId, image);

        return RedirectToAction("CatalogueIndex", new { userId });
    }

    [HttpGet]

    public async Task<IActionResult> UpdateList(int id)
    {

        if (id > 0)
        {
            var list = await _listService.GetById(id);
            return View(list);
        }
        return null;
    }

    [HttpPost]

    public async Task<IActionResult> UpdateList(ListModel list, IFormFile picture)
    {
        await _listService.Update(list, picture);
        return RedirectToAction("CatalogueIndex", new { list.UserId });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteList(int id)
    {
        if (id > 0)
        {
            var model = await _listService.GetById(id);
            return View(model);
        }
        return null;
    }

    [HttpPost]

    public async Task<IActionResult> DeleteList(ListModel list)
    {
        await _listService.Delete(list);
        return RedirectToAction("CatalogueIndex", new { list.UserId });
    }

}