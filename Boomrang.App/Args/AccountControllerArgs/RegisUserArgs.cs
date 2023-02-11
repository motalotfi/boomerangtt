using Boomrang.Model.Enum;

namespace Boomrang.App.Args.AccountControllerArgs
{
    public class RegisUserArgs
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public RoleType RoleType { get; set; }
    }
}
