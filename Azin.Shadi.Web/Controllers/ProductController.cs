using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Comment;
using Azin.Shadi.DAL.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azin.Shadi.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IOrderService _orderService;
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;

        public ProductController(IProductService service, IPermissionService permissionService, IOrderService orderService, IUserService userService)
        {
            _service = service;
            _permissionService = permissionService;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string filter = "", string orderBy = "Available  ", string typeBy = "", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null)
        {
            int take = 20;
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _service.GetProductGroups();
            ViewBag.pageId = pageId;
            ViewBag.take = take;
            int pageCount = _service.GetProducts().Count() / take;
            if ((pageCount % 2) != 0)
            {
                pageCount++;
            }
            ViewBag.pageCount = pageCount;
            return View(_service.GetProducts(pageId, filter, orderBy, typeBy, minPrice, maxPrice, selectedGroups, take));
        }

        [Route("ShowProduct/{id}")]
        public IActionResult ShowProduct(int id, bool addSuccess = false)
        {
            var product = _service.GetProductForShow(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        [Route("BuyProduct/{id}")]
        public IActionResult BuyProduct(int id, int count)
        {
            Product product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            int orderId = _orderService.AddOrder(id, count, username);
            _orderService.UpdateOrderEndPrice(orderId);
            ViewBag.AddSuccess = true;
            return RedirectToAction("ShowProduct", new { id = id, addSuccess = true });
        }

        [HttpPost]
        public IActionResult AddComment(int id, string text)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.ProductId = id;
            comment.CreateDate = DateTime.Now;
            comment.IsAdminRead = false;
            comment.UserId = _userService.GetUserByUsername(_permissionService.GetAuthonticatedUserUsername(Request.HttpContext)).UserId;
            _service.AddComment(comment);

            return View("ShowComments", _service.GetProductComments(comment.ProductId));
        }

        public IActionResult ShowComments(int id, int pageId = 1)
        {
            return View(_service.GetProductComments(id, pageId));
        }
    }
}
