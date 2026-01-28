using CatalogosMVC.Domain.Entities;

namespace CatalogosMVC.Business.Models;

public class UserModel
{
    public int Id { get; set; }
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
