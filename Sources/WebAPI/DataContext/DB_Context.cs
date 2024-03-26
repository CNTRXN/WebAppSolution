using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext.Models;
using ConfigurationManager = System.Configuration;

namespace WebAPI.DataContext
{
    public class DB_Context(DbContextOptions<DB_Context> options) 
        : DbContext(options)
    {
        public DbSet<CabEquipment> CabEquipments { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<CabPhoto> CabPhotos { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<AccountStat> AccountStats { get; set; }



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
                });

            static List<Permission> Init_Permissions() 
            {
                List<Permission> permissions = [
                    new Permission()
                    {
                        Name = "Пользователь"
                    },
                    new Permission()
                    {
                        Name = "Мастер"
                    },
                    new Permission()
                    {
                        Name = "Администратор"
                    }
                ];

                int i = 1;
                foreach (var post in permissions)
                {
                    post.Id = i;
                    i++;
                }

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
                        Patronymic = (new Random().Next(0, 2) == 0) ?  null : $"Patronymic{i}",
                        Birthday = new DateTime(new Random().Next(1990, 2005), new Random().Next(1, 12), new Random().Next(1, 28), 0, 0, 0, DateTimeKind.Utc),
                        PermissionId = new Random().Next(1, Init_Permissions().Count)
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
                    cabinet.Num = new Random().Next(10, 50) + (cabinet.Group * 100);


                    cabinets.Add(cabinet);
                }

                return cabinets;
            }
        }
    }
}
