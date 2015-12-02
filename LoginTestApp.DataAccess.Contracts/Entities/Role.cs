
using System.Collections.Generic;

namespace LoginTestApp.DataAccess.Contracts.Entities
{
    public class Role : EntityBase<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        #region Navigation Properties

        public List<User> Users { get; set; }

        #endregion Navigation Properties
    }
}
