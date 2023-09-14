using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Generators;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.Core.Tools;
using Azin.Shadi.DAL.Context;
using Azin.Shadi.DAL.Entities.Comment;
using Azin.Shadi.DAL.Entities.Order;
using Azin.Shadi.DAL.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.Update;

namespace Azin.Shadi.Core.Services;

public class ProductService : IProductService
{
    private readonly AzinShadiContext _context;

    public ProductService(AzinShadiContext context)
    {
        _context = context;
    }

    public int AddProduct(Product product, IFormFile productImageUp)
    {
        product.PictureName = "Defaul-Image.jpeg";
        if (productImageUp != null && productImageUp.IsImage())
        {
            string imageName = Generator.CreateUniqueText() + Path.GetExtension(productImageUp.FileName);
            product.PictureName = imageName;

            FileTools.SaveImage(productImageUp, imageName, "Product", true);

        }

        product.CreateDate = DateTime.Now;
        product.IsActive = true;
        _context.Products.Add(product);
        _context.SaveChanges();

        return product.Id;
    }

    public List<ShowProductViewModel> GetProducts(int pageId = 1, string filter = "", string orderBy = "", string typeBy = "", int minPrice = 0, int MaxPrice = 0, List<int> selectedGroups = null, int take = 8, bool orderByCreateDate = false)
    {
        IQueryable<Product> products = _context.Products;

        if (!string.IsNullOrEmpty(filter))
        {
            products = products.Where(t => t.Name.Contains(filter));
        }


        if (!string.IsNullOrEmpty(typeBy))
        {
            switch (typeBy)
            {
                case "MostSelection":
                    products = products.Include(t => t.OrderLines).OrderByDescending(t => t.OrderLines.Sum(o => o.Count));
                    break;

                case "LastProducts":
                    products = products.OrderByDescending(t => t.CreateDate);
                    break;

                case "MostPopular":
                    products = products.OrderByDescending(t => t.CreateDate); ;
                    break;
            }
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            switch (orderBy)
            {
                case "MostPrice":
                    products = products.OrderByDescending(t => t.Price);
                    break;

                case "LessPrice":
                    products = products.OrderBy(t => t.Price);
                    break;

                case "Available":
                    products = products.Where(t => t.Inventory > 0);
                    break;
            }
        }

        if (minPrice > 0)
        {
            products = products.Where(t => t.Price > minPrice);
        }

        if (MaxPrice > 0)
        {
            products = products.Where(t => t.Price < MaxPrice);
        }



        if (selectedGroups != null && selectedGroups.Any())
        {
            foreach (var id in selectedGroups)
            {
                if (_context.ProductGroups.Find(id).ParentId == null)
                {
                    products = products.Include(t => t.ProductGroup).Where(t => t.ProductGroup.ParentId == id);
                }
                else
                {
                    products = products.Include(t => t.ProductGroup).Where(t => t.ProductGroup.Id == id);
                }


            }
        }

        if (orderByCreateDate)
        {
            products = products.OrderByDescending(t => t.CreateDate);
        }

        int skip = (pageId - 1) * take;

        return products.Select(t => new ShowProductViewModel
        {
            Id = t.Id,
            Name = t.Name,
            PictureName = t.PictureName,
            Price = t.Price,
            CreateDate = t.CreateDate
        }).Skip(skip).Take(take).ToList();
    }


    public Product GetProductById(int productId)
    {
        return _context.Products.Find(productId);
    }

    public List<ProductGroup> GetProductGroupForManageProduct()
    {
        return _context.ProductGroups.ToList();
    }

    public List<ProductGroup> GetProductGroups()
    {
        return _context.ProductGroups.Include(t => t.Products).Include(t => t.ProductGroups).ToList();
    }

    public ShowProductForAdminViewModel GetProductsForAdmin(int pageId, string nameFilter, string groupFilter)
    {
        IQueryable<Product> products = _context.Products;

        if (!string.IsNullOrEmpty(nameFilter))
        {
            products = products.Where(t => t.Name.Contains(nameFilter));
        }

        if (!string.IsNullOrEmpty(groupFilter))
        {
            List<int> groupIds = _context.ProductGroups.Where(t => t.Title.Contains(groupFilter)).Select(t => t.Id).ToList();
            foreach (var id in groupIds)
            {
                products.ToList().AddRange(_context.Products.Where(t => t.ProductGroupId == id));
            }
        }

        int take = 20;
        int skip = (pageId - 1) * take;

        return new ShowProductForAdminViewModel
        {
            Products = products.Select(t => new ProductViewModel
            {
                Id = t.Id,
                Inventory = t.Inventory,
                Name = t.Name,
                Price = t.Price,
                StatusId = t.StatusId,
                ProductGroupId = t.ProductGroupId
            }).ToList(),

            CurrentPage = pageId,

            PageCount = products.Count() / take
        };
    }

