using CalculationProject.DTO;
using CalculationProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationProject.Repositories
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly AppDbContext _context;

        public CalculationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddHistoryAsync(CalculationHistory history)
        {
            await _context.CalculationHistory.AddAsync(history);
            await _context.SaveChangesAsync();
            return history.Id;
        }

        public  Task<List<CalculationHistoryDTO>> GetLastThreeAsync(int id)
        {
            var currentCalculate = _context.CalculationHistory.FirstOrDefault(calc=>calc.Id==id);
            return  (from calculationHistory in _context.CalculationHistory
                          where calculationHistory.OperationCode == currentCalculate.OperationCode && calculationHistory.Id !=id
                          orderby calculationHistory.CreatedAt descending
                          select new CalculationHistoryDTO
                          {
                              FieldA = calculationHistory.FieldA,
                              FieldB = calculationHistory.FieldB,
                              Result = calculationHistory.Result,
                              Symbol = calculationHistory.Symbol,
                              CreatedAt = calculationHistory.CreatedAt
                          })
                          .Take(3)
                          .ToListAsync();
        }

        public  Task<int> GetMonthlyCountAsync(int id)
        {
            string OperationCode = _context.CalculationHistory.Where(calc => calc.Id == id).Select(calc=>calc.OperationCode).FirstOrDefault();
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return _context.CalculationHistory
                .CountAsync(h => h.OperationCode == OperationCode &&
                                 h.CreatedAt >= startOfMonth);
        }

       
    }
}
