using System;
using System.IdentityModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Microsoft.Practices.Unity;

namespace SecureTockenService.Unity
{
    public class UnityWSTrustServiceHost : WSTrustServiceHost
    {
        #region Constructors

        public UnityWSTrustServiceHost(IUnityContainer unityContainer, SecurityTokenServiceConfiguration securityTokenServiceConfiguration, params Uri[] baseAddresses)
            : base(securityTokenServiceConfiguration, baseAddresses)
        {
            SetupContainer(unityContainer);
        }

        public UnityWSTrustServiceHost(IUnityContainer unityContainer, WSTrustServiceContract serviceContract, params Uri[] baseAddresses)
            : base(serviceContract, baseAddresses)
        {
            SetupContainer(unityContainer);
        }

        #endregion

        private void SetupContainer(IUnityContainer container)
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
    }
}