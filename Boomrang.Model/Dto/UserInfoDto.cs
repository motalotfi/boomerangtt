using Boomrang.Model.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boomrang.Model.Dto
{
    [ComplexType]
    public class UserInfoDto : IDbDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public Gender Gender { get; set; }
    }
}
