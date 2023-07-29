using System.ComponentModel;

namespace Test.Api.Application.Enum
{
    public enum UserRoles:byte
    {
        [Description("Employee")] Employee =1,
        [Description("Affiliate")] Affiliate =2,
        [Description("Customer")] Customer =3
    }
}
