using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model.LicenceNS
{
    public class LicenceBillItem : BillItem
    {
        private List<LicenceDelivery> _licenceDeliveries;

        #region Overrides of BillItem

        public override IEnumerable<UnitOfWork> UnitsOfWork => LicenceDeliveries;

        #endregion

        public virtual List<LicenceDelivery> LicenceDeliveries
        {
            get { return _licenceDeliveries; }
            set { _licenceDeliveries = value; }
        }
    }
}
