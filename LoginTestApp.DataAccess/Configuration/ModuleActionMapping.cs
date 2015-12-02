using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Configuration
{
    public class ModuleActionMapping : EntityTypeConfigurationBaseString<ModuleAction>
    {
        public ModuleActionMapping()
            : base("ModuleActions")
        {
            Property(t => t.ModuleId)
                .IsRequired()
                .HasColumnName("ModuleId");

            Property(t => t.GrantedRoles)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("GrantedRoles");

            Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive");

            //Navigation Properties
            HasRequired(t => t.Module)
                .WithMany(t => t.ModuleActions)
                .HasForeignKey(t => t.ModuleId);
        }
    }
}
