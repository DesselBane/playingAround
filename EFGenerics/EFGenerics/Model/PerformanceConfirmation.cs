using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFGenerics.Model.Enums;

namespace EFGenerics.Model
{
    public abstract class PerformanceConfirmation : EntityBase
    {
        public virtual PerformanceConfirmationStatus Status { get; set; }
        [NotMapped]
        public abstract IEnumerable<UnitOfWork> UnitOfWork { get; }
        [NotMapped]
        public abstract OrderItem OrderItem { get; }
    }
}
