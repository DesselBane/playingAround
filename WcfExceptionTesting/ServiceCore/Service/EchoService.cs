using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceCore.Contracts;

namespace ServiceCore.Service
{
    [ServiceBehavior]
    public class EchoService : IEchoService
    {
        #region Implementation of IEchoService

        public string Echo(string message)
        {
            if(string.IsNullOrWhiteSpace(message))
                throw new FaultException(nameof(message));

            return $"Echo: {message}";
        }

        #endregion
    }
}
