using System.Collections.Generic;

namespace LoginTestApp.Business.Contracts.Models
{
    public class Role : ModelBase<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        #region Navigation Properties

        public List<User> Users { get; set; }

        #endregion Navigation Properties
    }
}
