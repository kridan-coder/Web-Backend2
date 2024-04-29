using System;
using System.ComponentModel.DataAnnotations;

namespace Backend2.Models
{
    public class CalculatorModel
    {
        public String Result { get; set; }
        public String Option { get; set; }

        [Required(ErrorMessage = "First operand is required")]
        public String Operand1 { get; set; }
        [Required(ErrorMessage = "Second operand is required")]
        public String Operand2 { get; set; }
    }
}
