﻿using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Other;

namespace WebAPI.Services
{
    public class CabinetService(DB_Context context) : ICabinetService
    {
        private readonly DB_Context _context = context;

        public async Task<Cabinet?> AddCabinet(NewCabinetDTO newCabinet)
        {
            var cabinets = await _context.Cabinets.ToListAsync();

            if (EqualEntity.CabinetIsExist(cabinets, newCabinet))
                return null;

            var addedCabinet = new Cabinet()
            {
                Num = newCabinet.Num,
                PlanNum = newCabinet.PlanNum,
                Floor = newCabinet.Floor,
                Group = newCabinet.Group,
                Height = newCabinet.Height,
                Length = newCabinet.Length,
                Width = newCabinet.Width,
                ResponsiblePersonId = newCabinet.ResponsiblePersonId
            };

            await _context.AddAsync(addedCabinet);
            await _context.SaveChangesAsync();

            return addedCabinet;
        }

        public async Task<int?> AddEquipmentsToCabinet(int cabId, IEnumerable<AddEquipToCabDTO> equipments)
        {
            var cabinet = await _context.Cabinets
                .Where(c => c.Id == cabId)
                .FirstOrDefaultAsync();

            int addedRows = 0;
            List<CabEquipment> addedEquipment = [];

            if (cabinet != null) 
            {
                //Проверка на существование оборудования
                foreach (var equipment in equipments) 
                {
                    var equip = await _context.Equipments
                        .Where(e => e.Id == equipment.EquipmentId)
                        .FirstOrDefaultAsync();

                    if (equip != null && equip.Count > 0) 
                    {
                        int totalCount = 0;
                        switch (equip.Count - equipment.AddedEquipmentCount) 
                        {
                            case <= 0:
                                totalCount = equip.Count;
                                equip.Count = 0;
                                break;
                            case > 0:
                                totalCount = equipment.AddedEquipmentCount;
                                equip.Count -= equipment.AddedEquipmentCount;
                                break;
                        }

                        addedEquipment.Add(new CabEquipment() 
                        {
                            Id = equip.Id,
                            CabId = cabinet.Id,
                            EquipId = equip.Id,
                            Count = totalCount
                        });

                        _context.Equipments.Update(equip);
                        await _context.SaveChangesAsync();

                        addedRows++;
                    }
                }

                //Добавление оборудование к кабинету
                foreach (var equipment in addedEquipment) 
                {
                    var equipInCab = await _context.CabEquipments
                        .Where(eq => eq.EquipId == equipment.Id)
                        .FirstOrDefaultAsync();

                    if (equipInCab == null)
                    {
                        //Если оборудования нет в кабинет, то оно добавляется
                        await _context.CabEquipments.AddAsync(new CabEquipment()
                        {
                            CabId = cabinet.Id,
                            EquipId = equipment.Id,
                            Count = equipment.Count
                        });
                    }
                    else
                    {
                        //Если оборудование есть в кабинете, то прибавляется количество добавляемого
                        equipInCab.Count += equipment.Count;

                        _context.CabEquipments.Update(equipInCab);
                    }
                    await _context.SaveChangesAsync();
                }

                //await _context.CabEquipments.AddRangeAsync(addedEquipment);
                await _context.SaveChangesAsync();
            }

            return addedRows;
        }

        public async Task<bool> DeleteCabinet(int id)
        {
            var cabinet = await _context.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return false;

            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cabinet?> GetCabinet(int id)
        {
            var cabinet = await _context.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return null;

            return cabinet;
        }

        public async Task<IEnumerable<EquipmentDTO>> GetCabinetEquipments(int cabId)
        {
            var cabinetsEquipments = await _context.CabEquipments
                .Where(ce => ce.CabId == cabId)
                .ToListAsync();

            List<EquipmentDTO> equipments = [];
            foreach (var cabEquip in cabinetsEquipments) 
            {
                var foundedEquipment = await _context.Equipments
                    .Where(e => e.Id == cabEquip.EquipId)
                    .FirstOrDefaultAsync();

                if(foundedEquipment is Equipment equipment)
                equipments.Add(new EquipmentDTO() 
                {
                   Name = equipment.Name,
                   Count = equipment.Count,
                   Description = equipment.Description,
                   TypeId = equipment.TypeId
                });
            }

            return equipments;
        }

        public async Task<IEnumerable<CabinetDTO>> GetCabinets()
        {
            var cabs = await _context.Cabinets.ToListAsync();

            List<CabinetDTO> result = [];

            foreach (var cab in cabs) 
            {
                var convCab = new CabinetDTO()
                {
                    Id = cab.Id,
                    Num = cab.Num,
                    Floor = cab.Floor,
                    Group = cab.Group,
                    Length = cab.Length,
                    Height = cab.Height,
                    Width = cab.Width,
                    PlanNum = cab.PlanNum
                };

                var resposiblePerson = await _context.Users
                    .Where(u => u.Id == cab.ResponsiblePersonId)
                    .Select(u => new UserDTO() 
                    {
                        Id = u.Id,
                        Birthday = u.Birthday,
                        Name = u.Name,
                        Patronymic = u.Patronymic,
                        PermissionName = u.Patronymic ?? string.Empty,
                        Surname = u.Surname
                    })
                    .FirstOrDefaultAsync();

                if (resposiblePerson is UserDTO _resposiblePerson)
                    convCab.ResponsiblePerson = _resposiblePerson;

                result.Add(convCab);
            }

            return result;
        }
    }
}