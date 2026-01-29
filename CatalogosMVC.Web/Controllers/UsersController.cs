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

    public async Task<IActionResult> CreateUser(UserModel user)
    {
        await _userService.Add(user);
        return RedirectToAction("Index");
    }

    [HttpGet]

    public async Task<IActionResult> UpdateUser(int id)
    {
        var user = await _userService.GetById(id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    [HttpPost]

   public async Task<IActionResult> UpdateUser(UserModel user)
    {
        if(user == null)
        {
            return View("UpdateUser");
        }
        await _userService.UpdateUser(user);
        return RedirectToAction("Index");
    }

    [HttpGet]

    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.GetById(id);
        return View(user);
    }

    [HttpPost]

    public async Task<IActionResult> DeleteUser(UserModel user)
    {
        await _userService.Delete(user);
        return RedirectToAction("Index");
    }
}
