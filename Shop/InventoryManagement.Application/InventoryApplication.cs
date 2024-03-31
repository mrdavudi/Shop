using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operationResult = new OperationResult();
            if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId))
                return operationResult.Failed("قفسه مورد نظر تکراری است");

            var newInventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(newInventory);
            _inventoryRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operationResult = new OperationResult();
            var editInventory = _inventoryRepository.Get(command.Id);

            if (editInventory == null)
                return operationResult.Failed("رکوردی یافت نشد");

            if(_inventoryRepository.Exist(x=>x.ProductId == command.ProductId && x.Id != command.Id))
                return operationResult.Failed("قفسه مورد نظر تکراری است");

            editInventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChange();

            return operationResult.Succeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetInventoryOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetInventoryOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operationResult = new OperationResult();
            var increaseInventory = _inventoryRepository.Get(command.InventoryId);

            if (increaseInventory == null)
                return operationResult.Failed("رکورد مورد نظر یافت نشد");

            var operatorId = 1;
            increaseInventory.Increase(command.Count, operatorId,command.Description);
            _inventoryRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operationResult = new OperationResult();
            var reduceInventory = _inventoryRepository.Get(command.InventoryId);

            if (reduceInventory == null)
                return operationResult.Failed("رکورد مورد نظر یافت نشد");

            var operatorId = 1;
            reduceInventory.Reduce(command.Count, operatorId, command.Description, 0);
            _inventoryRepository.SaveChange();

            return operationResult.Succeded();

        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operationResult = new OperationResult();

            foreach (var item in command)
            {
                var reduceInventory = _inventoryRepository.GetBy(item.ProductId);
                var operatorId = 1;
                reduceInventory.Reduce(item.Count, operatorId,item.Description, item.OrderId);
            }

            _inventoryRepository.SaveChange();
            return operationResult.Succeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
