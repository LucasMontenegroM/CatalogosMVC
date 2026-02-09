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
    public IActionResult CreateUser()
    {
        return View("CreateUser");
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

    public async Task<IActionResult> UpdateUser(int id)
    {
        var user = await _userService.GetById(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost]

   public async Task<IActionResult> UpdateUser(UserModel userModel)
    {
        if(userModel == null)
        {
            return View("UpdateUser");
        }

        await _userService.UpdateUser(userModel);

        return RedirectToAction("Index");
    }

    [HttpGet]

    public async Task<IActionResult> DeleteUser(int id)
    {
        var userModel = await _userService.GetById(id);

        if (userModel == null)
        {
            return NotFound();
        }

        return View(userModel);
    }

    [HttpPost]

    public async Task<IActionResult> DeleteUser(UserModel userModel)
    {
        var ableToDelete = await _userService.Delete(userModel);

        if (!ableToDelete)
        {
            return NotFound();
        }            
            return RedirectToAction("Index");
    }
}
