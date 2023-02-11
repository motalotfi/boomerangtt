using Boomrang.Model.Dto;
using Boomrang.Model.Enum;
using Boomrang.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boomrang.Service.Implementations
{
    public class BoomrangDbService : DbServiceBase, IBoomrangDbService
    {
        //AccountController=======================================================================
        public async Task RegisterUserAsync(Guid userId, string userName, string password, string mobileNo, string firstName, string lastName, Gender gender, RoleType roleType)
        {
            await ExecuteStoredProcedureAsync("dbo.MspRegisterOperator",
              new
              {
                  userId,
                  userName,
                  password,
                  mobileNo,
                  firstName,
                  lastName,
                  gender,
                  roleType
              });
        }

        public async Task<LoginUserDto> LoginUserAsync(string userName, string password)
        {
            var result = await QueryStoredProcedureAsync<LoginUserDto>("dbo.MspLoginUser",
                new
                {
                    userName,
                    password
                });

            return result.FirstOrDefault();
        }

        //AdminController=========================================================================
        public async Task<List<UserInfoDto>> GetUsersInfo(string mobileNo, RoleType roleType)
        {
            var result = await QueryStoredProcedureAsync<UserInfoDto>("dbo.MspGetUsersInfo",
                new
                {
                    mobileNo,
                    roleType
                });

            return result;
        }

        public async Task UploadExcelAdmin(string excelFileName, List<CompanyInformationExcelDto> companyList)
        {
            await ExecuteStoredProcedureAsync("dbo.MspUploadExcelAdmin",
                new
                {
                    excelFileName
                },
                paramTables: new[]
                {
                    new ParamTableInfo()
                        {
                            ParameterName = "companyList",
                            ParameterValue = companyList,
                            DbType = "dbo.TpCompanyList"
                        }
                });

        }


        //MasterDataController=====================================================================
        public IQueryable<InformationDto> GetProvinces()
        {
            var result = SqlFunction<InformationDto>($"dbo.FnGetProvinces()");
            return result;
        }

        public IQueryable<InformationDto> GetCities(int ProvinceId)
        {
            var result = SqlFunction<InformationDto>($"dbo.FnGetCities({ProvinceId})");
            return result;
        }

        public IQueryable<InformationDto> GetProficiencyField()
        {
            var result = SqlFunction<InformationDto>($"dbo.FnGetProficiencyField()");
            return result;
        }

        public IQueryable<InformationDto> GetActivityField()
        {
            var result = SqlFunction<InformationDto>($"dbo.FnGetActivityField()");
            return result;
        }

        //OperatorController========================================================================
       
        public async Task SaveCompanyInformation(Guid userId, Guid? companyId, CompanyInformationDto companyInformation)
        {
            var companyProduct = companyInformation.CompanyProducts;

            await ExecuteStoredProcedureAsync("dbo.MspSaveCompanyInformation",
                new
                {
                    userId,
                    companyId,
                    companyInformation.FirstName,
                    companyInformation.LastName,
                    companyInformation.Gender,
                    companyInformation.Education,
                    companyInformation.Major,
                    companyInformation.CityId,
                    companyInformation.MobileNumber,
                    companyInformation.EmailAddress,
                    companyInformation.ProficiencyField,
                    companyInformation.ActivityField,
                    companyInformation.CompanyName,
                    companyInformation.UserPosition,
                    companyInformation.IsKnowledgeEnterprise,
                    companyInformation.PhoneNumber,
                    companyInformation.WebsiteAddress,
                    companyInformation.EstablishmentYear,
                    companyInformation.ManpowerCount,
                    companyInformation.DataImporter,
                    companyInformation.DataEntryDateTime,
                    companyInformation.UpdatedDataImporter,
                    companyInformation.DataUpdateDateTime
                },
                paramTables: new[]
                {
                    new ParamTableInfo("companyProduct", companyProduct, "dbo.TpCompanyProduct")
                });
        }

        public async Task<List<GetCompanyInformationDto>> GetCompanyInformation(string mobileNo)
        {
            var result = await QueryStoredProcedureAsync<GetCompanyInformationDto>("dbo.MspGetCompanyInformation",
                new
                {
                    mobileNo
                });

            return result;
        }
        public async Task DeleteCompanyInformation(Guid userId, Guid companyId)
        {
            await ExecuteStoredProcedureAsync("dbo.MspDeleteCompanyInformation",
              new
              {
                  userId,
                  companyId

              });
        }
    }
}
