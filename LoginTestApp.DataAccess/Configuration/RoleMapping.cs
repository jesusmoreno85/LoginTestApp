using System.ComponentModel.DataAnnotations.Schema;
using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Configuration
{
    public class RoleMapping : EntityTypeConfigurationBaseInt<Role>
    {
        public RoleMapping()
            : base("Roles", DatabaseGeneratedOption.Identity)
        {
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Name");

            Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Description");

            Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive");
        }
    }
}
