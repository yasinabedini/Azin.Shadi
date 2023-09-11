using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.ProductGroup
{
    [PermissionChecker(19)]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<DAL.Entities.Product.ProductGroup> ProductGroups { get; set; }

        public void OnGet()
        {
            ProductGroups = _productService.GetProductGroups();
        }
    }
}
