﻿using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.EquipmentService
{
    public class EquipmentService(DB_Context context) : IEquipmentService
    {
        #region Добавление оборудования
        public async Task<Equipment?> AddNewEquipment(NewEquipmentDTO newEquipment)
        {
            var equipmentIsExist = await context.Equipments
                .AnyAsync(e => e.Name == newEquipment.Name);

            if (equipmentIsExist)
                return null;

            var addedEquipment = await context.Equipments.AddAsync(new()
            {
                Name = newEquipment.Name,
                Count = newEquipment.Count,
                Description = newEquipment.Description,
                TypeId = newEquipment.TypeId
            });
            await context.SaveChangesAsync();

            return addedEquipment.Entity;
        }

        public async Task<int?> AddNewEquipments(IEnumerable<NewEquipmentDTO> newEquipments)
        {
            if (newEquipments.ToList().Count == 0)
                return null;

            int i = 0;

            //проверка: существует ли такой тип оборудования
            foreach (var equip in newEquipments)
            {
                var typeIsExist = await context.EquipmentTypes.AnyAsync(et => et.Id == equip.TypeId);

                if (!typeIsExist)
                    return null;

                i++;
            }

            await context.Equipments.AddRangeAsync(newEquipments.Select(eq => new Equipment()
            {
                Name = eq.Name,
                Description = eq.Description,
                Count = eq.Count,
                TypeId = eq.TypeId
            }));
            await context.SaveChangesAsync();

            return i;
        }

        public async Task<bool> AddNewEquipmentType(string typeName)
        {
            var equipTypeIsExist = await context.EquipmentTypes.AnyAsync(e => e.Name == typeName);

            if (equipTypeIsExist)
                return false;

            await context.EquipmentTypes.AddAsync(new EquipmentType()
            {
                Name = typeName
            });
            await context.SaveChangesAsync();

            return true;
        }
        #endregion

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public async Task<bool> DeleteEquipment(int id)
        {
            var equipmentOnDelete = await context.Equipments
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipmentOnDelete == null)
                return false;

            return true;
        }

        public async Task<int?> DeleteEquipments(IEnumerable<EquipmentDTO> deletedEquipments)
        {
            if (deletedEquipments.ToList().Count == 0)
                return null;

            int i = 0;

            foreach (var equip in deletedEquipments)
            {
                var typeIsExist = await context.EquipmentTypes.AnyAsync(et => et.Id == equip.EquipmentType.Id);

                if (!typeIsExist)
                    return null;

                i++;
            }

            context.Equipments.RemoveRange(deletedEquipments.Select(eq => new Equipment()
            {
                Name = eq.Name,
                Description = eq.Description,
                Count = eq.Count,
                TypeId = eq.EquipmentType.Id
            }));
            await context.SaveChangesAsync();

            return i;
        }

        public async Task<Equipment?> GetEquipment(int id)
        {
            return await context.Equipments.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Equipment>> GetEquipments()
        {
            return await context.Equipments.ToListAsync();
        }

        public async Task<EquipmentType?> GetEquipmentType(int equipmentTypeId)
        {
            return await context.EquipmentTypes.FirstOrDefaultAsync(et => et.Id == equipmentTypeId);
        }

        public async Task<IEnumerable<EquipmentType>> GetEquipmentTypes()
        {
            return await context.EquipmentTypes.ToListAsync();
        }
    }
}