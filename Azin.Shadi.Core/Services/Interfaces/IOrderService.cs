using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.DTOs.Enums;
using Azin.Shadi.DAL.Entities.Discount;
using Azin.Shadi.DAL.Entities.Forward;
using Azin.Shadi.DAL.Entities.Order;
using Azin.Shadi.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.Services.Interfaces
{
    public interface IOrderService
    {
        #region Order
        int AddOrder(int productId, int count, string username);
        List<Order> GetUserOrders(string username);
        void UpdateOrderEndPrice(int orderId);
        Order GetUserOrderById(string username, int orderId);
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);
        bool PayOrder(string username,int orderId, string address);
        Order GetCurrentOrder(string username);
        List<Order> GetAllOrder();
        void SendOrder(int id, string trackingCode);
        #endregion

        #region Forward

        int AddForward(Forward forward);
        Forward GetForwardById(int id);
        #endregion

        #region Discount

        DiscountStatus CheckDiscount(int userId, string discountCode);
        void AddDiscount(Discount discount);
        Discount GetDiscountById(int id);
        Discount GetDiscountByCode(string Code);
        Order EffectDiscount(int userId, int orderId, string code);
        void UpdateDiscount(Discount discount);
        List<Discount> GetAllDiscount();
        #endregion

    }
}
