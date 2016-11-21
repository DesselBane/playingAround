using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model
{
    public abstract class EntityBase
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
