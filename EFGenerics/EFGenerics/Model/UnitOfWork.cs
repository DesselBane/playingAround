using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFGenerics.Model.Enums;

namespace EFGenerics.Model
{
    public abstract class UnitOfWork : EntityBase
    {
        public virtual string Task { get; set; }
        public virtual string Comment { get; set; }
        public virtual string TaskResult { get; set; }
        public virtual UnitOfWorkStatus Status { get; set; }

        [NotMapped]
        public abstract ProductBase Product { get; }
        [NotMapped]
        public abstract OrderItem OrderItem { get; }
        [NotMapped]
        public abstract PerformanceConfirmation PerformanceConfirmation { get; }
        [NotMapped]
        public abstract IEnumerable<BillItem> BillItems { get; }
    }
}
