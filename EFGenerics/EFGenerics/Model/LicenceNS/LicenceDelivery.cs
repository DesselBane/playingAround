using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model.LicenceNS
{
    public class LicenceDelivery : UnitOfWork
    {
        private Licence _licence;
        private LicenceOrderItem _licenceOrderItem;
        private LicenceDeliveryConfirmation _licenceDeliveryConfirmation;
        private List<LicenceBillItem> _licenceBillItems;

        #region Overrides of UnitOfWork

        public override ProductBase Product => Licence;
        public override OrderItem OrderItem => LicenceOrderItem;
        public override PerformanceConfirmation PerformanceConfirmation => LicenceDeliveryConfirmation;
        public override IEnumerable<BillItem> BillItems => LicenceBillItems;

        #endregion

        public virtual Licence Licence
        {
            get { return _licence; }
            set { _licence = value; }
        }

        public virtual LicenceOrderItem LicenceOrderItem
        {
            get { return _licenceOrderItem; }
            set { _licenceOrderItem = value; }
        }

        public virtual LicenceDeliveryConfirmation LicenceDeliveryConfirmation
        {
            get { return _licenceDeliveryConfirmation; }
            set { _licenceDeliveryConfirmation = value; }
        }

        public virtual List<LicenceBillItem> LicenceBillItems
        {
            get { return _licenceBillItems; }
            set { _licenceBillItems = value; }
        }
    }
}
