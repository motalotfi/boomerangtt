using Boomrang.Model.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boomrang.Model.Dto
{
    [ComplexType]
    public class LoginUserDto : IDbDto
    {
        public Guid Id { get; set; }
        public RoleType RoleType { get; set; }
    }
}
