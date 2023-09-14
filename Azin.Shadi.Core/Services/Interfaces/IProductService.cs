using Azin.Shadi.Core.DTOs;
using Azin.Shadi.DAL.Entities.Comment;
using Azin.Shadi.DAL.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Azin.Shadi.Core.Services.Interfaces;

public interface IProductService
{
    #region Product Groups
    List<ProductGroup> GetProductGroups();
    List<ProductGroup> GetProductGroupForManageProduct();
    List<SelectListItem> GetProductStatuses();
    void AddProductGroup(ProductGroup productGroup, IFormFile productGroupPicture);
    ProductGroup GetProductGroupById(int id);
    void UpdateProductGroup(ProductGroup productGroup, IFormFile productGroupPicture);
    int GetFirstProductGroupId();
    List<ProductGroup> GetProductGroupsByParentId(int parentId);
    #endregion

    #region Product
    int AddProduct(Product product, IFormFile productImageUp);
    ShowProductForAdminViewModel GetProductsForAdmin(int pageId, string nameFilter, string groupFilter);
    Product GetProductById(int productId);
    void UpdateProduct(Product product, IFormFile productImageUp);
    List<ShowProductViewModel> GetProducts(int pageId = 1, string filter = "", string orderBy = "", string typeBy = "", int minPrice = 0, int MaxPrice = 0, List<int> selectedGroups = null, int take = 8, bool orderByCreateDate = false);
    Product GetProductForShow(int id);
    void DecreaseInventory(int productId, int count);
    int GetProductSellsNumber(int productId);
    List<ShowProductViewModel> GetPopularProducts();

    #endregion

    #region Comment

    void AddComment(Comment comment);
    public Tuple<int, List<Comment>> GetProductComments(int productId, int pageId = 1);

    #endregion

}
