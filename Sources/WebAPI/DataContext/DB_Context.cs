using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI.DataContext.Models;
using ConfigurationManager = System.Configuration;

namespace WebAPI.DataContext
{
    public class DB_Context(DbContextOptions<DB_Context> options) 
        : DbContext(options)
    {
        #region Кабинеты и оборудования
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<CabinetFiles> CabinetFiles { get; set; }
        public DbSet<CabinetEquipment> CabinetEquipments { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        #endregion

        #region Пользователи
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<AccountStat> AccountStats { get; set; }
        #endregion

        #region Заявки
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RequestFile> RequestFiles { get; set; }
        public DbSet<RequestEquipment> RequestEquipments { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Permission>()
                .HasData(Init_Permissions());

            modelBuilder
                .Entity<User>()
                .HasData(Init_Users());

            modelBuilder
                .Entity<Cabinet>()
                .HasData(Init_Cabs());

            modelBuilder
                .Entity<EquipmentType>()
                .HasData(Init_EquipmentType());

            modelBuilder
                .Entity<Equipment>()
                .HasData(Init_Equipments());

            modelBuilder
                .Entity<CabinetEquipment>()
                .HasData(Init_CabEquipments());

            /*modelBuilder
                .Entity<AccountStat>(entity =>
                {
                    entity.HasIndex(a => a.SignInKey).IsUnique();
                    entity.HasData(new List<AccountStat>()
                    {
                        new()
                        {
                            Id = 1,
                            SignInKey = Guid.NewGuid().ToString(),
                            UserId = 1,
                            ExpirationKeyDate = new DateTime(2024, 03, 25, 0, 0, 0, DateTimeKind.Utc)
                        },
                    });
                });*/

            modelBuilder
                .Entity<RequestType>()
                .HasData(Init_RequestTypes());

            modelBuilder
                .Entity<RequestStatus>()
                .HasData(Init_RequestsStatus());

            static List<Permission> Init_Permissions()
            {
                List<Permission> permissions = InitData<Permission>("PermissionsData");

                return permissions;
            }

            static List<User> Init_Users()
            {
                List<User> users = [];

                int i = 1;
                for (; i < 10; i++)
                {
                    users.Add(new User()
                    {
                        Id = i,
                        Login = $"login_{i}",
                        Password = $"password_{i}",
                        Name = $"Name-{i}",
                        Surname = $"Surname-{i}",
                        Patronymic = (new Random().Next(0, 2) == 0) ? null : $"Patronymic{i}",
                        Birthday = new DateTime(new Random().Next(1990, 2005), new Random().Next(1, 12), new Random().Next(1, 28), 0, 0, 0, DateTimeKind.Utc),
                        PermissionId = 1//new Random().Next(1, Init_Permissions().Count)
                    });
                }

                return users;
            }

            static List<Cabinet> Init_Cabs()
            {
                List<Cabinet> cabinets = [];

                int i = 1;
                for (; i < 20; i++)
                {
                    var cabinet = new Cabinet()
                    {
                        Id = i,
                        Floor = new Random().Next(0, 3),
                        Group = new Random().Next(1000, 4000),
                        Height = new Random().Next(100, 500),
                        Length = new Random().Next(100, 500),
                        Width = new Random().Next(100, 500),
                        PlanNum = new Random().Next(10000, 40000),
                        ResponsiblePersonId = new Random().Next(1, Init_Users().Count)
                    };
                    //cabinet.Num = new Random().Next(10, 50) + (cabinet.Group * 100);
                    cabinet.Num = i == 1 ? 216942 : GenerateNum(cabinet.Group * 100);

                    cabinets.Add(cabinet);
                }

                return cabinets;

                int GenerateNum(int sum)
                {
                    while (true)
                    {
                        var num = new Random().Next(10, 50) + sum;

                        var numIsExist = cabinets.Where(c => c.Num == num).Any();

                        if (!numIsExist)
                            return num;
                    }
                }
            }

            static List<EquipmentType> Init_EquipmentType() 
            {
                List<EquipmentType> equipmentTypes = InitData<EquipmentType>("EquipmentTypeData");    

                return equipmentTypes;
            }
        
            static List<Equipment> Init_Equipments() 
            {
                List<Equipment> equipments = InitData<Equipment>("EquipmentsData");

                return equipments;
            }

            static List<CabinetEquipment> Init_CabEquipments() 
            {
                List<CabinetEquipment> cabEquipments = InitData<CabinetEquipment>("CabinetEquipments");

                return cabEquipments;
            }

            static List<RequestType> Init_RequestTypes() 
            {
                List<RequestType> requestTypes = InitData<RequestType>("RequestTypesData");

                return requestTypes;
            }

            static List<RequestStatus> Init_RequestsStatus() 
            {
                List<RequestStatus> requestStatuses = InitData<RequestStatus>("RequestStatusesData");

                return requestStatuses;
            }

            static List<T> InitData<T>(string fileName) 
            {
                List<T> datas = [];

                string path = Path.Combine("Other", "InitData", $"{fileName}.json");
                var fileStream = File.ReadAllText(path);

                if (fileStream != null)
                {
                    datas = JsonSerializer.Deserialize<List<T>>(fileStream) ?? [];
                }

                return datas;
            }

        }
    }
}
