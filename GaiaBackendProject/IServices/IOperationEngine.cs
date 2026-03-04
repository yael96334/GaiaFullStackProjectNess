using CalculationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationProject.Interfaces
{
    
        public interface IOperationEngine
        {
            string ExecuteString(string code, string a, string b = "");
            string ExecuteArithmetic(string code, double a, double b);
            List<OperationDefinition> GetOperations();
            bool IsArithmeticOperation(string code);
        
    }
}
