using CatalogosMVC.Domain.Enums;

namespace CatalogosMVC.Domain.Entities;

public class BookEntity
{
    public int Id { get; private set; }

    public int UserId {  get; protected set; }

    public string Name { get; protected set; }

    public string Image {  get; protected set; }

    public DateTime CreationTime { get; protected set; }

    public ReadingStatus ReadingStatus { get; protected set; }

    public BookEntity(int userId, string name, string image)
    {
        UserId = userId;
        Name = name;
        Image = image;
        CreationTime = DateTime.Now;
    }
    public void UpdateName(string name)
    {
        Name = name;
    }
    public void UpdateImage(string image)
    {
        Image = image;
    }

}
