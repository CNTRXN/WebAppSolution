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
                        name: "FK_Оборудование в кабинетах_Оборудование_EquipId",
                        column: x => x.EquipId,
                        principalTable: "Оборудование",
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
                table: "Права доступа",
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
                    { 1, new DateTime(1992, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", null, 2, "Surname-1" },
                    { 2, new DateTime(1991, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", null, 1, "Surname-2" },
                    { 3, new DateTime(1994, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", "Patronymic3", 1, "Surname-3" },
                    { 4, new DateTime(2004, 10, 26, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", "Patronymic4", 1, "Surname-4" },
                    { 5, new DateTime(1999, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", null, 1, "Surname-5" },
                    { 6, new DateTime(1995, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", "Patronymic6", 2, "Surname-6" },
                    { 7, new DateTime(1992, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", "Patronymic7", 1, "Surname-7" },
                    { 8, new DateTime(2000, 8, 2, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", "Patronymic8", 2, "Surname-8" },
                    { 9, new DateTime(2003, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", null, 1, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "AccountStats",
                columns: new[] { "Id", "ExpirationKeyDate", "SignInKey", "UserId" },
                values: new object[] { 1, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "cc02ca31-7c33-447e-9177-a78e4ae040e3", 1 });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Group", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 1, 2423, 108.0, 172.0, 216942, 19614, 7, 387.0 },
                    { 2, 1, 3884, 162.0, 484.0, 388427, 23843, 1, 330.0 },
                    { 3, 1, 3374, 281.0, 360.0, 337420, 10371, 7, 328.0 },
                    { 4, 0, 2081, 488.0, 293.0, 208125, 38595, 8, 247.0 },
                    { 5, 0, 1778, 237.0, 231.0, 177821, 27827, 4, 311.0 },
                    { 6, 0, 2765, 125.0, 276.0, 276526, 21421, 8, 342.0 },
                    { 7, 2, 2663, 196.0, 137.0, 266342, 12036, 6, 371.0 },
                    { 8, 0, 2500, 198.0, 208.0, 250036, 11580, 3, 285.0 },
                    { 9, 2, 3406, 175.0, 439.0, 340648, 28929, 5, 103.0 },
                    { 10, 0, 2492, 485.0, 272.0, 249230, 12616, 8, 317.0 },
                    { 11, 1, 3572, 317.0, 466.0, 357219, 14735, 6, 202.0 },
                    { 12, 0, 2509, 291.0, 367.0, 250947, 21029, 5, 270.0 },
                    { 13, 1, 2398, 427.0, 312.0, 239824, 14719, 8, 178.0 },
                    { 14, 0, 2346, 241.0, 107.0, 234623, 27697, 3, 247.0 },
                    { 15, 1, 1656, 290.0, 398.0, 165630, 33875, 3, 498.0 },
                    { 16, 2, 1747, 448.0, 413.0, 174739, 35518, 6, 188.0 },
                    { 17, 2, 3502, 188.0, 297.0, 350228, 36765, 7, 331.0 },
                    { 18, 1, 2518, 148.0, 225.0, 251849, 11370, 1, 365.0 },
                    { 19, 0, 1015, 140.0, 116.0, 101539, 34516, 4, 217.0 }
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
                name: "IX_Оборудование_TypeId",
                table: "Оборудование",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование в кабинетах_CabId",
                table: "Оборудование в кабинетах",
                column: "CabId");

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование в кабинетах_EquipId",
                table: "Оборудование в кабинетах",
                column: "EquipId");

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
                name: "Оборудование");

            migrationBuilder.DropTable(
                name: "Кабинеты");

            migrationBuilder.DropTable(
                name: "Типы оборудования");

            migrationBuilder.DropTable(
                name: "Пользователи");

            migrationBuilder.DropTable(
                name: "Права доступа");
        }
    }
}
