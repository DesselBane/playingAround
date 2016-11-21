using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model
{
    public abstract class ProductBase : EntityBase
    {
        public virtual double PricePerUnit { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        
        [NotMapped]
        public abstract IEnumerable<OrderItem> OrderItems { get; }
    }
}
