using Bit.Core.Contracts;
using Bit.Core.Exceptions;
using Bit.OData.ODataControllers;
using Boomrang.App.Args;
using Boomrang.App.Args.AdminController;
using Boomrang.App.Args.MasterDateControllerArgs;
using Boomrang.Model.Dto;
using Boomrang.Service.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace Boomrang.App.Controllers
{
    public class MasterDataController : DtoController
    {
        public IBoomrangDbService BoomrangDbService { get; set; }

        [Action, Authorize]
        public IQueryable<InformationDto> GetProvinces(CancellationToken cancellationToken)
        {
            var query = BoomrangDbService.GetProvinces();
            return query;
        }

        [Action, Authorize]
        public IQueryable<InformationDto> GetCities(AreaArgs args, CancellationToken cancellationToken)
        {
            var query = BoomrangDbService.GetCities(args.ProvinceId);
            return query;
        }

        [Action, Authorize]
        public IQueryable<InformationDto> GetProficiencyField(CancellationToken cancellationToken)
        {
            var query = BoomrangDbService.GetProficiencyField();
            return query;
        }

        [Action, Authorize]
        public IQueryable<InformationDto> GetActivityField(CancellationToken cancellationToken)
        {
            var query = BoomrangDbService.GetActivityField();
            return query;
        }

    }
}
