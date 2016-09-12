using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServiceCore.Contracts
{
    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string message);
    }
}
