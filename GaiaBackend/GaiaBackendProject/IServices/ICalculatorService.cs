using CalculationProject.DTO;
using CalculationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace CalculationProject.Services
{
    public interface ICalculatorService
    {
         Task<List<OperationDefinition>> GetOperationsAsync();
        Task<CalculationResponse> CalculateAsync(CalculationRequest request);
        Task<OperationHistoryResponseDTO> GetHistoryAsync(int id);
    }
}
