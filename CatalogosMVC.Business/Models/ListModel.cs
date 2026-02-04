using CatalogosMVC.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CatalogosMVC.Business.Models;

public class ListModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Required(ErrorMessage="Nome é um campo obrigatório.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "Anexar uma imagem é obrigatório.")]
    public string Image { get; set; }

    public DateTime CreationTime { get; set; }

    public static ListModel Map(ListEntity entity)
    {
        if (entity == null)
        {
            return null;
        }
        else return new ListModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Name = entity.Name,
            Image = entity.Image,
            CreationTime = entity.CreationTime,
        };
    }
}
