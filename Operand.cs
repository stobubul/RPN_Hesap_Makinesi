using System;

namespace RPN_Calculator
{
    // Sayıları temsil eden sınıf.
    // Stack'e push edilen her sayı bir Operand nesnesidir.
    internal class Operand
    {
        public double Value { get; }

        public Operand(double value)
        {
            Value = value;
        }
    }
}
