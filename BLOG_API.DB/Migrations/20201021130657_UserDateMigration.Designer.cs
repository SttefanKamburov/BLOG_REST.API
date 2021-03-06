﻿// <auto-generated />
using System;
using BLOG_API.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BLOG_API.DB.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20201021130657_UserDateMigration")]
    partial class UserDateMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BLOG_API.DB.Models.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastDateModified")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastUserModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LastUserModifierId");

                    b.HasIndex("UserCreatorId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastDateModified")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastUserModifierId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LastUserModifierId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserCreatorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BlogId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastDateModified")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastUserModifierId")
                        .HasColumnType("bigint");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("LastUserModifierId");

                    b.HasIndex("UserCreatorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateLastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserLastModifiedId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatorId");

                    b.HasIndex("UserLastModifiedId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.Blog", b =>
                {
                    b.HasOne("BLOG_API.DB.Models.User", "LastUserModifier")
                        .WithMany()
                        .HasForeignKey("LastUserModifierId");

                    b.HasOne("BLOG_API.DB.Models.User", "UserCreator")
                        .WithMany("BlogsCreated")
                        .HasForeignKey("UserCreatorId");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.Comment", b =>
                {
                    b.HasOne("BLOG_API.DB.Models.User", "LastUserModifier")
                        .WithMany()
                        .HasForeignKey("LastUserModifierId");

                    b.HasOne("BLOG_API.DB.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BLOG_API.DB.Models.User", "UserCreator")
                        .WithMany("Comments")
                        .HasForeignKey("UserCreatorId");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.Post", b =>
                {
                    b.HasOne("BLOG_API.DB.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BLOG_API.DB.Models.User", "LastUserModifier")
                        .WithMany()
                        .HasForeignKey("LastUserModifierId");

                    b.HasOne("BLOG_API.DB.Models.User", "UserCreator")
                        .WithMany("Posts")
                        .HasForeignKey("UserCreatorId");
                });

            modelBuilder.Entity("BLOG_API.DB.Models.User", b =>
                {
                    b.HasOne("BLOG_API.DB.Models.User", "UserCreator")
                        .WithMany()
                        .HasForeignKey("UserCreatorId");

                    b.HasOne("BLOG_API.DB.Models.User", "UserLastModified")
                        .WithMany()
                        .HasForeignKey("UserLastModifiedId");
                });
#pragma warning restore 612, 618
        }
    }
}
