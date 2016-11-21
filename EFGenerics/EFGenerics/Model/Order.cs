using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model
{
    public class Order : EntityBase
    {
        public virtual string OrderIdent { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
