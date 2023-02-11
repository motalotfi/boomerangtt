using Bit.Core.Contracts;
using Bit.Core.Exceptions;
using Bit.OData.ODataControllers;
using Boomrang.App.Args;
using Boomrang.App.Args.OperatorControllerArgs;
using Boomrang.Model.Dto;
using Boomrang.Service.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace Boomrang.App.Controllers
{
    public class OperatorController : DtoController
    {
        public IBoomrangDbService BoomrangDbService { get; set; }
        public IUserInformationProvider UserInformationProvider { get; set; }


        [Action, Authorize]
        public async Task SaveCompanyInformation(CompanyInformationArgs args, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(UserInformationProvider.GetCurrentUserId());
                var companyId = args.CompanyInformation.CompanyId != null ? args.CompanyInformation.CompanyId : Guid.NewGuid();

                await BoomrangDbService.SaveCompanyInformation(userId, companyId, args.CompanyInformation);
            }
            catch (SqlException sqlException)
            {
                throw new DomainLogicException(sqlException.Message);
            }

        }

        [Action, Authorize]
        public async Task<List<CompanyInformationDto>> GetCompanyInformation(InformationArgs args, CancellationToken cancellationToken)
        {
            try
            {
                var result = await BoomrangDbService.GetCompanyInformation(args.MobileNo);

                var companyInfo = new List<CompanyInformationDto>();
                var companiesId = result.Select(c => c.CompanyId).Distinct().ToList();

                foreach (var companyId in companiesId)
                {
                    var companies = result.Where(i => i.CompanyId == companyId).ToList();
                    var companyProduct = new List<CompanyProductDto>();

                    foreach (var company in companies)
                    {
                        companyProduct.Add(new CompanyProductDto()
                        {
                            CompanyProductId = company.CompanyProductId,
                            CompanyImportantProduct = company.CompanyImportantProduct,
                            ProductKnowledgeField = company.ProductKnowledgeField,
                            ProductApplication = company.ProductApplication,
                            ProductDescription = company.ProductDescription
                        });
                    }

                    var information = result.Where(i => i.CompanyId == companyId).FirstOrDefault();

                    companyInfo.Add(new CompanyInformationDto()
                    {
                        CompanyId = information.CompanyId,
                        FirstName = information.FirstName,
                        LastName = information.LastName,
                        Gender = information.Gender,
                        Education = information.Education,
                        Major = information.Major,
                        CityId = information.CityId,
                        MobileNumber = information.MobileNumber,
                        EmailAddress = information.EmailAddress,
                        ProficiencyField = information.ProficiencyField,
                        ActivityField = information.ActivityField,
                        CompanyName = information.CompanyName,
                        UserPosition = information.UserPosition,
                        IsKnowledgeEnterprise = information.IsKnowledgeEnterprise,
                        PhoneNumber = information.PhoneNumber,
                        WebsiteAddress = information.WebsiteAddress,
                        EstablishmentYear = information.EstablishmentYear,
                        ManpowerCount = information.ManpowerCount,
                        DataImporter = information.DataImporter,
                        DataEntryDateTime = information.DataEntryDateTime,
                        UpdatedDataImporter = information.UpdatedDataImporter,
                        DataUpdateDateTime = information.DataUpdateDateTime,
                        CompanyProducts = companyProduct,
                    });
                }
                return companyInfo;
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
        public async Task DeleteCompanyInformation(DeleteCompanyInformationArgs args, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(UserInformationProvider.GetCurrentUserId());
                await BoomrangDbService.DeleteCompanyInformation(userId, args.CompanyId);
            }
            catch (SqlException sqlException)
            {
                throw new DomainLogicException(sqlException.Message);
            }

        }

    }
}
