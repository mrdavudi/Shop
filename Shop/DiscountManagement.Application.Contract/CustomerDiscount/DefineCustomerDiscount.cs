using ShopManagement.Application.Contract.Product;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set;}
        public string Reason { get; set; }
        public long ProductId { get; set; }
        public List<productViewModel> Products { get; set; }

    }
}