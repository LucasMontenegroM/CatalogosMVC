using CatalogosMVC.Business.Models;
using CatalogosMVC.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogosMVC.Web.Controllers;

public class BooksController : Controller
{

    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> Index(int userId)
    {

        var userExists = await _bookService.GetCorrespondingUser(userId);

        if (userExists)
        {
            ViewBag.UserId = userId;

            var ownedIds = await _bookService.ListAllOwnedByUser(userId);
        
            return View(ownedIds);            
        }
        return NotFound();
    }   

    [HttpGet]

    public async Task<IActionResult> Create(int userId)
    {
        var userExists = await _bookService.GetCorrespondingUser(userId);

        if (userExists) 
        {
            return View();
        }

        return NotFound();

    }

    [HttpPost]

    public async Task<IActionResult> Create(BookModel bookModel, int userId, IFormFile image)
    {
        var AbleToAdd = await _bookService.Add(bookModel, userId, image);

        if (AbleToAdd)
        {
            return RedirectToAction("Index", new { userId } );
        }

        return View();
    }

    [HttpGet]

    public async Task<IActionResult> Update(int id)
    {
        var bookModel = await _bookService.GetById(id);

        if (id > 0 || bookModel != null)
        {
            return View(bookModel);
        }

        return NotFound();
    }

    [HttpPost]

    public async Task<IActionResult> Update(BookModel bookModel, IFormFile picture)
    {
        var ableToUpdate = await _bookService.Update(bookModel, picture);

        if (ableToUpdate)
        {        
            return RedirectToAction("Index", new { bookModel.UserId });
        }

        return View();
    }

    [HttpGet]

    public async Task<IActionResult> Delete(int id)
    {
        var model = await _bookService.GetById(id);

        if (id > 0 && model != null)
        {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]

    public async Task<IActionResult> Delete(BookModel bookModel)
    {
        var ableToDelete = await _bookService.Delete(bookModel);
        if (ableToDelete)
        {
            return RedirectToAction("Index", new { bookModel.UserId });
        }
        return View();
    }

}