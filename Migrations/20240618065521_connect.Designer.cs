﻿// <auto-generated />
using System;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRM.Migrations
{
    [DbContext(typeof(CRMContext))]
    [Migration("20240618065521_connect")]
    partial class connect
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CRM.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressID"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CRM.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientStatusID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SourceID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClientID");

                    b.HasIndex("ClientStatusID");

                    b.HasIndex("SourceID");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CRM.Models.ClientActivity", b =>
                {
                    b.Property<int>("clientActivityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("clientActivityID"));

                    b.Property<DateTime>("Activity_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Activity_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Activity_Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Added_By_SalesExe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.HasKey("clientActivityID");

                    b.HasIndex("ClientID");

                    b.ToTable("ClientActivities");
                });

            modelBuilder.Entity("CRM.Models.ClientStage", b =>
                {
                    b.Property<int>("ClientStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientStatusID"));

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientStatusID");

                    b.ToTable("ClientStages");
                });

            modelBuilder.Entity("CRM.Models.Communication", b =>
                {
                    b.Property<int>("CommunicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommunicationID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeadID")
                        .HasColumnType("int");

                    b.HasKey("CommunicationID");

                    b.HasIndex("LeadID");

                    b.ToTable("Communications");
                });

            modelBuilder.Entity("CRM.Models.Deal", b =>
                {
                    b.Property<int>("deal_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("deal_id"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("Total_Amount")
                        .HasColumnType("int");

                    b.Property<int>("amount_received")
                        .HasColumnType("int");

                    b.Property<DateTime>("closing_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("deal_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("deal_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deal_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pending_amount")
                        .HasColumnType("int");

                    b.Property<string>("product_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("deal_id");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("CRM.Models.FinancialDetails", b =>
                {
                    b.Property<int>("FID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FID"));

                    b.Property<int>("AnnualIncome")
                        .HasColumnType("int");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("IncomeSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FID");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.ToTable("FinancialDetails");
                });

            modelBuilder.Entity("CRM.Models.Lead", b =>
                {
                    b.Property<int>("LeadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeadID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LeadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeadDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeadName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeadStageID")
                        .HasColumnType("int");

                    b.HasKey("LeadID");

                    b.HasIndex("ClientID");

                    b.HasIndex("LeadStageID");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("CRM.Models.LeadStage", b =>
                {
                    b.Property<int>("LeadStageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeadStageID"));

                    b.Property<string>("LeadStageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeadStageID");

                    b.ToTable("LeadStages");
                });

            modelBuilder.Entity("CRM.Models.Source", b =>
                {
                    b.Property<int>("SourceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SourceID"));

                    b.Property<string>("SourceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SourceID");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("CRM.Models.Transaction", b =>
                {
                    b.Property<int>("Transaction_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Transaction_id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Transaction_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("deal_id")
                        .HasColumnType("int");

                    b.HasKey("Transaction_id");

                    b.HasIndex("deal_id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CRM.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRM.Models.Address", b =>
                {
                    b.HasOne("CRM.Models.Client", "Client")
                        .WithOne("Address")
                        .HasForeignKey("CRM.Models.Address", "ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CRM.Models.Client", b =>
                {
                    b.HasOne("CRM.Models.ClientStage", "ClientStatus")
                        .WithMany()
                        .HasForeignKey("ClientStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.User", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId");

                    b.Navigation("ClientStatus");

                    b.Navigation("Source");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRM.Models.ClientActivity", b =>
                {
                    b.HasOne("CRM.Models.Client", "Client")
                        .WithMany("ClientActivity")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CRM.Models.Communication", b =>
                {
                    b.HasOne("CRM.Models.Lead", "Lead")
                        .WithMany("Communications")
                        .HasForeignKey("LeadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lead");
                });

            modelBuilder.Entity("CRM.Models.Deal", b =>
                {
                    b.HasOne("CRM.Models.Client", "Client")
                        .WithOne("Deal")
                        .HasForeignKey("CRM.Models.Deal", "ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CRM.Models.FinancialDetails", b =>
                {
                    b.HasOne("CRM.Models.Client", "Client")
                        .WithOne("FinancialDetails")
                        .HasForeignKey("CRM.Models.FinancialDetails", "ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CRM.Models.Lead", b =>
                {
                    b.HasOne("CRM.Models.Client", "Client")
                        .WithMany("Lead")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Models.LeadStage", "LeadStage")
                        .WithMany()
                        .HasForeignKey("LeadStageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("LeadStage");
                });

            modelBuilder.Entity("CRM.Models.Transaction", b =>
                {
                    b.HasOne("CRM.Models.Deal", "Deal")
                        .WithMany("Transactions")
                        .HasForeignKey("deal_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deal");
                });

            modelBuilder.Entity("CRM.Models.Client", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("ClientActivity");

                    b.Navigation("Deal")
                        .IsRequired();

                    b.Navigation("FinancialDetails")
                        .IsRequired();

                    b.Navigation("Lead");
                });

            modelBuilder.Entity("CRM.Models.Deal", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("CRM.Models.Lead", b =>
                {
                    b.Navigation("Communications");
                });

            modelBuilder.Entity("CRM.Models.User", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
