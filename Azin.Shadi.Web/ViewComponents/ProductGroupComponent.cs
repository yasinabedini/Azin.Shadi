using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Azin.Shadi.Web.ViewComponents
{
    public class ProductGroupComponent : ViewComponent
    {
        private readonly IProductService _service;

        public ProductGroupComponent(IProductService service)
        {
            _service = service;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ProductGroup", _service.GetProductGroups()));
        }
    }
}
