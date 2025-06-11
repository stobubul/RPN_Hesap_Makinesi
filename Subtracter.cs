using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNP_Calculator
{
    internal class Subtracter : Operator
    {
        // '-' operatörünü temsil eden sınıf.
        public override double Execute(double a, double b)
        {
            return a - b;
        }
    }
}
