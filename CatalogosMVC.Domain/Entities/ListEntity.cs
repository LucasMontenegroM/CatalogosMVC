namespace CatalogosMVC.Domain.Entities;

public class ListEntity
{
    public int Id { get; private set; }

    public int UserId {  get; protected set; }

    public string Name { get; protected set; }

    public string Image {  get; protected set; }

    public DateTime CreationTime { get; protected set; }
}
