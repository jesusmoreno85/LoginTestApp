using LoginTestApp.DataAccess.Contracts;
using LoginTestApp.DataAccess.Contracts.Entities;
using Constants = LoginTestApp.DataAccess.Contracts.Constants;

namespace LoginTestApp.DataAccess.Configuration
{
    public class UserMapping : EntityTypeConfigurationBase<User>
    {
        public UserMapping()
            : base("Users")
        {
            Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(Constants.Users.AliasMaxLength)
                .HasColumnName("Alias");

            Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("Password");

            Property(t => t.PasswordRecoveryClue)
                .HasMaxLength(50)
                .HasColumnName("PasswordRecoveryClue");

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("Email");

            Property(t => t.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("PhoneNumber");

            Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive");
        }
    }
}