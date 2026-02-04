using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // check if user who owns the Id exists

        ViewBag.UserId = userId;

        var ownedIds = await _listService.ListAllOwnedByUser(userId);
        
        return View(ownedIds);
    }   

    [HttpGet]

    public IActionResult CreateList(int userId)
    {
        //check if there is an user whose id corresponds to userId

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateList(ListModel list, int userId, IFormFile image)
    {
        var AbleToAdd = await _listService.AddList(list, userId, image);

        if (AbleToAdd)
        {
            return RedirectToAction("CatalogueIndex", new { userId });
        }

        return View();
    }

    [HttpGet]

    public async Task<IActionResult> UpdateList(int id)
    {
        var list = await _listService.GetById(id);

        if (id > 0 && list != null)
        {
            return View(list);
        }

        return NotFound();
    }

    [HttpPost]

    public async Task<IActionResult> UpdateList(ListModel list, IFormFile picture)
    {
        var ableToUpdate = await _listService.Update(list, picture);

        if (ableToUpdate)
        {        
            return RedirectToAction("CatalogueIndex", new { list.UserId });
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteList(int id)
    {
        var model = await _listService.GetById(id);

        if (id > 0 && model != null)
        {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]

    public async Task<IActionResult> DeleteList(ListModel list)
    {
        var ableToDelete = await _listService.Delete(list);
        if (ableToDelete)
        {
            return RedirectToAction("CatalogueIndex", new { list.UserId });
        }
        return View();
    }

}