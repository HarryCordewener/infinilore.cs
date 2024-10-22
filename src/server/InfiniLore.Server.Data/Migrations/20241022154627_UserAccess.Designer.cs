﻿// <auto-generated />
using System;
using InfiniLore.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfiniLore.Server.Data.Migrations
{
    [DbContext(typeof(InfiniLoreDbContext))]
    [Migration("20241022154627_UserAccess")]
    partial class UserAccess
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "d957c0f8-e90e-4068-a968-4f4b49fc165c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "89bd417a-a110-4cf2-8a83-09dc79e47335",
                            Email = "testuser@example.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "TESTUSER@EXAMPLE.COM",
                            NormalizedUserName = "TESTUSER",
                            PasswordHash = "AQAAAAIAAYagAAAAEB5p3i60R7of7y4lr9a8K13lTFUUhOYvDP6a38zvLQMYNCmfqaQ0KL2r9XF0KYX4sw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d957c0f8-e90e-4068-a968-4f4b49fc165b",
                            TwoFactorEnabled = false,
                            UserName = "testuser"
                        });
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Account.JwtRefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExpiresInDays")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TokenHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TokenHash")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("JwtRefreshTokens");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Base.UserContentAccess<InfiniLore.Server.Data.Models.UserData.LoreScopeModel>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("LoreScopeModelId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LoreScopeModelId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContentAccess<LoreScopeModel>");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Base.UserContentAccess<InfiniLore.Server.Data.Models.UserData.MultiverseModel>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("MultiverseModelId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MultiverseModelId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContentAccess<MultiverseModel>");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Base.UserContentAccess<InfiniLore.Server.Data.Models.UserData.UniverseModel>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UniverseModelId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UniverseModelId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContentAccess<UniverseModel>");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.LoreScopeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.HasIndex("Name", "OwnerId")
                        .IsUnique();

                    b.HasIndex("Id", "OwnerId", "IsPublic");

                    b.ToTable("LoreScopes");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.MultiverseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("LoreScopeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LoreScopeId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Name", "LoreScopeId")
                        .IsUnique();

                    b.HasIndex("Id", "OwnerId", "IsPublic");

                    b.ToTable("Multiverses");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.UniverseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("MultiverseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SoftDeleteDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MultiverseId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("Name", "MultiverseId")
                        .IsUnique();

                    b.HasIndex("Id", "OwnerId", "IsPublic");

                    b.ToTable("Universes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "2ff7761f-b907-493b-8b89-43b2ea6f644f",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "f443a138-c5da-4f16-abac-b55ae062c731",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Account.JwtRefreshToken", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "User")
                        .WithMany("JwtRefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Base.UserContentAccess<InfiniLore.Server.Data.Models.UserData.LoreScopeModel>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.UserData.LoreScopeModel", null)
                        .WithMany("UserAccess")
                        .HasForeignKey("LoreScopeModelId");

                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Base.UserContentAccess<InfiniLore.Server.Data.Models.UserData.MultiverseModel>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.UserData.MultiverseModel", null)
                        .WithMany("UserAccess")
                        .HasForeignKey("MultiverseModelId");

                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Base.UserContentAccess<InfiniLore.Server.Data.Models.UserData.UniverseModel>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.UserData.UniverseModel", null)
                        .WithMany("UserAccess")
                        .HasForeignKey("UniverseModelId");

                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.LoreScopeModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "Owner")
                        .WithMany("LoreScopes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.MultiverseModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.UserData.LoreScopeModel", "LoreScope")
                        .WithMany("Multiverses")
                        .HasForeignKey("LoreScopeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "Owner")
                        .WithMany("Multiverses")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoreScope");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.UniverseModel", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.UserData.MultiverseModel", "Multiverse")
                        .WithMany("Universes")
                        .HasForeignKey("MultiverseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", "Owner")
                        .WithMany("Universes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Multiverse");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.Account.InfiniLoreUser", b =>
                {
                    b.Navigation("JwtRefreshTokens");

                    b.Navigation("LoreScopes");

                    b.Navigation("Multiverses");

                    b.Navigation("Universes");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.LoreScopeModel", b =>
                {
                    b.Navigation("Multiverses");

                    b.Navigation("UserAccess");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.MultiverseModel", b =>
                {
                    b.Navigation("Universes");

                    b.Navigation("UserAccess");
                });

            modelBuilder.Entity("InfiniLore.Server.Data.Models.UserData.UniverseModel", b =>
                {
                    b.Navigation("UserAccess");
                });
#pragma warning restore 612, 618
        }
    }
}
