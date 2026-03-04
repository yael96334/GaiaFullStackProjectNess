using CalculationProject.DTO;
using CalculationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationProject.Repositories
{
    public interface ICalculationRepository
    {
        Task<int> AddHistoryAsync(CalculationHistory history);
        Task<List<CalculationHistoryDTO>> GetLastThreeAsync(int id);
        Task<int> GetMonthlyCountAsync(int id);
    }
}
