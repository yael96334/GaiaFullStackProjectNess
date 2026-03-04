using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationProject.DTO
{
    public class CalculationHistoryDTO
    {
        public string FieldA { get; set; }
        public string FieldB { get; set; }
        public string Result { get; set; }
        public string Symbol { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
