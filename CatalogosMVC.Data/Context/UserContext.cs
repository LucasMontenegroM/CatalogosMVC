using Microsoft.EntityFrameworkCore;

namespace CatalogosMVC.Data.Context;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions options) : base(options) { }
    public DbSet<CatalogosMVC.Domain.Entities.UserEntity> Users { get; set; }
}
