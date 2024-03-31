using _0_Framework.Application;
using _0_Framework.Repository;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure;

namespace InventoryManagement.Infrastructure.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }
        public Inventory GetBy(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory
                .Select(x => new EditInventory
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetInventoryOperationLog(long inventoryId)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            return inventory.Operations.Select(x => new InventoryOperationViewModel
                {
                    Id = x.Id,
                    Count = x.Count,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    Operation = x.Operation,
                    OperationDate = x.OperationDate.ToFarsi(),
                    OperatorId = x.OperatorId,
                    OrderId = x.OrderId,
                    Operator = "مدیر سیستم"
                }).OrderByDescending(x=>x.Id).ToList();

        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _inventoryContext.Inventory
                .Select(x => new InventoryViewModel
                {
                    Id = x.Id,
                    UnitPrice = x.UnitPrice,
                    ProductId = x.ProductId,
                    InStock = x.InStock,
                    CurrentCount = x.CalculateCurrentCount(),
                    CreationDate = x.CreatetionDateTime.ToFarsi()
                });

            if (searchModel.InStock)
                query = query.Where(x => x.InStock == false);

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach
            (
                item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name
            );

            return inventory;
        }
    }
}
