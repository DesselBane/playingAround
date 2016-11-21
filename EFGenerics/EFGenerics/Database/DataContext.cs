using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFGenerics.Model;

namespace EFGenerics.Database
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; protected set; }
    }
}
