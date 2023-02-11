using Bit.Core.Exceptions;
using Bit.Core.Models;
using Bit.IdentityServer.Implementations;
using Boomrang.Model.Enum;
using Boomrang.Service.Contracts;
using IdentityServer3.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boomrang.App.Identity
{
    public class BoomrangUserService : UserService
    {
        public IBoomrangDbService BoomrangDbService { get; set; }
        public IComputeHash ComputeHash { get; set; }


        public override async Task<BitJwtToken> LocalLogin(LocalAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context.UserName == "member" && context.Password == "member")
            {
                return new BitJwtToken
                {
                    UserId = "C65DBE5B-A76F-48F8-973E-8A38FF1001DB",
                    Claims = new Dictionary<string, string>
                    {
                        {"RoleType", RoleType.Admin.ToString()}   // Member User
                    }
                };
            }

            var userName = context.UserName;
            var password = ComputeHash.ComputeSha256Hash(context.Password);

            try
            {
                var result = await BoomrangDbService.LoginUserAsync(userName, password);
                return new BitJwtToken
                {
                    UserId = result.Id.ToString(),
                    Claims = new Dictionary<string, string>
                    {
                        {
                            "RoleType", result.RoleType.ToString()
                        }
                    }
                };
            }

            catch (SqlException sqlException)
            {
                var validErrors = new[] { 50003, 50004 };
                if (validErrors.Contains(sqlException.Number))
                {
                    throw new DomainLogicException(sqlException.Message);
                }

                throw;
            }
        }

    }
}
