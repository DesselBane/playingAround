using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Client
{
    public class TokenClientOptions : ITokenClientOptions
    {
        #region Implementation of ITokenClientOptions

        public string AudienceUri { get; set; }
        public string SigningCertificateName { get; set; }

        #endregion
    }
}
