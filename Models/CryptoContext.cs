using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Crypto.Models
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options)
            : base(options)
        {
        }

        public DbSet<CryptoItem> CryptoItems { get; set; } = null!;
    }
}