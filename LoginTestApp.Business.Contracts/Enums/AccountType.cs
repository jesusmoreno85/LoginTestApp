
namespace LoginTestApp.Business.Contracts.Enums
{
    /// <summary>
    /// Represents the overall account access level
    /// </summary>
    public enum AccountType
    {
        SysAdmin = 1,
        Admin = 2,
        //Some other types
        User = 98,
        Anonymous = 99
    }
}
