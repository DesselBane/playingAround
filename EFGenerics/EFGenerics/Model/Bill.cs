using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model
{
    public class Bill : EntityBase
    {
        [ForeignKey(nameof(Order_Id))]
        public virtual Order Order { get; set; }
        public virtual int Order_Id { get; protected set; }

        public virtual List<BillItem> BillItems { get; protected set; }
    }
}
