using System.Data.Entity.ModelConfiguration;
using LoginTestApp.DataAccess.Contracts.Constants;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Contracts
{
	/// <summary>
	/// Base class that configures the common properties of an entity
	/// </summary>
	public abstract class EntityTypeConfigurationBase<T> : EntityTypeConfiguration<T>
        where T : EntityBase
    {
	    protected EntityTypeConfigurationBase(string tableName)
		{
			// Table & Column Mappings
			ToTable(tableName);

            Property(t => t.CreatedBy)
				.HasMaxLength(Users.AliasMaxLength)
				.IsRequired()
				.HasColumnName("CreatedBy");

			Property(t => t.CreatedDate)
				.IsRequired()
				.HasColumnName("CreatedDate")
				.HasColumnType("DateTime");

			Property(t => t.LastModifiedBy)
				.HasMaxLength(Users.AliasMaxLength)
				.IsOptional()
				.HasColumnName("LastModifiedBy");

			Property(t => t.LastModifiedDate)
				.IsOptional()
				.HasColumnName("LastModifiedDate")
				.HasColumnType("DateTime");
		}
	}
}
