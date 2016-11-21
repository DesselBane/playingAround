using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model
{
    public abstract class BillItem : EntityBase
    {
        [ForeignKey(nameof(Bill_Id))]
        public virtual Bill Bill { get; set; }
        public virtual int Bill_Id { get; protected set; }

        public virtual int Amount { get; set; }

        [NotMapped]
        public abstract IEnumerable<UnitOfWork> UnitsOfWork { get; }
    }
}
