using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Configuration
{
    public class DynamicLinksMapping : EntityTypeConfigurationBase<DynamicLink>
    {
        public DynamicLinksMapping()
            : base("DynamicLinks")
        {
            Property(t => t.GuidId)
                .IsRequired()
                .HasColumnName("GuidId");

            Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("Type");

            Property(t => t.SubType)
                .HasMaxLength(30)
                .HasColumnName("SubType");

            Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Url");

            Property(t => t.ExpiresOn)
                .IsRequired()
                .HasColumnName("ExpiresOn");

            Property(t => t.IsConsumed)
                .IsRequired()
                .HasColumnName("IsConsumed");
        }
    }
}