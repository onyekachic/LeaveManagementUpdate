using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Identity.Configurations
{
    public  class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "cac43a6e-f7bb-4448-baaf-ladd431ccbbf",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                 new IdentityRole
                 {
                     Id = "cac43a6e-f7bb-5558-bEEf-ladd431aajjk",
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR"
                 }
                );
        }

      
    }
}
