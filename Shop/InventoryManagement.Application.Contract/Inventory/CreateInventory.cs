using ShopManagement.Application.Contract.Product;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<productViewModel> Products { get; set; }
    }
}