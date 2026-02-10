using CatalogosMVC.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CatalogosMVC.Business.Models;

public class UserModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatório.")]

    [StringLength(50, ErrorMessage = "O nome deve ter menos de 50 caracteres.")]

    public string Name { get; set; }

    public static UserModel Map(UserEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        else return new UserModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }    
}
