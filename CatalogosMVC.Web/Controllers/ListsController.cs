using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using CatalogosMVC.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

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
        var ownedIds = await _listService.ListAllOwnedByUser(userId);
        return View(ownedIds);
    }

    [HttpGet]

    public IActionResult CreateList(int userId)
    {
        return View("CreateList");
    }

    public async Task<IActionResult> CreateList(ListModel list)
    {
        await _listService.AddList(list);
        return RedirectToAction("CatalogueIndex");
    }
}
