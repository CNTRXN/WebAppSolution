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
                    InventoryNumber = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
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
                    ResponsiblePersonId = table.Column<int>(type: "integer", nullable: true),
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
                        principalColumn: "Id");
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
                    EquipmentId = table.Column<int>(type: "integer", nullable: false)
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
                table: "Пользователи",
                columns: new[] { "Id", "Birthday", "Login", "Name", "Password", "Patronymic", "PermissionId", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1998, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", null, 1, "Surname-1" },
                    { 2, new DateTime(1999, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", null, 1, "Surname-2" },
                    { 3, new DateTime(1993, 3, 14, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", "Patronymic3", 1, "Surname-3" },
                    { 4, new DateTime(2000, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", null, 1, "Surname-4" },
                    { 5, new DateTime(1993, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", null, 1, "Surname-5" },
                    { 6, new DateTime(1993, 11, 12, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", "Patronymic6", 1, "Surname-6" },
                    { 7, new DateTime(1993, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", null, 1, "Surname-7" },
                    { 8, new DateTime(1992, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", null, 1, "Surname-8" },
                    { 9, new DateTime(1993, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", "Patronymic9", 1, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 2, 498.0, 427.0, 216942, 21908, 5, 490.0 },
                    { 2, 2, 223.0, 252.0, 213126, 18846, 6, 254.0 },
                    { 3, 2, 469.0, 295.0, 156627, 37651, 2, 479.0 },
                    { 4, 2, 180.0, 378.0, 149047, 19617, 7, 110.0 },
                    { 5, 1, 216.0, 424.0, 105240, 25807, 1, 131.0 },
                    { 6, 1, 234.0, 410.0, 201017, 31813, 4, 413.0 },
                    { 7, 1, 474.0, 149.0, 265523, 23123, 3, 152.0 },
                    { 8, 0, 300.0, 224.0, 103718, 15520, 8, 232.0 },
                    { 9, 2, 373.0, 459.0, 336420, 22417, null, 320.0 },
                    { 10, 1, 146.0, 376.0, 269124, 24520, null, 236.0 },
                    { 11, 0, 444.0, 470.0, 115921, 34698, null, 107.0 },
                    { 12, 1, 236.0, 159.0, 385824, 13328, null, 373.0 },
                    { 13, 0, 293.0, 444.0, 162419, 12872, null, 376.0 },
                    { 14, 2, 424.0, 338.0, 105449, 30734, null, 210.0 },
                    { 15, 2, 374.0, 428.0, 288115, 10085, null, 251.0 },
                    { 16, 0, 303.0, 118.0, 201943, 38005, null, 302.0 },
                    { 17, 1, 347.0, 315.0, 218245, 20198, null, 374.0 },
                    { 18, 0, 449.0, 258.0, 294913, 37823, null, 279.0 },
                    { 19, 0, 425.0, 290.0, 237210, 29272, null, 276.0 }
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
                columns: new[] { "Id", "Description", "InventoryNumber", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, "Сис. требования: ", "10892", "Монитор", 1 },
                    { 2, "Сис. требования: ", "11090", "Монитор", 1 },
                    { 3, "Сис. требования: ", "10975", "Системный блок", 1 },
                    { 4, "Сис. требования: ", "11296", "Системный блок", 1 },
                    { 5, "Сис. требования: ", "12095", "Мышка", 1 }
                });

            

            migrationBuilder.InsertData(
                table: "Оборудование в кабинетах",
                columns: new[] { "Id", "CabinetId", "EquipmentId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 4 }
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
                name: "IX_Кабинеты_Num",
                table: "Кабинеты",
                column: "Num",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_PlanNum",
                table: "Кабинеты",
                column: "PlanNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Кабинеты_ResponsiblePersonId",
                table: "Кабинеты",
                column: "ResponsiblePersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Оборудование_InventoryNumber",
                table: "Оборудование",
                column: "InventoryNumber",
                unique: true);

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
                name: "IX_Пользователи_Login",
                table: "Пользователи",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Пользователи_PermissionId",
                table: "Пользователи",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Права доступа_Name",
                table: "Права доступа",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Статус заявки_StatusName",
                table: "Статус заявки",
                column: "StatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Тип заявки_TypeName",
                table: "Тип заявки",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Типы оборудования_Name",
                table: "Типы оборудования",
                column: "Name",
                unique: true);

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
