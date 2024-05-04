using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
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
                    ImageAuthor = table.Column<int>(type: "integer", nullable: false)
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
                    CompleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RequestTypeId = table.Column<int>(type: "integer", nullable: false),
                    RequestStatusId = table.Column<int>(type: "integer", nullable: false),
                    FromId = table.Column<int>(type: "integer", nullable: true),
                    CabId = table.Column<int>(type: "integer", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Заявки_Фотографии кабинета_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Фотографии кабинета",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Фотографии заявки",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Фотографии заявки", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Фотографии заявки_Заявки_RequestId",
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
                    { 1, new DateTime(1995, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", null, 1, "Surname-1" },
                    { 2, new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", "Patronymic2", 1, "Surname-2" },
                    { 3, new DateTime(1993, 5, 21, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", null, 1, "Surname-3" },
                    { 4, new DateTime(2003, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", null, 1, "Surname-4" },
                    { 5, new DateTime(2002, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", "Patronymic5", 1, "Surname-5" },
                    { 6, new DateTime(2004, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", null, 1, "Surname-6" },
                    { 7, new DateTime(2000, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", "Patronymic7", 1, "Surname-7" },
                    { 8, new DateTime(1992, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", "Patronymic8", 1, "Surname-8" },
                    { 9, new DateTime(2003, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", null, 1, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Group", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 2, 1568, 431.0, 417.0, 216942, 21308, 3, 143.0 },
                    { 2, 2, 2395, 182.0, 206.0, 239540, 12458, 3, 289.0 },
                    { 3, 0, 2967, 440.0, 415.0, 296711, 30318, 5, 375.0 },
                    { 4, 2, 1781, 145.0, 428.0, 178124, 20629, 4, 241.0 },
                    { 5, 1, 2212, 363.0, 388.0, 221215, 10144, 4, 473.0 },
                    { 6, 2, 2626, 233.0, 318.0, 262641, 34268, 7, 456.0 },
                    { 7, 2, 2217, 214.0, 176.0, 221726, 26331, 4, 380.0 },
                    { 8, 1, 1912, 130.0, 422.0, 191228, 37255, 1, 202.0 },
                    { 9, 2, 2826, 257.0, 396.0, 282613, 39695, 6, 441.0 },
                    { 10, 2, 2691, 258.0, 363.0, 269146, 22172, 7, 279.0 },
                    { 11, 1, 1502, 149.0, 319.0, 150249, 18567, 3, 456.0 },
                    { 12, 0, 1568, 102.0, 144.0, 156841, 24861, 2, 136.0 },
                    { 13, 2, 2744, 306.0, 295.0, 274442, 29184, 2, 482.0 },
                    { 14, 0, 2615, 390.0, 492.0, 261521, 25692, 3, 487.0 },
                    { 15, 1, 2763, 415.0, 106.0, 276321, 21637, 4, 133.0 },
                    { 16, 2, 2917, 475.0, 163.0, 291744, 13196, 2, 362.0 },
                    { 17, 0, 2610, 144.0, 124.0, 261041, 35286, 6, 310.0 },
                    { 18, 0, 1482, 356.0, 314.0, 148228, 22759, 8, 340.0 },
                    { 19, 2, 3524, 318.0, 371.0, 352411, 35994, 5, 445.0 }
                });

            migrationBuilder.InsertData(
                table: "Оборудование в кабинетах",
                columns: new[] { "Id", "CabId", "Count", "EquipId" },
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
                name: "IX_Заявки_ImageId",
                table: "Заявки",
                column: "ImageId");

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
                name: "IX_Фотографии заявки_RequestId",
                table: "Фотографии заявки",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Фотографии кабинета_CabId",
                table: "Фотографии кабинета",
                column: "CabId");

            migrationBuilder.CreateIndex(
                name: "IX_Фотографии кабинета_ImageAuthor",
                table: "Фотографии кабинета",
                column: "ImageAuthor");

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
                name: "Фотографии заявки");

            migrationBuilder.DropTable(
                name: "AccountStats");

            migrationBuilder.DropTable(
                name: "Оборудование");

            migrationBuilder.DropTable(
                name: "Заявки");

            migrationBuilder.DropTable(
                name: "Типы оборудования");

            migrationBuilder.DropTable(
                name: "Статус заявки");

            migrationBuilder.DropTable(
                name: "Тип заявки");

            migrationBuilder.DropTable(
                name: "Фотографии кабинета");

            migrationBuilder.DropTable(
                name: "Кабинеты");

            migrationBuilder.DropTable(
                name: "Пользователи");

            migrationBuilder.DropTable(
                name: "Права доступа");
        }
    }
}
