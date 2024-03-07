using DWShop.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWShop.Domain.Entities
{
    [Table(nameof(Basket),Schema = "movs")]
    public class Basket: AuditableEntity<int>
    {
        [MaxLength(40)]
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
