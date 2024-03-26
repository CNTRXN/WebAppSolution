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
                    { 1, "Пользователь" },
                    { 2, "Мастер" },
                    { 3, "Администратор" }
                });

            migrationBuilder.InsertData(
                table: "Пользователи",
                columns: new[] { "Id", "Birthday", "Login", "Name", "Password", "Patronymic", "PermissionId", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1993, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", "Patronymic1", 2, "Surname-1" },
                    { 2, new DateTime(2000, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", null, 1, "Surname-2" },
                    { 3, new DateTime(2004, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", "Patronymic3", 1, "Surname-3" },
                    { 4, new DateTime(1992, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", "Patronymic4", 2, "Surname-4" },
                    { 5, new DateTime(1997, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", "Patronymic5", 1, "Surname-5" },
                    { 6, new DateTime(1992, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", "Patronymic6", 1, "Surname-6" },
                    { 7, new DateTime(1998, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", "Patronymic7", 2, "Surname-7" },
                    { 8, new DateTime(2003, 11, 18, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", null, 2, "Surname-8" },
                    { 9, new DateTime(1991, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", "Patronymic9", 1, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "AccountStats",
                columns: new[] { "Id", "ExpirationKeyDate", "SignInKey", "UserId" },
                values: new object[] { 1, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "f4ba7aaa-1590-4919-912c-c9ef5dd6ee25", 1 });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Group", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 2, 3472, 296.0, 343.0, 347237, 12088, 5, 263.0 },
                    { 2, 2, 1189, 122.0, 252.0, 118939, 16838, 7, 478.0 },
                    { 3, 1, 3541, 468.0, 395.0, 354138, 26409, 3, 238.0 },
                    { 4, 1, 1513, 461.0, 303.0, 151310, 28390, 5, 481.0 },
                    { 5, 2, 1489, 442.0, 267.0, 148910, 29773, 4, 168.0 },
                    { 6, 0, 1535, 103.0, 231.0, 153530, 29928, 3, 437.0 },
                    { 7, 0, 1126, 430.0, 286.0, 112645, 35884, 4, 394.0 },
                    { 8, 1, 2801, 373.0, 353.0, 280120, 13202, 7, 115.0 },
                    { 9, 1, 1566, 358.0, 301.0, 156629, 39296, 3, 275.0 },
                    { 10, 1, 1152, 335.0, 300.0, 115247, 36829, 3, 350.0 },
                    { 11, 0, 3361, 394.0, 351.0, 336114, 36726, 2, 131.0 },
                    { 12, 1, 3620, 457.0, 175.0, 362035, 25020, 6, 349.0 },
                    { 13, 2, 2626, 110.0, 226.0, 262643, 18753, 7, 121.0 },
                    { 14, 0, 2576, 224.0, 170.0, 257614, 30775, 1, 232.0 },
                    { 15, 0, 1636, 109.0, 185.0, 163634, 18169, 2, 162.0 },
                    { 16, 0, 3277, 133.0, 498.0, 327738, 12443, 7, 493.0 },
                    { 17, 0, 2842, 222.0, 119.0, 284231, 32178, 6, 467.0 },
                    { 18, 1, 1331, 495.0, 356.0, 133146, 25833, 4, 325.0 },
                    { 19, 1, 1547, 117.0, 453.0, 154732, 39553, 6, 395.0 }
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