    public List<SelectListItem> GetProductStatuses()
    {
        return _context.ProductStatuses.Select(t => new SelectListItem
        {
            Value = t.Id.ToString(),
            Text = t.Title
        }).ToList();
    }

    public void UpdateProduct(Product product, IFormFile productImageUp)
    {
        if (productImageUp != null && productImageUp.IsImage())
        {
            if (product.PictureName != "default-profile.jpg")
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\Product", product.PictureName);
                FileTools.DeleteFile(deletePath);

                deletePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\Product\\Thumb", product.PictureName);
                FileTools.DeleteFile(deletePath);
            }

            string imageName = Generator.CreateUniqueText() + Path.GetExtension(productImageUp.FileName);
            product.PictureName = imageName;

            FileTools.SaveImage(productImageUp, imageName, "Product", true);
        }

        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public Product GetProductForShow(int id)
    {
        return _context.Products.Include(t => t.Comments).Include(t => t.ProductGroup).Include(t => t.ProductStatus).First(t => t.Id == id);
    }

    public void DecreaseInventory(int productId, int count)
    {
        _context.Products.Find(productId).Inventory -= count;
        _context.SaveChanges();
    }

    public void AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    public Tuple<int, List<Comment>> GetProductComments(int productId, int pageId = 1)
    {
        int take = 5;
        int skip = (pageId - 1) * take;
        var productList = _context.Comments.Where(t => t.ProductId == productId).OrderByDescending(t => t.CreateDate).Include(t => t.User).Include(t => t.Product);
        int pageCount = productList.ToList().Count / take;
        if ((pageCount % 2) != 0)
        {
            pageCount++;
        }

        return Tuple.Create(pageCount, productList.Skip(skip).Take(take).ToList());
    }

    public int GetProductSellsNumber(int productId)
    {
        var orderlines = _context.OrderLines.Include(t => t.Order).Where(t => t.ProductId == productId && t.Order.IsPay == true);
        return orderlines.Sum(t => t.Count);
    }

    public List<ShowProductViewModel> GetPopularProducts()
    {
        return _context.Products.Include(t => t.OrderLines).OrderByDescending(t => t.OrderLines.Sum(t => t.Count)).Select(t => new ShowProductViewModel
        {
            Id = t.Id,
            Name = t.Name,
            PictureName = t.PictureName,
            Price = t.Price
        }).ToList();
    }

    public void AddProductGroup(ProductGroup productGroup, IFormFile productGroupPicture)
    {
        productGroup.PictureName = "Defaul-Image.jpeg";
        if (productGroupPicture != null && productGroupPicture.IsImage())
        {
            string imageName = Generator.CreateUniqueText() + Path.GetExtension(productGroupPicture.FileName);
            productGroup.PictureName = imageName;

            FileTools.SaveImage(productGroupPicture, imageName, "ProductGroup", true);

        }

        _context.ProductGroups.Add(productGroup);
        _context.SaveChanges();
    }

    public ProductGroup GetProductGroupById(int id)
    {
        return _context.ProductGroups.First(t => t.Id == id);
    }

    public void UpdateProductGroup(ProductGroup productGroup, IFormFile productGroupPicture)
    {
        if (productGroupPicture != null && productGroupPicture.IsImage())
        {
            string imageName = Generator.CreateUniqueText() + Path.GetExtension(productGroupPicture.FileName);

            if (productGroup.PictureName != "Default.jpeg")
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\productGroup", productGroup.PictureName);
                FileTools.DeleteFile(deletePath);

                deletePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\productGroup\\Thumb", productGroup.PictureName);
                FileTools.DeleteFile(deletePath);
            }

            FileTools.SaveImage(productGroupPicture, imageName, "productGroup", true);
            productGroup.PictureName = imageName;
        }

        _context.ProductGroups.Update(productGroup);
        _context.SaveChanges();
    }

    public int GetFirstProductGroupId()
    {
        return _context.Products.First().Id;
    }

    public List<ProductGroup> GetProductGroupsByParentId(int parentId)
    {
        return _context.ProductGroups.Where(t => t.ParentId == parentId).ToList();
    }
}


