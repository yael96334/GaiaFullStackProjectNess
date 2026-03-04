using CalculationProject.DTO;
using CalculationProject.Models;
using CalculationProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace CalculationProject.Services
{
    
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculationRepository _repository;
        private readonly OperationEngineService _engine;


        public CalculatorService(ICalculationRepository repository, OperationEngineService engine)
        {
            _repository = repository;
            _engine = engine;   
        }

        public async Task<CalculationResponse> CalculateAsync(CalculationRequest request)
        {
            object result;

            var opDef = _engine.GetOperations().FirstOrDefault(o => o.Code == request.OperationType);
            if (opDef == null)
                throw new Exception("Operation not found");

            bool isArithmetic = _engine.IsArithmeticOperation(request.OperationType);

            if (isArithmetic)
            {
                if (!double.TryParse(request.FieldA, out double a))
                    result="Field is not a valid number";
              else  if (!double.TryParse(request.FieldB, out double b))
                    result="Field is not a valid number";
                else
                result = _engine.ExecuteArithmetic(request.OperationType, a, b);
            }
            else
            {
                result = _engine.ExecuteString(request.OperationType, request.FieldA, request.FieldB);
            }
           

            var history = new CalculationHistory
            {
                FieldA = request.FieldA.ToString(),
                FieldB = request.FieldB.ToString(),
                Symbol = opDef.Symbol,
                OperationCode = request.OperationType.ToString(),
                Result = result.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            int lastId = await _repository.AddHistoryAsync(history);

            return new CalculationResponse
            {
                Result = result.ToString(),
                LastId = lastId
            };
        }

        public async Task<OperationHistoryResponseDTO> GetHistoryAsync(int id)
        {
            var lastThree = await _repository.GetLastThreeAsync(id);
            var monthlyCount = await _repository.GetMonthlyCountAsync(id);

            return new OperationHistoryResponseDTO
            {
                LastThree = lastThree,
                MonthlyCount = monthlyCount
            };
        }
        public Task<List<OperationDefinition>> GetOperationsAsync()
        {
            return Task.FromResult(_engine.GetOperations());

        }
    }
}
