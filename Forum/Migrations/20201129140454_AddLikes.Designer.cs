﻿// <auto-generated />
using System;
using Forum.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Forum.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20201129140454_AddLikes")]
    partial class AddLikes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Forum.Models.Label", b =>
                {
                    b.Property<int>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("LabelId");

                    b.HasIndex("TopicId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("Forum.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AuthorUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("AuthorUserId");

                    b.HasIndex("TopicId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Forum.Models.Profile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("Forum.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AuthorUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TopicId");

                    b.HasIndex("AuthorUserId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("Forum.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MessageUser", b =>
                {
                    b.Property<int>("LikesMessageId")
                        .HasColumnType("int");

                    b.Property<int>("LikesUserId")
                        .HasColumnType("int");

                    b.HasKey("LikesMessageId", "LikesUserId");

                    b.HasIndex("LikesUserId");

                    b.ToTable("MessageLikes");
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.Property<int>("FeaturedTopicId")
                        .HasColumnType("int");

                    b.Property<int>("FeaturedUsersUserId")
                        .HasColumnType("int");

                    b.HasKey("FeaturedTopicId", "FeaturedUsersUserId");

                    b.HasIndex("FeaturedUsersUserId");

                    b.ToTable("UserFeaturedTopics");
                });

            modelBuilder.Entity("Forum.Models.Label", b =>
                {
                    b.HasOne("Forum.Models.Topic", null)
                        .WithMany("Labels")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("Forum.Models.Message", b =>
                {
                    b.HasOne("Forum.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserId");

                    b.HasOne("Forum.Models.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.Navigation("Author");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Forum.Models.Profile", b =>
                {
                    b.HasOne("Forum.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Forum.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Forum.Models.Topic", b =>
                {
                    b.HasOne("Forum.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("MessageUser", b =>
                {
                    b.HasOne("Forum.Models.Message", null)
                        .WithMany()
                        .HasForeignKey("LikesMessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Forum.Models.User", null)
                        .WithMany()
                        .HasForeignKey("LikesUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.HasOne("Forum.Models.Topic", null)
                        .WithMany()
                        .HasForeignKey("FeaturedTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Forum.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FeaturedUsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Forum.Models.Topic", b =>
                {
                    b.Navigation("Labels");
                });

            modelBuilder.Entity("Forum.Models.User", b =>
                {
                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
