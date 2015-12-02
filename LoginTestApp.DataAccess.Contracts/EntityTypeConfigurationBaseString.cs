using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Contracts
{
    /// <summary>
    /// Base class that configures the common properties of an entity
    /// </summary>
    public abstract class EntityTypeConfigurationBaseString<T> : EntityTypeConfigurationBase<T>
        where T : EntityBase<string>
    {
        protected EntityTypeConfigurationBaseString(string tableName)
            : base(tableName)
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id");
        }
    }
}
