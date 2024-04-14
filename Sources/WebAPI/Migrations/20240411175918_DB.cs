using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Права",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Права", x => x.Id);
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
                        name: "FK_Пользователи_Права_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Права",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Оборудоввание",
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
                    table.PrimaryKey("PK_Оборудоввание", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Оборудоввание_Типы оборудования_TypeId",
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
                name: "Оборудование в кабинетах",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CabId = table.Column<int>(type: "integer", nullable: false),
                    EquipId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Оборудование в кабинетах", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Оборудование в кабинетах_Кабинеты_CabId",
                        column: x => x.CabId,
                        principalTable: "Кабинеты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Оборудование в кабинетах_Оборудоввание_EquipId",
                        column: x => x.EquipId,
                        principalTable: "Оборудоввание",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Фотографии кабинета",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CabId = table.Column<int>(type: "integer", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    ImageAuthor = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Фотографии кабинета", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Фотографии кабинета_Кабинеты_CabId",
                        column: x => x.CabId,
                        principalTable: "Кабинеты",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Фотографии кабинета_Пользователи_ImageAuthor",
                        column: x => x.ImageAuthor,
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
                    AcceptanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ImageId = table.Column<int>(type: "integer", nullable: true),
                    FromId = table.Column<int>(type: "integer", nullable: false),
                    CabId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Заявки", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Заявки_Кабинеты_CabId",
                        column: x => x.CabId,
                        principalTable: "Кабинеты",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Заявки_Пользователи_FromId",
                        column: x => x.FromId,
                        principalTable: "Пользователи",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Заявки_Фотографии кабинета_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Фотографии кабинета",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Права",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Master" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Пользователи",
                columns: new[] { "Id", "Birthday", "Login", "Name", "Password", "Patronymic", "PermissionId", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1994, 3, 18, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", "Patronymic1", 1, "Surname-1" },
                    { 2, new DateTime(1993, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", null, 1, "Surname-2" },
                    { 3, new DateTime(1995, 3, 27, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", "Patronymic3", 2, "Surname-3" },
                    { 4, new DateTime(1995, 11, 19, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", "Patronymic4", 1, "Surname-4" },
                    { 5, new DateTime(2004, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", null, 2, "Surname-5" },
                    { 6, new DateTime(2000, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", null, 2, "Surname-6" },
                    { 7, new DateTime(1990, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", "Patronymic7", 1, "Surname-7" },
                    { 8, new DateTime(1995, 11, 19, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", "Patronymic8", 2, "Surname-8" },
                    { 9, new DateTime(1997, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", null, 2, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "AccountStats",
                columns: new[] { "Id", "ExpirationKeyDate", "SignInKey", "UserId" },
                values: new object[] { 1, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ebf54d3b-e3bf-4093-af54-d74fd8cf0e7b", 1 });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Group", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 2, 1767, 107.0, 295.0, 176737, 20936, 5, 314.0 },
                    { 2, 0, 1748, 229.0, 232.0, 174811, 11862, 4, 198.0 },
                    { 3, 0, 3828, 175.0, 353.0, 382822, 23170, 3, 195.0 },
                    { 4, 0, 1722, 215.0, 278.0, 172223, 30791, 7, 418.0 },
                    { 5, 0, 3949, 428.0, 456.0, 394917, 20097, 7, 193.0 },
                    { 6, 1, 3999, 395.0, 423.0, 399948, 33820, 7, 107.0 },
                    { 7, 0, 1263, 497.0, 410.0, 126344, 11984, 3, 170.0 },
                    { 8, 2, 1826, 373.0, 284.0, 182617, 35537, 5, 342.0 },
                    { 9, 1, 1145, 460.0, 332.0, 114535, 14601, 2, 132.0 },
                    { 10, 2, 3510, 468.0, 403.0, 351028, 33685, 4, 191.0 },
                    { 11, 2, 2691, 102.0, 406.0, 269131, 10630, 3, 159.0 },
                    { 12, 2, 1119, 308.0, 111.0, 111921, 23146, 5, 263.0 },
                    { 13, 1, 3193, 220.0, 156.0, 319340, 13960, 8, 171.0 },
                    { 14, 0, 1433, 345.0, 195.0, 143344, 18394, 7, 268.0 },
                    { 15, 1, 3982, 431.0, 287.0, 398247, 17852, 5, 436.0 },
                    { 16, 0, 2515, 167.0, 349.0, 251528, 13832, 6, 139.0 },
                    { 17, 2, 2026, 109.0, 407.0, 202636, 31463, 6, 120.0 },
                    { 18, 1, 1890, 486.0, 489.0, 189030, 24290, 6, 235.0 },
                    { 19, 1, 3349, 218.0, 366.0, 334946, 37591, 3, 211.0 }
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
                name: "IX_Заявки_ImageId",
                table: "Заявки",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_ResponsiblePersonId",
                table: "Кабинеты",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование в кабинетах_CabId",
                table: "Оборудование в кабинетах",
                column: "CabId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование в кабинетах_EquipId",
                table: "Оборудование в кабинетах",
                column: "EquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудоввание_TypeId",
                table: "Оборудоввание",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Пользователи_PermissionId",
                table: "Пользователи",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Фотографии кабинета_CabId",
                table: "Фотографии кабинета",
                column: "CabId");

            migrationBuilder.CreateIndex(
                name: "IX_Фотографии кабинета_ImageAuthor",
                table: "Фотографии кабинета",
                column: "ImageAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStats_SignInKey",
                table: "AccountStats",
                column: "SignInKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountStats_UserId",
                table: "AccountStats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Заявки");

            migrationBuilder.DropTable(
                name: "Оборудование в кабинетах");

            migrationBuilder.DropTable(
                name: "AccountStats");

            migrationBuilder.DropTable(
                name: "Фотографии кабинета");

            migrationBuilder.DropTable(
                name: "Оборудоввание");

            migrationBuilder.DropTable(
                name: "Кабинеты");

            migrationBuilder.DropTable(
                name: "Типы оборудования");

            migrationBuilder.DropTable(
                name: "Пользователи");

            migrationBuilder.DropTable(
                name: "Права");
        }
    }
}
