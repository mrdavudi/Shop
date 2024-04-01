using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, 10000000000, ErrorMessage = ValidationMessage.IsRequired)]
        public double UnitPrice { get; set; }
        public List<productViewModel> Products { get; set; }
    }
}