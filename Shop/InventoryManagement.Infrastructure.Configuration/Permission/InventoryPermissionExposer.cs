using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Repository.Permissions;

namespace InventoryManagement.Infrastructure.Configuration.Permission
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDTO>> Expose()
        {
            return new Dictionary<string, List<PermissionDTO>>
            {
                {
                    "Inventory", new List<PermissionDTO>
                    {
                        new PermissionDTO(InventoryPermission.ListInventory, "ListInventory"),
                        new PermissionDTO(InventoryPermission.SearchInventory, "SearchInventory"),
                        new PermissionDTO(InventoryPermission.CreateInventory, "CreateInventory"),
                        new PermissionDTO(InventoryPermission.EditInventory, "EditInventory"),
                        new PermissionDTO(InventoryPermission.IncreaseInventory, "Increase"),
                        new PermissionDTO(InventoryPermission.ReduceInventory, "Reduce"),
                        new PermissionDTO(InventoryPermission.OperationLog, "OperationLog"),
                    }
                }
            };
        }
    }
}
