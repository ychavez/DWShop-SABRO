using DWShop.Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWShop.Domain.Entities
{
    [Table(nameof(Basket),Schema = "movs")]
    public class Basket: AuditableEntity<int>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
