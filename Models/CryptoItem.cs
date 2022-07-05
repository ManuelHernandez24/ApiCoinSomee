using System.ComponentModel.DataAnnotations;

namespace Crypto.Models
{
    public class CryptoItem
    {
        [Key]
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public double Valor { get; set; }
        public string? ImageUrl { get; set; }
    }
}