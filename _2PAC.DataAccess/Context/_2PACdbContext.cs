using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace _2PAC.DataAccess.Context
{
    [ExcludeFromCodeCoverage]
    public class _2PACdbContext : DbContext
    {
        public virtual DbSet<D_User> Users { get; set; }
        public virtual DbSet<D_Game> Games { get; set; }
        public virtual DbSet<D_Score> Scores { get; set; }
        public virtual DbSet<D_GameData> GameDatas { get; set; }
        public virtual DbSet<D_Review> Reviews { get; set; }
        public virtual DbSet<D_Notice> Notices { get; set; }


        public _2PACdbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<D_User>().HasData(
                new D_User()
                {
                    UserId = 1,
                    PictureId = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    Username = "admin",
                    Password = "admin",
                    Admin = true
                }
            );
            modelBuilder.Entity<D_Game>().HasData(
                new D_Game()
                {
                    GameId = 1,
                    GameName = "Hangman",
                    GameDescription = "Test your programming memory skills to fill out the words! Study for a upcoming job interview while testing your friend's game at the same time! See if you can get the high score!"
                }
            );
            modelBuilder.Entity<D_Score>().HasData(
                new D_Score()
                {
                    ScoreId = 1,
                    UserId = 1,
                    GameId = 1,
                    Score = 0
                }
            );
            modelBuilder.Entity<D_GameData>().HasData(
                new D_GameData()
                {
                    DataId = 1,
                    GameId = 1,
                    Difficulty = 1,
                    Question = "Sample Question?",
                    Answer = "Sample Answer."
                }
            );
            modelBuilder.Entity<D_Review>().HasData(
                new D_Review()
                {
                    ReviewId = 1,
                    UserId = 1,
                    GameId = 1,
                    Rating = 10,
                    ReviewBody = "I know this is the best game ever. Why? Because I helped make it!"
                }
            );
            modelBuilder.Entity<D_Notice>().HasData(
                new D_Notice()
                {
                    NoticeId = 1,
                    Description = "The app has been created!",
                    Time = new DateTime(year:2020,month:04,day:14,hour:10,minute:21,second:09)
                }
            );


// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            modelBuilder.Entity<D_User>(entity =>
            {

                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.Property(e => e.PictureId);
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60);
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(60);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<D_Game>(entity =>
            {
                entity.Property(e => e.GameId)
                    .IsRequired();
                entity.Property(e => e.GameName)
                    .IsRequired()
                    .HasMaxLength(90);
                entity.Property(e => e.GameDescription)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<D_Score>(entity =>
            {
                entity.Property(e => e.ScoreId)
                    .IsRequired();
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.GameId);
                entity.Property(e => e.Score)
                    .IsRequired();
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Scores)
                    .HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Game)
                    .WithMany(e => e.Scores)
                    .HasForeignKey(e => e.GameId);
            });

            modelBuilder.Entity<D_GameData>(entity =>
            {
                entity.Property(e => e.DataId)
                    .IsRequired();
                entity.HasIndex(e => e.GameId);
                entity.Property(e => e.Difficulty)
                    .IsRequired();
                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.HasOne(e => e.Game)
                    .WithMany(e => e.Data)
                    .HasForeignKey(e => e.GameId);
            });

            modelBuilder.Entity<D_Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .IsRequired();
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.GameId);
                entity.Property(e => e.Rating)
                    .IsRequired();
                entity.Property(e => e.ReviewBody)
                    .HasMaxLength(1000);
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Reviews)
                    .HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Game)
                    .WithMany(e => e.Reviews)
                    .HasForeignKey(e => e.GameId);
            });

            modelBuilder.Entity<D_Notice>(entity =>
            {
                entity.Property(e => e.NoticeId)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .IsRequired();
                entity.Property(e => e.Time)
                    .IsRequired();
            });
        }
    }
}

