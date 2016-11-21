using System.Collections.Generic;

namespace EFGenerics.Model.LicenceNS
{
    public class Licence : ProductBase
    {
        private List<LicenceOrderItem> _licenceOrderItems;
        public virtual double BuyPrice { get; set; }
        public virtual string LicenceRef { get; set; }

        #region Overrides of ProductBase

        public override IEnumerable<OrderItem> OrderItems => LicenceOrderItems;

        #endregion

        public virtual List<LicenceOrderItem> LicenceOrderItems
        {
            get { return _licenceOrderItems; }
            set { _licenceOrderItems = value; }
        }
    }
}
