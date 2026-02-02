namespace CatalogosMVC.Domain.Entities;

public class UserEntity
{
    public int Id { get; private set; }
    public string Name { get; protected set; }

    public UserEntity(string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}