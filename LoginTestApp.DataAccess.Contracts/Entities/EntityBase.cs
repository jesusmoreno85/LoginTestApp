using System;

namespace LoginTestApp.DataAccess.Contracts.Entities
{
    public class EntityBase<T> : EntityBase, IEntity<T>
    {
        public T Id { get; set; }
    }

    public class EntityBase : IEntity
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
