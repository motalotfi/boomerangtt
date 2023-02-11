using Bit.Core.Contracts;
using Bit.Core.Exceptions;
using Bit.OData.ODataControllers;
using Boomrang.App.Args;
using Boomrang.App.Args.AccountControllerArgs;
using Boomrang.Service.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Boomrang.App.Controllers
{
    public class AccountController : DtoController
    {
        public IComputeHash ComputeHash { get; set; }
        public IBoomrangDbService BoomrangDbService { get; set; }


        [Action, Authorize]
        public async Task RegisterUserAsync(RegisUserArgs args, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var password = ComputeHash.ComputeSha256Hash(args.Password);


            try
            {
                await BoomrangDbService.RegisterUserAsync(userId, args.UserName, password, args.MobileNo,
                    args.FirstName, args.LastName, args.Gender, args.RoleType);
            }

            catch (SqlException sqlException)
            {
                var validErrors = new[] { 50001 };
                if (validErrors.Contains(sqlException.Number))
                {
                    throw new DomainLogicException(sqlException.Message);
                }

                throw;
            }

        }

    }
}
