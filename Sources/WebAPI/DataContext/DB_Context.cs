using Microsoft.EntityFrameworkCore;
using System;
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
            #region Права доступа
            modelBuilder
                .Entity<Permission>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder
                .Entity<Permission>()
                .HasData(Init_Permissions());
            #endregion

            #region Пользователи
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasData(Init_Users());
            #endregion

            #region Кабинет
            modelBuilder
                .Entity<Cabinet>()
                .HasIndex(c => c.Num)
                .IsUnique();

            modelBuilder
                .Entity<Cabinet>()
                .HasIndex(c => c.PlanNum)
                .IsUnique();

            modelBuilder
                .Entity<Cabinet>()
                .HasIndex(c => c.ResponsiblePersonId)
                .IsUnique();

            modelBuilder
                .Entity<Cabinet>()
                .HasData(Init_Cabs());
            #endregion

            #region Типы оборудования
            modelBuilder
                .Entity<EquipmentType>()
                .HasIndex(et => et.Name)
                .IsUnique();

            modelBuilder
                .Entity<EquipmentType>()
                .HasData(Init_EquipmentType());
            #endregion

            #region Оборудование
            modelBuilder
                .Entity<Equipment>()
                .HasIndex(e => e.InventoryNumber)
                .IsUnique();

            modelBuilder
                .Entity<Equipment>()
                .HasData(Init_Equipments());
            #endregion

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

            #region Тип заявки
            modelBuilder
                .Entity<RequestType>()
                .HasIndex(r => r.TypeName)
                .IsUnique();

            modelBuilder
                .Entity<RequestType>()
                .HasData(Init_RequestTypes());
            #endregion

            #region Статус заявки
            modelBuilder
                .Entity<RequestStatus>()
                .HasIndex(rs => rs.StatusName)
                .IsUnique();

            modelBuilder
                .Entity<RequestStatus>()
                .HasData(Init_RequestsStatus());
            #endregion

            List<Permission> Init_Permissions()
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

            List<Cabinet> Init_Cabs()
            {
                List<Cabinet> cabinets = [];

                for (int i = 1; i < 20; i++)
                {
                    var cabinet = new Cabinet
                    {
                        Id = i,
                        Floor = new Random().Next(0, 3),
                        //Group = new Random().Next(1000, 4000),
                        Height = new Random().Next(100, 500),
                        Length = new Random().Next(100, 500),
                        Width = new Random().Next(100, 500),
                        PlanNum = new Random().Next(10000, 40000),
                        //ResponsiblePersonId = (i < Init_Users().Count) ? i : null,
                        ResponsiblePersonId = ResponsiblePerson(i),
                        //cabinet.Num = new Random().Next(10, 50) + (cabinet.Group * 100);
                        Num = i == 1 ? 216942 : GenerateNum(new Random().Next(1000, 4000) * 100)
                    };

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



                int? ResponsiblePerson(int currentIteration)
                {
                    while (true)
                    {
                        if (currentIteration >= Init_Users().Count)
                            break;

                        var person = new Random().Next(1, Init_Users().Count);

                        var numIsExist = cabinets.Where(c => c.ResponsiblePersonId == person).Any();

                        if (!numIsExist)
                            return person;
                    }

                    return null;
                }
            }

            List<EquipmentType> Init_EquipmentType() 
            {
                List<EquipmentType> equipmentTypes = InitData<EquipmentType>("EquipmentTypeData");    

                return equipmentTypes;
            }
        
            List<Equipment> Init_Equipments() 
            {
                List<Equipment> equipments = InitData<Equipment>("EquipmentsData");

                return equipments;
            }

            List<CabinetEquipment> Init_CabEquipments() 
            {
                List<CabinetEquipment> cabEquipments = InitData<CabinetEquipment>("CabinetEquipments");

                return cabEquipments;
            }

            List<RequestType> Init_RequestTypes() 
            {
                List<RequestType> requestTypes = InitData<RequestType>("RequestTypesData");

                return requestTypes;
            }

            List<RequestStatus> Init_RequestsStatus() 
            {
                List<RequestStatus> requestStatuses = InitData<RequestStatus>("RequestStatusesData");

                return requestStatuses;
            }

            List<T> InitData<T>(string fileName) 
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
