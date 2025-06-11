using System;

namespace RPN_Calculator
{
    // Tüm operatörlerin türediği soyut sınıf.
    // Her operatör a ve b operandlarını alıp double döner.
    internal abstract class Operator
    {
        public abstract double Execute(double a, double b);
    }
}
