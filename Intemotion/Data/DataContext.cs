using Intemotion.Entities;
using Intemotion.Entities.Rounds.FirstRound;
using Intemotion.Entities.Rounds.SecondRound;
using Intemotion.Entities.Rounds.ThirdRound;
using Intemotion.Hubs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<SecondRound> SecondRounds { get; set; }
        public DbSet<TruthQuestion> TruthQuestions { get; set; }
        public DbSet<FirstRound> FirstRounds { get; set; }
        public DbSet<Intemotion.Entities.Rounds.FirstRound.IntellectualQuestionCost> IntellectualQuestionCosts { get; set; }
        public DbSet<Intemotion.Entities.Rounds.FirstRound.QuestionsCategory> QuestionsCategorys { get; set; }
        public DbSet<GameProcess> GameProcesses{ get; set; }
        public DbSet<ConnectedUser> ConnectedUsers{ get; set; }
        public DbSet<FirstRoundResult> FirstRoundResults{ get; set; }
        public DbSet<SecondRoundResult> SecondRoundResults{ get; set; }
        public DbSet<FileModel> Files{ get; set; }
        public DbSet<SponsorBanner> SponsorBanners{ get; set; }
        public DbSet<SponsorBannerGame> SponsorBannerGames{ get; set; }
        public DbSet<ThirdRound> ThirdRounds{ get; set; }


        public DbSet<ChatMessage> ChatMessages{ get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>().HasKey(x => x.Id);
            modelBuilder.Entity<SecondRound>().HasKey(x => x.Id);
            modelBuilder.Entity<SponsorBannerGame>().HasKey(x =>new { x.GameId,x.SponsorBannerId});
            modelBuilder.Entity<SponsorBannerGame>().HasOne(x => x.SponsorBanner).WithMany(x => x.Games).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ChatMessage>().HasOne(x => x.ConnectedUser).WithMany(x => x.ChatMessages).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ConnectedUser>().HasMany(x => x.ChatMessages).WithOne(x => x.ConnectedUser).HasForeignKey(x=>x.ConnectedUserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SponsorBannerGame>().HasOne(x => x.Game).WithMany(x => x.SponsorBanners).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FirstRound>().HasMany(x => x.QuestionsCategories).WithOne(x => x.FirstRound).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<FirstRound>().HasMany(x => x.QuestionCosts).WithOne(x => x.FirstRound).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<QuestionsCategory>().HasMany(x => x.Questions).WithOne(x => x.QuestionsCategory).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IntellectualQuestion>().HasMany(x => x.Answers).WithOne(x => x.IntellectualQuestion).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FirstRoundResulAnswer>().HasOne(x => x.Question).WithMany(x=>x.FirstRoundResulAnswers).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<ThirdRound>().HasMany(x => x.Associations).WithOne(x => x.ThirdRound).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ThirdRound>().HasMany(x => x.Chronologies).WithOne(x => x.ThirdRound).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ThirdRound>().HasMany(x => x.MelodyGuesses).WithOne(x => x.ThirdRound).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Association>().HasMany(x => x.AssociationVariants).WithOne(x => x.Association).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Chronology>().HasMany(x => x.ChronologyItems).WithOne(x => x.Chronology).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MelodyGuess>().HasMany(x => x.MelodyGuessVariants).WithOne(x => x.MelodyGuess).OnDelete(DeleteBehavior.Cascade);


        }

    }
}
