using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "cac43a6e-f7bb-4448-baaf-ladd431ccbbf",
                    UserId= "mng98a5e-uecc-5558-bmmf-ladd143ttrrf"

                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "cac43a6e-f7bb-5558-bEEf-ladd431aajjk",
                     UserId = "yqy56a6e-f8cc-3338-bqqf-lann143cceef"

                 }
                );
        }
    }
}
