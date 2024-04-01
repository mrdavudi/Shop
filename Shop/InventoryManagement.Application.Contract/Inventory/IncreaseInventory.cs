using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class IncreaseInventory
    {
        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public long InventoryId { get; set; }

        [Range(1, 1000, ErrorMessage = ValidationMessage.IsRequired)]
        public long Count { get; set; }

        [Required (ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }
    }
}