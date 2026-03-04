using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CalculationProject.Models
{
    public class CalculationHistory
    {
        [Key]
        public int Id { get; set; }
        public string FieldA { get; set; }
        public string FieldB { get; set; }
        public string Symbol { get; set; }
        public string OperationCode { get; set; } 
        public string Result { get; set; }       
        public DateTime CreatedAt { get; set; }
    }
}
