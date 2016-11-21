using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFGenerics.Model.LicenceNS
{
    public class LicenceOrderItem : OrderItem
    {
        private Licence _licence;
        private int _amount;
        private List<LicenceDelivery> _licenceDeliveries;
        private List<LicenceDeliveryConfirmation> _licenceDeliveryConfirmations;

        #region Overrides of OrderItem

        public override int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public override ProductBase Product => _licence;
        public override IEnumerable<UnitOfWork> UnitsOfWork => LicenceDeliveries;
        public override IEnumerable<PerformanceConfirmation> PerformanceConfirmations => LicenceDeliveryConfirmations;

        #endregion

        public virtual Licence Licence
        {
            get { return _licence; }
            set { _licence = value; }
        }

        public virtual List<LicenceDelivery> LicenceDeliveries
        {
            get { return _licenceDeliveries; }
            set { _licenceDeliveries = value; }
        }

        public virtual List<LicenceDeliveryConfirmation> LicenceDeliveryConfirmations
        {
            get { return _licenceDeliveryConfirmations; }
            set { _licenceDeliveryConfirmations = value; }
        }
    }
}
