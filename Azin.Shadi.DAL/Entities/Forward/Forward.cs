using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.DAL.Entities.Forward
{
    public class Forward
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Address { get; set; }
        public bool IsForward { get; set; }
        public string? TrackingCode { get; set; }

        [ForeignKey("OrderId")]
        public Order.Order Order { get; set; }

    }
}
