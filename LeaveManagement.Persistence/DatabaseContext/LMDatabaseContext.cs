using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Domain;
using LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.DatabaseContext
{
    public class LMDatabaseContext : DbContext
    {
        private readonly IUserService _userService;

        public LMDatabaseContext(DbContextOptions<LMDatabaseContext> options,IUserService userService) : base(options)
        {
            _userService = userService;
        }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LMDatabaseContext).Assembly);
         
            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                entry.Entity.ModifiedBy = _userService.UserId;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _userService.UserId;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
