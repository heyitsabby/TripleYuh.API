﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230501044411_AddParentComment")]
    partial class AddParentComment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("boolean")
                        .HasColumnName("accept_terms");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("password_reset");

                    b.Property<int>("Reputation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1)
                        .HasColumnName("reputation");

                    b.Property<string>("ResetToken")
                        .HasColumnType("text")
                        .HasColumnName("reset_token");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reset_token_expires");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("username");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("text")
                        .HasColumnName("verification_token");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("verified");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("account_id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("character varying(10000)")
                        .HasColumnName("body");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<int>("PostId")
                        .HasColumnType("integer")
                        .HasColumnName("post_id");

                    b.Property<int>("Reputation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1)
                        .HasColumnName("reputation");

                    b.HasKey("Id")
                        .HasName("pk_comments");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_comments_account_id");

                    b.HasIndex("ParentId")
                        .HasDatabaseName("ix_comments_parent_id");

                    b.HasIndex("PostId")
                        .HasDatabaseName("ix_comments_post_id");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<int>("Reputation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1)
                        .HasColumnName("reputation");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(260)
                        .HasColumnType("character varying(260)")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Text")
                        .HasColumnName("type");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.HasKey("Id")
                        .HasName("pk_posts");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_posts_account_id");

                    b.ToTable("posts", (string)null);

                    b.HasDiscriminator<string>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("text")
                        .HasColumnName("created_by_ip");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("text")
                        .HasColumnName("reason_revoked");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("text")
                        .HasColumnName("replaced_by_token");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("revoked");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("text")
                        .HasColumnName("revoked_by_ip");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("Id")
                        .HasName("pk_refresh_tokens");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("ix_refresh_tokens_account_id");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.LinkPost", b =>
                {
                    b.HasBaseType("Domain.Entities.Post");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasDiscriminator().HasValue("Link");
                });

            modelBuilder.Entity("Domain.Entities.TextPost", b =>
                {
                    b.HasBaseType("Domain.Entities.Post");

                    b.Property<string>("Body")
                        .HasMaxLength(42000)
                        .HasColumnType("character varying(42000)")
                        .HasColumnName("body");

                    b.HasDiscriminator().HasValue("Text");
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Comments")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_accounts_account_id");

                    b.HasOne("Domain.Entities.Comment", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .HasConstraintName("fk_comments_comments_parent_id");

                    b.HasOne("Domain.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_posts_post_id");

                    b.Navigation("Account");

                    b.Navigation("Parent");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Posts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_posts_accounts_account_id");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_refresh_tokens_accounts_account_id");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
