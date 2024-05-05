using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Права доступа",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Права доступа", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Статус заявки",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Статус заявки", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Тип заявки",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Тип заявки", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Типы оборудования",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Типы оборудования", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Пользователи",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Пользователи", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Пользователи_Права доступа_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Права доступа",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Оборудование",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Оборудование", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Оборудование_Типы оборудования_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Типы оборудования",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Кабинеты",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Num = table.Column<int>(type: "integer", nullable: false),
                    PlanNum = table.Column<int>(type: "integer", nullable: false),
                    ResponsiblePersonId = table.Column<int>(type: "integer", nullable: false),
                    Group = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Length = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Кабинеты", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Кабинеты_Пользователи_ResponsiblePersonId",
                        column: x => x.ResponsiblePersonId,
                        principalTable: "Пользователи",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SignInKey = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExpirationKeyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountStats_Пользователи_UserId",
                        column: x => x.UserId,
                        principalTable: "Пользователи",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Заявки",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RequestTypeId = table.Column<int>(type: "integer", nullable: false),
                    RequestStatusId = table.Column<int>(type: "integer", nullable: false),
                    FromId = table.Column<int>(type: "integer", nullable: true),
                    CabId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Заявки", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Заявки_Кабинеты_CabId",
                        column: x => x.CabId,
                        principalTable: "Кабинеты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Заявки_Пользователи_FromId",
                        column: x => x.FromId,
                        principalTable: "Пользователи",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Заявки_Статус заявки_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "Статус заявки",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Заявки_Тип заявки_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "Тип заявки",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Оборудование в кабинетах",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CabinetId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Оборудование в кабинетах", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Оборудование в кабинетах_Кабинеты_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Кабинеты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Оборудование в кабинетах_Оборудование_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Оборудование",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Файлы кабинета",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CabinetId = table.Column<int>(type: "integer", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileAuthor = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Файлы кабинета", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Файлы кабинета_Кабинеты_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Кабинеты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Файлы кабинета_Пользователи_FileAuthor",
                        column: x => x.FileAuthor,
                        principalTable: "Пользователи",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Оборудование заявок",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Оборудование заявок", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Оборудование заявок_Заявки_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Заявки",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Оборудование заявок_Оборудование_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Оборудование",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Файлы заявки",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Файлы заявки", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Файлы заявки_Заявки_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Заявки",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Права доступа",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Master" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Статус заявки",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Новая" },
                    { 2, "В обработке" },
                    { 3, "Ожидание материалов" },
                    { 4, "Выполняется" },
                    { 5, "Выполнена" },
                    { 6, "Отклонена" },
                    { 7, "Отложена" },
                    { 8, "Просрочена" }
                });

            migrationBuilder.InsertData(
                table: "Тип заявки",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Ремонт" },
                    { 2, "Тех. обслуживание" },
                    { 3, "Закупка" },
                    { 4, "Установка" },
                    { 5, "Консультация" }
                });

            migrationBuilder.InsertData(
                table: "Типы оборудования",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Электрооборудование" },
                    { 2, "Мебель" },
                    { 3, "Инфраструктура" }
                });

            migrationBuilder.InsertData(
                table: "Оборудование",
                columns: new[] { "Id", "Count", "Description", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, 5, "ПК", "Монитор", 1 },
                    { 2, 5, "ПК", "Системный блок", 1 }
                });

            migrationBuilder.InsertData(
                table: "Пользователи",
                columns: new[] { "Id", "Birthday", "Login", "Name", "Password", "Patronymic", "PermissionId", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1998, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", null, 1, "Surname-1" },
                    { 2, new DateTime(1990, 6, 20, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", "Patronymic2", 1, "Surname-2" },
                    { 3, new DateTime(1990, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", null, 1, "Surname-3" },
                    { 4, new DateTime(1999, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", null, 1, "Surname-4" },
                    { 5, new DateTime(1994, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", null, 1, "Surname-5" },
                    { 6, new DateTime(1996, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", "Patronymic6", 1, "Surname-6" },
                    { 7, new DateTime(1990, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", null, 1, "Surname-7" },
                    { 8, new DateTime(2001, 3, 18, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", null, 1, "Surname-8" },
                    { 9, new DateTime(1991, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", "Patronymic9", 1, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Group", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 0, 3079, 192.0, 333.0, 216942, 35122, 2, 259.0 },
                    { 2, 0, 3991, 322.0, 491.0, 399141, 20121, 1, 425.0 },
                    { 3, 0, 2025, 351.0, 108.0, 202542, 13469, 6, 332.0 },
                    { 4, 1, 2014, 323.0, 403.0, 201423, 34947, 3, 367.0 },
                    { 5, 0, 3750, 236.0, 220.0, 375036, 35605, 1, 399.0 },
                    { 6, 0, 2332, 487.0, 212.0, 233220, 15859, 3, 101.0 },
                    { 7, 0, 2008, 281.0, 355.0, 200840, 35635, 6, 107.0 },
                    { 8, 2, 1838, 200.0, 106.0, 183812, 38513, 1, 468.0 },
                    { 9, 0, 1046, 342.0, 255.0, 104624, 30690, 2, 312.0 },
                    { 10, 0, 1707, 276.0, 373.0, 170718, 10157, 8, 220.0 },
                    { 11, 0, 2194, 381.0, 488.0, 219442, 17780, 2, 107.0 },
                    { 12, 1, 1835, 324.0, 315.0, 183544, 17032, 1, 466.0 },
                    { 13, 1, 1763, 419.0, 219.0, 176327, 17126, 8, 131.0 },
                    { 14, 1, 1673, 355.0, 451.0, 167347, 11934, 1, 450.0 },
                    { 15, 1, 2891, 178.0, 172.0, 289144, 20793, 7, 423.0 },
                    { 16, 2, 1141, 195.0, 308.0, 114123, 13144, 1, 449.0 },
                    { 17, 2, 2837, 245.0, 197.0, 283712, 25065, 3, 459.0 },
                    { 18, 2, 2160, 150.0, 264.0, 216020, 15304, 3, 416.0 },
                    { 19, 0, 1976, 184.0, 257.0, 197649, 39393, 1, 315.0 }
                });

            migrationBuilder.InsertData(
                table: "Оборудование в кабинетах",
                columns: new[] { "Id", "CabinetId", "Count", "EquipmentId" },
                values: new object[,]
                {
                    { 1, 1, 5, 1 },
                    { 2, 1, 5, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Заявки_CabId",
                table: "Заявки",
                column: "CabId");

            migrationBuilder.CreateIndex(
                name: "IX_Заявки_FromId",
                table: "Заявки",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Заявки_RequestStatusId",
                table: "Заявки",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Заявки_RequestTypeId",
                table: "Заявки",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_ResponsiblePersonId",
                table: "Кабинеты",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование_TypeId",
                table: "Оборудование",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование в кабинетах_CabinetId",
                table: "Оборудование в кабинетах",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование в кабинетах_EquipmentId",
                table: "Оборудование в кабинетах",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование заявок_EquipmentId",
                table: "Оборудование заявок",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование заявок_RequestId",
                table: "Оборудование заявок",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Пользователи_PermissionId",
                table: "Пользователи",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Файлы заявки_RequestId",
                table: "Файлы заявки",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Файлы кабинета_CabinetId",
                table: "Файлы кабинета",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Файлы кабинета_FileAuthor",
                table: "Файлы кабинета",
                column: "FileAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStats_UserId",
                table: "AccountStats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Оборудование в кабинетах");

            migrationBuilder.DropTable(
                name: "Оборудование заявок");

            migrationBuilder.DropTable(
                name: "Файлы заявки");

            migrationBuilder.DropTable(
                name: "Файлы кабинета");

            migrationBuilder.DropTable(
                name: "AccountStats");

            migrationBuilder.DropTable(
                name: "Оборудование");

            migrationBuilder.DropTable(
                name: "Заявки");

            migrationBuilder.DropTable(
                name: "Типы оборудования");

            migrationBuilder.DropTable(
                name: "Кабинеты");

            migrationBuilder.DropTable(
                name: "Статус заявки");

            migrationBuilder.DropTable(
                name: "Тип заявки");

            migrationBuilder.DropTable(
                name: "Пользователи");

            migrationBuilder.DropTable(
                name: "Права доступа");
        }
    }
}
