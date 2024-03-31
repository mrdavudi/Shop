using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contract.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1, 100000, ErrorMessage = "این فیلد ضروری است")]
        public long ProductId { get; set; }
        [Range(1, 100, ErrorMessage = "درصد تخفیف باید بین 1 و 100 باشد")]
        public int DiscountRate { get; set;}
        [Required(ErrorMessage = "این فیلد ضروری است")]
        public List<productViewModel> Products { get; set; }
    }
}