
using _01_Query.Contract.Product;
using Comment.Application.Contract;
using CommentManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product;
        public string value;

        public ProductDetailModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            value = id;
            Product = _productQuery.GetProductDetail(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = CommentType.ProductType;
            _commentApplication.Create(command);
            return RedirectToPage("/ProductDetail", new { Id = productSlug });
        }
    }
}
