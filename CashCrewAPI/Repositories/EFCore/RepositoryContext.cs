﻿using System;
using Entities.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Repositories.EFCore.Config;

namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) :
        base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationUserAssociation> VacationUserAssociation { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentParticipant> PaymentParticipant { get; set; }
        public DbSet<Debt> Debt { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacation>()
           .HasMany(v => v.VacationUserAssociations)
           .WithOne()
           .HasForeignKey(va => va.VacationID);

            modelBuilder.Entity<VacationUserAssociation>()
                .HasOne(va => va.User)
                .WithMany()
                .HasForeignKey(va => va.UserID);

        }
    }


}

