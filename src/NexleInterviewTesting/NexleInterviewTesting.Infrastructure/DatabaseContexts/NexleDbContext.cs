using Microsoft.AspNetCore.Identity;

namespace NexleInterviewTesting.Infrastructure.DatabaseContexts
{
    public class NexleDbContext: IdentityDbContext<User, IdentityRole<int>, int>
    {
        public NexleDbContext(DbContextOptions<NexleDbContext> options)
            : base(options)
        {

        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        private void InternalSaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => (e.Entity is IHasCreationTime || e.Entity is IHasModificationTime) && (e.State == EntityState.Added || e.State == EntityState.Modified));
        
            foreach (var entry in entries)
            {
                if (entry.Entity is IHasCreationTime hasCreationTimeEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        hasCreationTimeEntity.CreatedAt = DateTime.Now;
                    }
                }

                if (entry.Entity is IHasModificationTime hasModificationEntity)
                {
                    if (entry.State == EntityState.Modified)
                    {
                        hasModificationEntity.UpdatedAt = DateTime.Now;
                    }
                }
            }
        }

        public override int SaveChanges()
        {
            InternalSaveChanges();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            InternalSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            InternalSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            InternalSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("users");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                    .HasColumnName("id");

                e.Property(x => x.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(30);

                e.Property(x => x.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(30);

                e.Property(x => x.Email)
                    .HasColumnName("email")
                    .HasMaxLength(250);

                e.Property(x => x.PasswordHash)
                    .HasColumnName("password")
                    .HasMaxLength(250);

                e.Property(x => x.UpdatedAt)
                    .HasColumnName("updatedAt");

                e.Property(x => x.CreatedAt)
                    .HasColumnName("createdAt");

                e.Property(x => x.NormalizedEmail)
                    .HasColumnName("normalizedEmail");

                e.Property(x => x.UserName)
                    .HasColumnName("userName");

                e.Property(x => x.NormalizedUserName)
                    .HasColumnName("normalizedUserName");

                e.Property(x => x.SecurityStamp)
                    .HasColumnName("securityStamp")
                    .HasDefaultValue(Guid.NewGuid().ToString());

                e.Ignore(x => x.EmailConfirmed);
                e.Ignore(x => x.AccessFailedCount);
                e.Ignore(x => x.ConcurrencyStamp);
                e.Ignore(x => x.LockoutEnabled);
                e.Ignore(x => x.LockoutEnd);
                
                e.Ignore(x => x.PhoneNumber);
                e.Ignore(x => x.PhoneNumberConfirmed);
                
                e.Ignore(x => x.Tokens);
                e.Ignore(x => x.TwoFactorEnabled);
            });

            modelBuilder.Entity<Token>(e =>
            {
                e.ToTable("tokens");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id)
                   .HasColumnName("id");

                e.Property(x => x.UserId)
                   .IsRequired()
                   .HasColumnName("userId");

                e.Property(x => x.RefreshToken)
                   .IsRequired()
                   .HasColumnName("refreshToken");

                e.Property(x => x.ExpireInMs)
                    .IsRequired()
                    .HasColumnName("expireInMs")
                    .HasDefaultValue(0);

                e.Property(x => x.ExpiresIn)
                  .HasColumnName("expireIn");

                e.Property(x => x.UpdatedAt)
                    .HasColumnName("updatedAt");

                e.Property(x => x.CreatedAt)
                    .HasColumnName("createdAt");
            });

            modelBuilder.Entity<IdentityRole<int>>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("role_claims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens");


            
        }
    }
}
