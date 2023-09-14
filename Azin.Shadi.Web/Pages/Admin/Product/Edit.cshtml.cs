using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Azin.Shadi.Web.Pages.Admin.Product;

[PermissionChecker(12)]
public class EditModel : PageModel
{
    private readonly IProductService _service;

    public EditModel(IProductService service)
    {
        _service = service;
    }

    [BindProperty]
    public DAL.Entities.Product.Product Product { get; set; }

    public void OnGet(int id)
    {
        Product = _service.GetProductById(id);
        ViewData["Groups"] = _service.GetProductGroupForManageProduct();
        ViewData["Statuses"] = new SelectList(_service.GetProductStatuses(), "Value", "Text",Product.StatusId);
    }

    public IActionResult OnPost(IFormFile? productImageUp)
    {
        if (!ModelState.IsValid) return Page();

        _service.UpdateProduct(Product,productImageUp);
        return RedirectToPage("Index");
    }
}
