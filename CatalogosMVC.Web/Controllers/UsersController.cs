using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogosMVC.Web.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var usersList = await _userService.ListAll();
        return View(usersList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View("Create");
    }

    [HttpPost]

    public async Task<IActionResult> Create(UserModel userModel)
    {
        var ableToAdd = await _userService.Add(userModel);

        if (ableToAdd)
        {
            return RedirectToAction("Index");
        }

        return View();
    }

    [HttpGet]

    public async Task<IActionResult> Update(int id)
    {
        if (id > 0)
        {
            var userModel = await _userService.GetById(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        return NotFound();
    }

    [HttpPost]

   public async Task<IActionResult> Update(UserModel userModel)
    {
        if(!string.IsNullOrWhiteSpace(userModel.Name))
        {
            await _userService.Update(userModel);

            return RedirectToAction();
        }

        return View();
    }

    [HttpGet]

    public async Task<IActionResult> Delete(int id)
    {
        var userModel = await _userService.GetById(id);

        if (userModel == null)
        {
            return NotFound();
        }

        return View(userModel);
    }

    [HttpPost]

    public async Task<IActionResult> Delete(UserModel userModel)
    {
        var ableToDelete = await _userService.Delete(userModel);

        if (!ableToDelete)
        {
            return NotFound();
        }            
            
        return RedirectToAction("Index");
    }
}
