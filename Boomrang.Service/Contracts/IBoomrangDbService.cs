using Boomrang.Model.Dto;
using Boomrang.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boomrang.Service.Contracts
{
    public interface IBoomrangDbService
    {
        //AccountController======================================================================
        Task RegisterUserAsync(Guid userId, string userName, string password, string mobileNo, string firstName, string lastName, Gender gender, RoleType roleType);
        Task<LoginUserDto> LoginUserAsync(string userName, string password);

        //AdminController=========================================================================
        Task<List<UserInfoDto>> GetUsersInfo(string mobileNo, RoleType roleType);
        Task UploadExcelAdmin(string excelFileName, List<CompanyInformationExcelDto> companyList);


        //MasterDataController====================================================================
        IQueryable<InformationDto> GetProvinces();
        IQueryable<InformationDto> GetCities(int ProvinceId);
        IQueryable<InformationDto> GetProficiencyField();
        IQueryable<InformationDto> GetActivityField();


        //OperatorController======================================================================
        Task SaveCompanyInformation(Guid userId, Guid? companyId, CompanyInformationDto companyInformation);
        Task<List<GetCompanyInformationDto>> GetCompanyInformation(string mobileNo);
        Task DeleteCompanyInformation(Guid userId, Guid companyId);
    }

}
