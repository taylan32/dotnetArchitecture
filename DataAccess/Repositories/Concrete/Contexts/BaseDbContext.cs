
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Contexts
{
	public class BaseDbContext : DbContext
	{
		protected IConfiguration Configuration { get; set; }

		public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
		{
			Configuration = configuration;
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}

		public DbSet<OperationClaim> OperationClaims { get; set; }
		public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<OperationClaim>(e =>
			{
				e.Property(e => e.Id).IsRequired();
				e.Property(e => e.Name).IsRequired();
				e.HasKey(e => e.Id);

			});

			modelBuilder.Entity<User>(e =>
			{
				e.Property(e => e.Id).IsRequired();
				e.Property(e => e.Name).HasColumnName("Name");
				e.Property(e => e.LastName).HasColumnName("LastName");
				e.Property(e => e.Email).HasColumnName("Email");
				e.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
				e.Property(e => e.PasswordSalt).HasColumnName("PasswordSalt");
				e.HasKey(e => e.Id);
			});

			modelBuilder.Entity<UserOperationClaim>(e =>
			{
				e.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
				e.Property(e => e.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();
				e.HasIndex(uoc => new { uoc.UserId, uoc.OperationClaimId }).IsUnique();
				e.HasOne(uoc => uoc.User).WithMany(user => user.UserOperationClaims).HasForeignKey(uoc => uoc.UserId);
				e.HasOne(uoc => uoc.OperationClaim).WithMany().HasForeignKey(uoc => uoc.OperationClaimId);
				

			});

			modelBuilder.Entity<RefreshToken>(e =>
			{
				e.Property(e => e.Id).IsRequired();
				e.Property(e => e.UserId).IsRequired();
				e.Property(e => e.Created).IsRequired();
				e.Property(e => e.Expires).IsRequired();
				e.Property(e => e.Token).IsRequired();
				e.HasKey(e => e.Id);
				e.HasOne(e => e.User).WithOne(e => e.RefreshToken).HasForeignKey<RefreshToken>(e => e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
				
			});


			OperationClaim[] operationClaimsEntitySeeds = { new OperationClaim(1, "ADMIN"), new(2, "MANAGER") };
			modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);

		}




	}
}
