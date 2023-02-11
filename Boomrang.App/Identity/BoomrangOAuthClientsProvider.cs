using Bit.IdentityServer.Contracts;
using Bit.IdentityServer.Implementations;
using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;

namespace Boomrang.App.Identity
{
    public class BoomrangOAuthClientsProvider : OAuthClientsProvider
    {
        public override IEnumerable<Client> GetClients()
        {
            return new[]
            {
                GetResourceOwnerFlowClient(new BitResourceOwnerFlowClient
                {
                    ClientName = "BoomrangWebClient",
                    ClientId = "BoomrangWebClient",
                    Secret = "secret",
                    TokensLifetime = TimeSpan.FromDays(7),
                    Enabled = true
                })
            };
        }
    }
}
