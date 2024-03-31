using _0_Framework.Application;
using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contract.Product;

namespace ServiceHosts.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;
        public SelectList products;
        public List<InventoryViewModel> Inventories;
        public InventorySearchModel SearchModel;


        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel SearchModel)
        {
            products = new SelectList(_productApplication.GetProduct(), "Id", "Name");
            Inventories = _inventoryApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var product = new CreateInventory
            {
                Products = _productApplication.GetProduct()
            };

            return Partial("./Create", product);
        }

        public JsonResult OnPost(CreateInventory command)
        {
            var newProduct = _inventoryApplication.Create(command);
            return new JsonResult(newProduct);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editInevntory = _inventoryApplication.GetDetails(id);
            editInevntory.Products = _productApplication.GetProduct();

            return Partial("./Edit", editInevntory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var editInventory = _inventoryApplication.Edit(command);
            return new JsonResult(editInventory);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var increase = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("./Increase", increase);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var increase = _inventoryApplication.Increase(command);
            return new JsonResult(increase);
        }

        public IActionResult OnGetReduce(long id)
        {
            var reduce = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("./Reduce", reduce);
        }

        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var reduce = _inventoryApplication.Reduce(command);
            return new JsonResult(reduce);
        }

        public IActionResult OnGetInventoryOperation(long id)
        {
            var inventoryOperation = _inventoryApplication.GetInventoryOperationLog(id);
            return Partial("./InventoryOperation", inventoryOperation);
        }
    }
}
