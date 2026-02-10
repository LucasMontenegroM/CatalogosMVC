using CatalogosMVC.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CatalogosMVC.Business.Models;

public class BookModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Required(ErrorMessage="Nome é um campo obrigatório.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "Anexar uma imagem do tipo jpeg, png ou webp é obrigatório.")]
    public string Image { get; set; }

    public DateTime CreationTime { get; set; }

    public int ReadingStatus { get; set; }

    public static BookModel Map(BookEntity bookEntity)
    {
        if (bookEntity == null)
        {
            return null;
        }

        else return new BookModel
        {
            Id = bookEntity.Id,
            UserId = bookEntity.UserId,
            Name = bookEntity.Name,
            Image = bookEntity.Image,
            CreationTime = bookEntity.CreationTime,
            ReadingStatus = bookEntity.ReadingStatus
        };
    }
}
