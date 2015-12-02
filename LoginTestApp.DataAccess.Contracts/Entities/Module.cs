
using System.Collections.Generic;

namespace LoginTestApp.DataAccess.Contracts.Entities
{
    public class Module : EntityBase<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        #region Navigation Properties

        public List<ModuleAction> ModuleActions { get; set; }

        #endregion Navigation Properties
    }
}
