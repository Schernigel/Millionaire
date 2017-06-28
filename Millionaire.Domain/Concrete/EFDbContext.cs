using Millionaire.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Millionaire.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<GameQuestion> GameQuestions { get; set; }
        public DbSet<Results> Resultses { get; set; }
        public DbSet<UserStatistics> UserStatisticses { get; set; }
        public DbSet<TotalUserResult> TotalUserResults { get; set; }

        public EFDbContext() : base("Millionaire")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<GameQuestion>()
                .HasRequired(c1 => c1.Answer)
                .WithRequiredPrincipal(c2 => c2.GameQuestion);

        }
    }
}
