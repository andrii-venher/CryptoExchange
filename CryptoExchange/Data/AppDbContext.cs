﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(x => new
                {
                    x.UserId,
                    x.CoinId
                });
            modelBuilder.Entity<Account>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Account>()
                .HasOne<Coin>()
                .WithMany()
                .HasForeignKey(x => x.CoinId);

            modelBuilder.Entity<Transaction>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Transaction>()
                .HasOne<Coin>()
                .WithMany()
                .HasForeignKey(x => x.CoinId);

            base.OnModelCreating(modelBuilder);
        }
    }
}