using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationProject.DTO
{
    public class OperationHistoryResponseDTO
    {
        public List<CalculationHistoryDTO> LastThree { get; set; }
        public int MonthlyCount { get; set; }
    }
}
