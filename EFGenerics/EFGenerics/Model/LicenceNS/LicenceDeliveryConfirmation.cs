using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model.LicenceNS
{
    public class LicenceDeliveryConfirmation : PerformanceConfirmation
    {
        private List<LicenceDelivery> _licenceDelivery;
        private LicenceOrderItem _licenceOrderItem;

        #region Overrides of PerformanceConfirmation

        public override IEnumerable<UnitOfWork> UnitOfWork => LicenceDelivery;
        public override OrderItem OrderItem => LicenceOrderItem;

        #endregion

        public virtual List<LicenceDelivery> LicenceDelivery
        {
            get { return _licenceDelivery; }
            set { _licenceDelivery = value; }
        }

        public virtual LicenceOrderItem LicenceOrderItem
        {
            get { return _licenceOrderItem; }
            set { _licenceOrderItem = value; }
        }
    }
}
