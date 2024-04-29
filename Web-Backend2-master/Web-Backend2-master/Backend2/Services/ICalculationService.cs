using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend2.Services
{
    public interface ICalculationService
    {
        Int64 Addition(Int64 num1, Int64 num2);

        Int64 Substraction(Int64 num1, Int64 num2);

        Int64 Division(Int64 num1, Int64 num2);

        Int64 Multiplication(Int64 num1, Int64 num2);
    }
}
