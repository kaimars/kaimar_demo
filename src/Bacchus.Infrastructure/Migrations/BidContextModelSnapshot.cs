﻿// <auto-generated />
using System;
using Bacchus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bacchus.Infrastructure.Migrations
{
    [DbContext(typeof(BidContext))]
    partial class BidContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("Bacchus.Core.Entities.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
