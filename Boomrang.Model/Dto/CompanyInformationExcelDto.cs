using Boomrang.Model.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boomrang.Model.Dto
{
    [ComplexType]
    [Keyless]
    public class CompanyInformationExcelDto : IDbDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public EducationType Education { get; set; }
        public string Major { get; set; }
        public int? CityId { get; set; }
        public string MobileNumber { get; set; }
        public string CityTitle { get; set; }
        public string EmailAddress { get; set; }
        public string ProficiencyField1 { get; set; }
        public string ProficiencyField2 { get; set; }
        public string ProficiencyField3 { get; set; }
        public string ActivityField1 { get; set; }
        public string ActivityField2 { get; set; }
        public string ActivityField3 { get; set; }
        public string CompanyName { get; set; }
        public string UserPosition { get; set; }
        public bool? IsKnowledgeEnterprise { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteAddress { get; set; }
        public string EstablishmentYear { get; set; }
        public int? ManpowerCount { get; set; }
        public string CompanyImportantProduct1 { get; set; }
        public string ProductKnowledgeField1 { get; set; }
        public string ProductApplication1 { get; set; }
        public string ProductDescription1 { get; set; }
        public string CompanyImportantProduct2 { get; set; }
        public string ProductKnowledgeField2 { get; set; }
        public string ProductApplication2 { get; set; }
        public string ProductDescription2 { get; set; }
        public string CompanyImportantProduct3 { get; set; }
        public string ProductKnowledgeField3 { get; set; }
        public string ProductApplication3 { get; set; }
        public string ProductDescription3 { get; set; }
        public string DataImporter { get; set; }
        public DateTimeOffset? DataEntryDateTime { get; set; }
        public string UpdatedDataImporter { get; set; }
        public DateTimeOffset? DataUpdateDateTime { get; set; }

    }

}