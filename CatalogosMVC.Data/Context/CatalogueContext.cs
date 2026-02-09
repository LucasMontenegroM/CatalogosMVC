using CatalogosMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogosMVC.Data.Context;

public class CatalogueContext : DbContext
{
    public CatalogueContext(DbContextOptions options) : base(options) { }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BookEntity> Books { get; set; }
}
