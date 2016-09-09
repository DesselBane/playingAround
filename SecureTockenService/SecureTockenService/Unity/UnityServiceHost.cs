using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.Unity;

namespace SecureTockenService.Unity
{
    public class UnityServiceHost : ServiceHost
    {
        #region Constructors

        public UnityServiceHost(IUnityContainer container,
                                Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            foreach (ContractDescription cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new UnityInstanceProvider(container));
            }
        }

        #endregion
    }
}