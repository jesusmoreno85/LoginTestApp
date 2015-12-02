
namespace LoginTestApp.DataAccess.Contracts.Entities
{
    public class ModuleAction : EntityBase<string>
    {
        public int ModuleId { get; set; }

        public string GrantedRoles { get; set; }

        public bool IsActive { get; set; }

        #region Navigation Properties

        public Module Module { get; set; }

        #endregion Navigation Properties
    }
}
