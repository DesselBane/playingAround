using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model
{
    public abstract class OrderItem: EntityBase
    {
        [Key]
        [Column(Order = 2)]
        public override int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public virtual int Order_Id { get; protected set; }

        [ForeignKey(nameof(Order_Id))]
        public virtual Order Order { get; set; }

        public virtual int Amount { get; set; }



        [NotMapped]
        public abstract ProductBase Product { get; }

        [NotMapped]
        public abstract IEnumerable<UnitOfWork> UnitsOfWork { get; }
        [NotMapped]
        public abstract IEnumerable<PerformanceConfirmation> PerformanceConfirmations { get;}
    }



}
