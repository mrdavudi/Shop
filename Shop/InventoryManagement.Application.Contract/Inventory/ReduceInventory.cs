using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class ReduceInventory
    {
        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public long InventoryId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, 10000000, ErrorMessage = ValidationMessage.IsRequired)]
        public long OrderId { get; set; }

        [Range(1, 1000, ErrorMessage = ValidationMessage.IsRequired)]
        public long Count { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }
    }
}