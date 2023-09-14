using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.ProductGroup;

[PermissionChecker(21)]
public class EditModel : PageModel
{
    private readonly IProductService _service;

    public EditModel(IProductService service)
    {
        _service = service;
    }

    [BindProperty]
    public DAL.Entities.Product.ProductGroup ProductGroup { get; set; }

    public void OnGet(int id)
    {
        ProductGroup = _service.GetProductGroupById(id);
    }

    public IActionResult OnPost(IFormFile? productGroupPictureUp)
    {
        _service.UpdateProductGroup(ProductGroup, productGroupPictureUp);
        return RedirectToPage("Index");
    }
}
