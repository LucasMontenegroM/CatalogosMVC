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
    public async Task<IActionResult> CreateUser()
    {
        return View("CreateUser");
    }

    [HttpPost]

    public async Task<IActionResult> CreateUser(UserModel user)
    {
        await _userService.Add(user);
        return RedirectToAction("Index");
    }
}
