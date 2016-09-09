using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace SecureTockenService.Unity
{
    public class UnityInstanceProvider : IInstanceProvider, IContractBehavior
    {
        #region Vars

        private readonly IUnityContainer _container;

        #endregion

        #region Constructors

        public UnityInstanceProvider(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        #endregion

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return _container.Resolve(
                instanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(ContractDescription contractDescription,
                                         ServiceEndpoint endpoint,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription,
                                        ServiceEndpoint endpoint,
                                        ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription,
                                          ServiceEndpoint endpoint,
                                          DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription,
                             ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}