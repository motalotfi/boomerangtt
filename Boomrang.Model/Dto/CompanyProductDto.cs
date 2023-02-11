using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boomrang.Model.Dto
{
    [ComplexType]
    [Keyless]
    public class CompanyProductDto : IDbDto
    {
        public Guid? CompanyProductId { get; set; }
        public string CompanyImportantProduct { get; set; }
        public string ProductKnowledgeField { get; set; }
        public string ProductApplication { get; set; }
        public string ProductDescription { get; set; }
    }
}
