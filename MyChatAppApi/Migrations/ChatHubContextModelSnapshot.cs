﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyChatAppApi.Context;

#nullable disable

namespace MyChatAppApi.Migrations
{
    [DbContext(typeof(ChatHubContext))]
    partial class ChatHubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MyChatAppApi.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FamliyName")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("MyChatAppApi.Models.GroupSubscribtion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("JoinedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Subscribtions", (string)null);
                });

            modelBuilder.Entity("MyChatAppApi.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool?>("IsSeen")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("char(36)");

                    b.Property<string>("SenderName")
                        .HasColumnType("longtext");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("MyChatAppApi.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GroupOwnerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LastMessage")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("MyChatAppApi.Models.Message", b =>
                {
                    b.HasOne("MyChatAppApi.Models.Room", null)
                        .WithMany("Messages")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("MyChatAppApi.Models.Room", b =>
                {
                    b.HasOne("MyChatAppApi.Models.Client", null)
                        .WithMany("Room")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("MyChatAppApi.Models.Client", b =>
                {
                    b.Navigation("Room");
                });

            modelBuilder.Entity("MyChatAppApi.Models.Room", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
