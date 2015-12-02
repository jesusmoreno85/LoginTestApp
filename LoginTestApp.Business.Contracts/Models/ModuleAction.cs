
namespace LoginTestApp.Business.Contracts.Models
{
    public class ModuleAction : ModelBase<string>
    {
        public int ModuleId { get; set; }

        public string GrantedRoles { get; set; }

        public bool IsActive { get; set; }
    }
}
