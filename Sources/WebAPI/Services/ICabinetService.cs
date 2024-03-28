﻿using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services
{
    public interface ICabinetService
    {
        Task<IEnumerable<CabinetDTO>> GetCabinets();
        Task<Cabinet?> GetCabinet(int id);
        Task<Cabinet?> AddCabinet(NewCabinetDTO newCabinet);
        Task<bool> DeleteCabinet(int id);
    }
}
