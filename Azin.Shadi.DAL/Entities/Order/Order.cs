using Azin.Shadi.DAL.Entities.Forward;
using Azin.Shadi.DAL.Entities.Transaction;
using Azin.Shadi.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Order
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsFinaly { get; set; }
        public bool IsPay { get; set; }
        public int SumPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public int StatusId { get; set; }
        public int? ForwardId { get; set; }

        public User.User User { get; set; }

        [ForeignKey("ForwardId")]
        public Forward.Forward? Forward { get; set; }
        [ForeignKey("StatusId")]
        public OrderStatus OrderStatus { get; set; }
        public List<OrderLine> OrderLines { get; set; }

    }
}
