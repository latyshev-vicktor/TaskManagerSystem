using AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationService.DataAccess.Postgres.Configurations
{
    public class EmailVerificationTokenConfiguration : BaseConfiguration<EmailVerificationTokenEntity>
    {
        protected override void Config(EntityTypeBuilder<EmailVerificationTokenEntity> builder)
        {
            builder.ToTable("EmailVerificationTokens");

            builder.Property(x => x.CreateOnUtc).IsRequired();
            builder.Property(x => x.ExpiredOnUtc).IsRequired();
            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId);
        }
    }
}
