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
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 9, 2, 277.0, 276.0, 312535, 36457, null, 325.0 },
                    { 10, 1, 165.0, 175.0, 286527, 13197, null, 366.0 },
                    { 11, 1, 323.0, 183.0, 301945, 11326, null, 472.0 },
                    { 12, 0, 359.0, 141.0, 103322, 13989, null, 343.0 },
                    { 13, 1, 419.0, 483.0, 192411, 37925, null, 429.0 },
                    { 14, 2, 407.0, 401.0, 179146, 37056, null, 240.0 },
                    { 15, 2, 126.0, 422.0, 149611, 39183, null, 161.0 },
                    { 16, 2, 141.0, 493.0, 338129, 35671, null, 219.0 },
                    { 17, 2, 475.0, 288.0, 367715, 18299, null, 318.0 },
                    { 18, 0, 414.0, 381.0, 255442, 31633, null, 405.0 },
                    { 19, 0, 431.0, 263.0, 330415, 29661, null, 349.0 }
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
                table: "Пользователи",
                columns: new[] { "Id", "Birthday", "Login", "Name", "Password", "Patronymic", "PermissionId", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), "login_1", "Name-1", "password_1", "Patronymic1", 1, "Surname-1" },
                    { 2, new DateTime(1994, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "login_2", "Name-2", "password_2", "Patronymic2", 1, "Surname-2" },
                    { 3, new DateTime(1997, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), "login_3", "Name-3", "password_3", "Patronymic3", 1, "Surname-3" },
                    { 4, new DateTime(1998, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "login_4", "Name-4", "password_4", "Patronymic4", 1, "Surname-4" },
                    { 5, new DateTime(1991, 5, 27, 0, 0, 0, 0, DateTimeKind.Utc), "login_5", "Name-5", "password_5", "Patronymic5", 1, "Surname-5" },
                    { 6, new DateTime(1993, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "login_6", "Name-6", "password_6", null, 1, "Surname-6" },
                    { 7, new DateTime(1998, 5, 24, 0, 0, 0, 0, DateTimeKind.Utc), "login_7", "Name-7", "password_7", "Patronymic7", 1, "Surname-7" },
                    { 8, new DateTime(1992, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc), "login_8", "Name-8", "password_8", null, 1, "Surname-8" },
                    { 9, new DateTime(2004, 9, 22, 0, 0, 0, 0, DateTimeKind.Utc), "login_9", "Name-9", "password_9", "Patronymic9", 1, "Surname-9" }
                });

            migrationBuilder.InsertData(
                table: "Кабинеты",
                columns: new[] { "Id", "Floor", "Height", "Length", "Num", "PlanNum", "ResponsiblePersonId", "Width" },
                values: new object[,]
                {
                    { 1, 0, 181.0, 385.0, 216942, 15340, 2, 173.0 },
                    { 2, 0, 242.0, 117.0, 170214, 14906, 5, 369.0 },
                    { 3, 0, 407.0, 330.0, 219124, 37563, 6, 210.0 },
                    { 4, 1, 212.0, 220.0, 226610, 13319, 3, 209.0 },
                    { 5, 2, 174.0, 478.0, 384818, 12592, 8, 139.0 },
                    { 6, 0, 253.0, 243.0, 308721, 38563, 7, 418.0 },
                    { 7, 0, 358.0, 206.0, 260214, 30715, 4, 227.0 },
                    { 8, 2, 199.0, 427.0, 201148, 11533, 1, 130.0 }
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
                column: "ResponsiblePersonId");

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
