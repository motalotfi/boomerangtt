using Bit.Core.Contracts;
using Bit.Core.Exceptions;
using Bit.OData.ODataControllers;
using Boomrang.App.Args;
using Boomrang.App.Args.AdminController;
using Boomrang.App.Args.AdminControllerArgs;
using Boomrang.Model.Dto;
using Boomrang.Service.Contracts;
using Boomrang.Service.Implementations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace Boomrang.App.Controllers
{
    public class AdminController : DtoController
    {
        public IBoomrangDbService BoomrangDbService { get; set; }
        public IUserInformationProvider UserInformationProvider { get; set; }
        public IConfiguration Configuration { get; set; }

        [Action, Authorize]
        public async Task<List<UserInfoDto>> GetUsersInfo(UserInfoArgs args, CancellationToken cancellationToken)
        {
            try
            {
                var result = await BoomrangDbService.GetUsersInfo(args.MobileNo, args.RoleType);
                return result;
            }
            catch (SqlException sqlException)
            {
                var validErrors = new[] { 50003 };
                if (validErrors.Contains(sqlException.Number))
                {
                    throw new DomainLogicException(sqlException.Message);
                }

                throw;
            }
        }

        [Action, Authorize]
        public async Task UploadExcelAdmin(UploadExcelAdminArgs args, CancellationToken cancellationToken)
        {
            try
            {
                var imageService = new ImageService();
                var excelFileName = imageService.CreateImageUrl(args.FileUrl, args.FileName, "Excel");

                var environment = Configuration.GetValue<string>("Environment");
                var excelAddress = $"{environment}/{excelFileName}";

                using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(excelAddress)))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var sheet = package.Workbook.Worksheets["Boomrang"];
                    var companyList = new ExcelFileImportService().GetList<CompanyInformationExcelDto>(sheet);

                    await BoomrangDbService.UploadExcelAdmin(excelFileName, companyList);
                }
            }
            catch (SqlException sqlException)
            {
                var validErrors = new[] { 50005 };
                if (validErrors.Contains(sqlException.Number))
                {
                    throw new DomainLogicException(sqlException.Message);
                }
                throw;
            }
        }

    }
}
