using Boomrang.Model.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boomrang.Model.Dto
{
    [ComplexType]
    [Keyless]
    public class CompanyInformationDto : IDbDto
    {
        public Guid? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public EducationType Education { get; set; }
        public string Major { get; set; }
        public int? CityId { get; set; }
        public string CityTitle { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ProficiencyField { get; set; }
        public string ActivityField { get; set; }
        public string CompanyName { get; set; }
        public string UserPosition { get; set; }
        public bool? IsKnowledgeEnterprise { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteAddress { get; set; }
        public string EstablishmentYear { get; set; }
        public int? ManpowerCount { get; set; }
        public string DataImporter { get; set; }
        public DateTimeOffset? DataEntryDateTime { get; set; }
        public string UpdatedDataImporter { get; set; }
        public DateTimeOffset? DataUpdateDateTime { get; set; }
        public List<CompanyProductDto> CompanyProducts { get; set; }
    }
}