﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebAPI.DataContext;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(DB_Context))]
    [Migration("20240505135953_database")]
    partial class database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebAPI.DataContext.Models.AccountStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpirationKeyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SignInKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountStats");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Cabinet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<int>("Group")
                        .HasColumnType("integer");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<int>("Num")
                        .HasColumnType("integer");

                    b.Property<int>("PlanNum")
                        .HasColumnType("integer");

                    b.Property<int>("ResponsiblePersonId")
                        .HasColumnType("integer");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ResponsiblePersonId");

                    b.ToTable("Кабинеты");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Floor = 0,
                            Group = 3079,
                            Height = 192.0,
                            Length = 333.0,
                            Num = 216942,
                            PlanNum = 35122,
                            ResponsiblePersonId = 2,
                            Width = 259.0
                        },
                        new
                        {
                            Id = 2,
                            Floor = 0,
                            Group = 3991,
                            Height = 322.0,
                            Length = 491.0,
                            Num = 399141,
                            PlanNum = 20121,
                            ResponsiblePersonId = 1,
                            Width = 425.0
                        },
                        new
                        {
                            Id = 3,
                            Floor = 0,
                            Group = 2025,
                            Height = 351.0,
                            Length = 108.0,
                            Num = 202542,
                            PlanNum = 13469,
                            ResponsiblePersonId = 6,
                            Width = 332.0
                        },
                        new
                        {
                            Id = 4,
                            Floor = 1,
                            Group = 2014,
                            Height = 323.0,
                            Length = 403.0,
                            Num = 201423,
                            PlanNum = 34947,
                            ResponsiblePersonId = 3,
                            Width = 367.0
                        },
                        new
                        {
                            Id = 5,
                            Floor = 0,
                            Group = 3750,
                            Height = 236.0,
                            Length = 220.0,
                            Num = 375036,
                            PlanNum = 35605,
                            ResponsiblePersonId = 1,
                            Width = 399.0
                        },
                        new
                        {
                            Id = 6,
                            Floor = 0,
                            Group = 2332,
                            Height = 487.0,
                            Length = 212.0,
                            Num = 233220,
                            PlanNum = 15859,
                            ResponsiblePersonId = 3,
                            Width = 101.0
                        },
                        new
                        {
                            Id = 7,
                            Floor = 0,
                            Group = 2008,
                            Height = 281.0,
                            Length = 355.0,
                            Num = 200840,
                            PlanNum = 35635,
                            ResponsiblePersonId = 6,
                            Width = 107.0
                        },
                        new
                        {
                            Id = 8,
                            Floor = 2,
                            Group = 1838,
                            Height = 200.0,
                            Length = 106.0,
                            Num = 183812,
                            PlanNum = 38513,
                            ResponsiblePersonId = 1,
                            Width = 468.0
                        },
                        new
                        {
                            Id = 9,
                            Floor = 0,
                            Group = 1046,
                            Height = 342.0,
                            Length = 255.0,
                            Num = 104624,
                            PlanNum = 30690,
                            ResponsiblePersonId = 2,
                            Width = 312.0
                        },
                        new
                        {
                            Id = 10,
                            Floor = 0,
                            Group = 1707,
                            Height = 276.0,
                            Length = 373.0,
                            Num = 170718,
                            PlanNum = 10157,
                            ResponsiblePersonId = 8,
                            Width = 220.0
                        },
                        new
                        {
                            Id = 11,
                            Floor = 0,
                            Group = 2194,
                            Height = 381.0,
                            Length = 488.0,
                            Num = 219442,
                            PlanNum = 17780,
                            ResponsiblePersonId = 2,
                            Width = 107.0
                        },
                        new
                        {
                            Id = 12,
                            Floor = 1,
                            Group = 1835,
                            Height = 324.0,
                            Length = 315.0,
                            Num = 183544,
                            PlanNum = 17032,
                            ResponsiblePersonId = 1,
                            Width = 466.0
                        },
                        new
                        {
                            Id = 13,
                            Floor = 1,
                            Group = 1763,
                            Height = 419.0,
                            Length = 219.0,
                            Num = 176327,
                            PlanNum = 17126,
                            ResponsiblePersonId = 8,
                            Width = 131.0
                        },
                        new
                        {
                            Id = 14,
                            Floor = 1,
                            Group = 1673,
                            Height = 355.0,
                            Length = 451.0,
                            Num = 167347,
                            PlanNum = 11934,
                            ResponsiblePersonId = 1,
                            Width = 450.0
                        },
                        new
                        {
                            Id = 15,
                            Floor = 1,
                            Group = 2891,
                            Height = 178.0,
                            Length = 172.0,
                            Num = 289144,
                            PlanNum = 20793,
                            ResponsiblePersonId = 7,
                            Width = 423.0
                        },
                        new
                        {
                            Id = 16,
                            Floor = 2,
                            Group = 1141,
                            Height = 195.0,
                            Length = 308.0,
                            Num = 114123,
                            PlanNum = 13144,
                            ResponsiblePersonId = 1,
                            Width = 449.0
                        },
                        new
                        {
                            Id = 17,
                            Floor = 2,
                            Group = 2837,
                            Height = 245.0,
                            Length = 197.0,
                            Num = 283712,
                            PlanNum = 25065,
                            ResponsiblePersonId = 3,
                            Width = 459.0
                        },
                        new
                        {
                            Id = 18,
                            Floor = 2,
                            Group = 2160,
                            Height = 150.0,
                            Length = 264.0,
                            Num = 216020,
                            PlanNum = 15304,
                            ResponsiblePersonId = 3,
                            Width = 416.0
                        },
                        new
                        {
                            Id = 19,
                            Floor = 0,
                            Group = 1976,
                            Height = 184.0,
                            Length = 257.0,
                            Num = 197649,
                            PlanNum = 39393,
                            ResponsiblePersonId = 1,
                            Width = 315.0
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.CabinetEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CabinetId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CabinetId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("Оборудование в кабинетах");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CabinetId = 1,
                            Count = 5,
                            EquipmentId = 1
                        },
                        new
                        {
                            Id = 2,
                            CabinetId = 1,
                            Count = 5,
                            EquipmentId = 1
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.CabinetFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CabinetId")
                        .HasColumnType("integer");

                    b.Property<int?>("FileAuthor")
                        .HasColumnType("integer");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CabinetId");

                    b.HasIndex("FileAuthor");

                    b.ToTable("Файлы кабинета");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Оборудование");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 5,
                            Description = "ПК",
                            Name = "Монитор",
                            TypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Count = 5,
                            Description = "ПК",
                            Name = "Системный блок",
                            TypeId = 1
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.EquipmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Типы оборудования");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Электрооборудование"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Мебель"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Инфраструктура"
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Права доступа");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Master"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CabId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CompleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("FromId")
                        .HasColumnType("integer");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("RequestTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CabId");

                    b.HasIndex("FromId");

                    b.HasIndex("RequestStatusId");

                    b.HasIndex("RequestTypeId");

                    b.ToTable("Заявки");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.RequestEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipmentId")
                        .HasColumnType("integer");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("RequestId");

                    b.ToTable("Оборудование заявок");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.RequestFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Файлы заявки");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.RequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Статус заявки");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StatusName = "Новая"
                        },
                        new
                        {
                            Id = 2,
                            StatusName = "В обработке"
                        },
                        new
                        {
                            Id = 3,
                            StatusName = "Ожидание материалов"
                        },
                        new
                        {
                            Id = 4,
                            StatusName = "Выполняется"
                        },
                        new
                        {
                            Id = 5,
                            StatusName = "Выполнена"
                        },
                        new
                        {
                            Id = 6,
                            StatusName = "Отклонена"
                        },
                        new
                        {
                            Id = 7,
                            StatusName = "Отложена"
                        },
                        new
                        {
                            Id = 8,
                            StatusName = "Просрочена"
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.RequestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Тип заявки");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TypeName = "Ремонт"
                        },
                        new
                        {
                            Id = 2,
                            TypeName = "Тех. обслуживание"
                        },
                        new
                        {
                            Id = 3,
                            TypeName = "Закупка"
                        },
                        new
                        {
                            Id = 4,
                            TypeName = "Установка"
                        },
                        new
                        {
                            Id = 5,
                            TypeName = "Консультация"
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.ToTable("Пользователи");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthday = new DateTime(1998, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_1",
                            Name = "Name-1",
                            Password = "password_1",
                            PermissionId = 1,
                            Surname = "Surname-1"
                        },
                        new
                        {
                            Id = 2,
                            Birthday = new DateTime(1990, 6, 20, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_2",
                            Name = "Name-2",
                            Password = "password_2",
                            Patronymic = "Patronymic2",
                            PermissionId = 1,
                            Surname = "Surname-2"
                        },
                        new
                        {
                            Id = 3,
                            Birthday = new DateTime(1990, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_3",
                            Name = "Name-3",
                            Password = "password_3",
                            PermissionId = 1,
                            Surname = "Surname-3"
                        },
                        new
                        {
                            Id = 4,
                            Birthday = new DateTime(1999, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_4",
                            Name = "Name-4",
                            Password = "password_4",
                            PermissionId = 1,
                            Surname = "Surname-4"
                        },
                        new
                        {
                            Id = 5,
                            Birthday = new DateTime(1994, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_5",
                            Name = "Name-5",
                            Password = "password_5",
                            PermissionId = 1,
                            Surname = "Surname-5"
                        },
                        new
                        {
                            Id = 6,
                            Birthday = new DateTime(1996, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_6",
                            Name = "Name-6",
                            Password = "password_6",
                            Patronymic = "Patronymic6",
                            PermissionId = 1,
                            Surname = "Surname-6"
                        },
                        new
                        {
                            Id = 7,
                            Birthday = new DateTime(1990, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_7",
                            Name = "Name-7",
                            Password = "password_7",
                            PermissionId = 1,
                            Surname = "Surname-7"
                        },
                        new
                        {
                            Id = 8,
                            Birthday = new DateTime(2001, 3, 18, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_8",
                            Name = "Name-8",
                            Password = "password_8",
                            PermissionId = 1,
                            Surname = "Surname-8"
                        },
                        new
                        {
                            Id = 9,
                            Birthday = new DateTime(1991, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc),
                            Login = "login_9",
                            Name = "Name-9",
                            Password = "password_9",
                            Patronymic = "Patronymic9",
                            PermissionId = 1,
                            Surname = "Surname-9"
                        });
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.AccountStat", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Cabinet", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("ResponsiblePersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.CabinetEquipment", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.Cabinet", "Cabinet")
                        .WithMany()
                        .HasForeignKey("CabinetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.DataContext.Models.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cabinet");

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.CabinetFiles", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.Cabinet", "Cabinet")
                        .WithMany()
                        .HasForeignKey("CabinetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.DataContext.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("FileAuthor");

                    b.Navigation("Cabinet");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Equipment", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.EquipmentType", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentType");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.Request", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.Cabinet", "Cabinet")
                        .WithMany()
                        .HasForeignKey("CabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.DataContext.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("WebAPI.DataContext.Models.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.DataContext.Models.RequestType", "RequestType")
                        .WithMany()
                        .HasForeignKey("RequestTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cabinet");

                    b.Navigation("RequestStatus");

                    b.Navigation("RequestType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.RequestEquipment", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.DataContext.Models.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.RequestFiles", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("WebAPI.DataContext.Models.User", b =>
                {
                    b.HasOne("WebAPI.DataContext.Models.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");
                });
#pragma warning restore 612, 618
        }
    }
}