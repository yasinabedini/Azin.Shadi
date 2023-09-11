using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.DTOs.Enums;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Context;
using Azin.Shadi.DAL.Entities.Discount;
using Azin.Shadi.DAL.Entities.Forward;
using Azin.Shadi.DAL.Entities.Order;
using Azin.Shadi.DAL.Entities.Product;
using Azin.Shadi.DAL.Entities.User;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Azin.Shadi.Core.Services;
public class OrderService : IOrderService
{
    private IUserService _Userservice;
    private readonly IProductService _ProductService;
    private readonly IPermissionService _PermissionService;
    private readonly AzinShadiContext _context;

    public OrderService(IUserService service, AzinShadiContext context, IProductService productService)
    {
        _Userservice = service;
        _context = context;
        _ProductService = productService;
    }

    public int AddOrder(int productId, int count, string username)
    {
        User user = _Userservice.GetUserByUsername(username);

        Order order = _context.Orders.FirstOrDefault(t => t.UserId == user.UserId && !t.IsFinaly);

        Product product = _ProductService.GetProductById(productId);

        //If User Has'nt Any Active Cart
        if (order == null)
        {
            order = new Order()
            {
                UserId = user.UserId,
                IsFinaly = false,
                SumPrice = product.Price * count,
                CreateDate = DateTime.Now,
                StatusId = 1,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        ProductId = productId,
                        Price = product.Price,
                        Count = count,
                        SumPrice = product.Price*count
                    }
                }
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        //If User Has Any Active Cart
        else
        {
            OrderLine orderLine = _context.OrderLines.FirstOrDefault(t => t.OrderId == order.Id && t.ProductId == productId);

            //If This Product Is Not In The Cart
            if (orderLine == null)
            {
                orderLine = new OrderLine()
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Count = count,
                    Price = product.Price,
                    SumPrice = product.Price * count
                };
                _context.OrderLines.Add(orderLine);
                _context.SaveChanges();
            }

            //If Some Of This Product Is Available In The Shopping Cart
            else
            {
                orderLine.Count += count;
                orderLine.SumPrice += product.Price * count;

                _context.OrderLines.Update(orderLine);
                _context.SaveChanges();
            }
        }
        return order.Id;
    }

    public List<Order> GetUserOrders(string username)
    {
        int userId = _Userservice.GetUserByUsername(username).UserId;

        return _context.Orders.Include(t => t.OrderStatus).Include(t => t.OrderLines).ThenInclude(t => t.Product).Where(t => t.UserId == userId).ToList();
    }

    public void UpdateOrderEndPrice(int orderId)
    {
        Order order = _context.Orders.Include(t => t.OrderLines).First(t => t.Id == orderId);
        order.SumPrice = order.OrderLines.Sum(t => t.SumPrice);
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public Order GetUserOrderById(string username, int orderId)
    {
        int userId = _Userservice.GetUserByUsername(username).UserId;
        return _context.Orders.Include(t => t.User).Include(t => t.OrderLines).ThenInclude(t => t.Product).Include(t => t.OrderStatus).FirstOrDefault(t => t.Id == orderId && t.UserId == userId);
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public int AddForward(Forward forward)
    {
        _context.Forwards.Add(forward);
        _context.SaveChanges();
        return forward.Id;
    }

    public Forward GetForwardById(int id)
    {
        return _context.Forwards.FirstOrDefault(t => t.Id == id);
    }

    public bool PayOrder(string username, int orderId, string address)
    {
        Order payedOrder = GetUserOrderById(username, orderId);
        if (PayOrder == null)
        {
            return false;
        }
        int forwardId = AddForward(new DAL.Entities.Forward.Forward
        {
            Address = address,
            OrderId = orderId
        });
        payedOrder.IsPay = true;
        payedOrder.ForwardId = forwardId;
        payedOrder.StatusId = 3;

        foreach (var orderline in payedOrder.OrderLines)
        {
            _ProductService.DecreaseInventory(orderline.ProductId, orderline.Count);
        }
        UpdateOrder(payedOrder);
        return true;
    }

    public DiscountStatus CheckDiscount(int userId, string discountCode)
    {
        Discount discount = GetDiscountByCode(discountCode);
        if (discount == null)
        {
            return DiscountStatus.NotFound;
        }
        if (discount.StartDate != null && discount.StartDate > DateTime.Now)
        {
            return DiscountStatus.Expired;
        }
        if (discount.EndDate != null && discount.EndDate < DateTime.Now)
        {
            return DiscountStatus.Expired;
        }
        if (discount.UsableCount < 1)
        {
            return DiscountStatus.Finished;
        }
        if (_context.UserDiscounts.Any(t => t.UserId == userId && t.Code == discount.DiscountCode))
        {
            return DiscountStatus.Usable;
        }
        return DiscountStatus.Success;
    }



    public void AddDiscount(Discount discount)
    {
        _context.Discounts.Add(discount);
        _context.SaveChanges();
    }

    public Discount GetDiscountById(int id)
    {
        return _context.Discounts.Find(id);
    }


    public Order EffectDiscount(int userId, int orderId, string code)
    {
        Discount discount = GetDiscountByCode(code);
        Order order = _context.Orders.FirstOrDefault(t => t.Id == orderId);
        order.SumPrice -= (order.SumPrice * discount.DiscountPercent) / 100;
        UpdateOrder(order);

        _context.UserDiscounts.Add(new UserDiscounts
        {
            UserId = userId,
            Code = code
        });
        _context.SaveChanges();

        if (discount.UsableCount != null && discount.UsableCount >= 1)
        {
            discount.UsableCount--;
            UpdateDiscount(discount);
        }
        
        return order;
    }

    public Discount GetDiscountByCode(string Code)
    {
        return _context.Discounts.FirstOrDefault(t => t.DiscountCode == Code);
    }

    public void UpdateDiscount(Discount discount)
    {
        _context.Discounts.Update(discount);
        _context.SaveChanges();
    }

    public Order GetCurrentOrder(string username)
    {
        return _context.Orders.Include(t => t.User).Where(t => t.User.Username == username && !t.IsPay).FirstOrDefault();
    }

    public List<Discount> GetAllDiscount()
    {
        return _context.Discounts.ToList();
    }

    public List<Order> GetAllOrder()
    {
        return _context.Orders.Include(t=>t.OrderStatus).Include(t=>t.OrderLines).Include(t=>t.Forward).Include(t=>t.User).ToList();
    }

    public Order GetOrderById(int orderId)
    {
        return _context.Orders.Include(t=>t.Forward).Include(t=>t.User).Include(t=>t.OrderStatus).Include(t=>t.OrderLines).ThenInclude(t=>t.Product).FirstOrDefault(t=>t.Id==orderId);
    }

    public void SendOrder(int id,string trackingCode)
    {
        Order order = GetOrderById(id);
        order.StatusId = 4;
        order.Forward.IsForward = true;
        order.Forward.TrackingCode = trackingCode;
        _context.Orders.Update(order);
        _context.SaveChanges();
    }
}
