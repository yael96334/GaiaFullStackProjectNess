using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationProject.Models
{
    public class OperationDefinition
    {
        public string Code { get; set; }
        public string Expression { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
    }
    public class OperationConfig
    {
        public List<OperationDefinition> Arithmetic { get; set; } = new List<OperationDefinition>();

        public List<OperationDefinition> String { get; set; } = new List<OperationDefinition>();
    }
}
