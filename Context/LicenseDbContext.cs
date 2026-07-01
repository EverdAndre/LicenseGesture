using LicenseGesture.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseGesture.Context;

public class LicenseDbContext : DbContext
{
    public LicenseDbContext(DbContextOptions<LicenseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    public DbSet<Venda> Vendas { get; set; } = null!;
}
