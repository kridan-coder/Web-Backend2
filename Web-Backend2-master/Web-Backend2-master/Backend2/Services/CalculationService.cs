using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend2.Services
{
    public class CalculationService : ICalculationService
    {
        public Int64 Addition(Int64 num1, Int64 num2)
        {
            return num1 + num2;
        }

        public Int64 Division(Int64 num1, Int64 num2)
        {
            return num1 / num2;
        }

        public Int64 Multiplication(Int64 num1, Int64 num2)
        {
            return num1 * num2;
        }

        public Int64 Substraction(Int64 num1, Int64 num2)
        {
            return num1 - num2;
        }
    }
}
