using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN_Calculator
{
    internal class Divider : Operator
    {
        // '/' operatörünü temsil eden sınıf.
        public override double Execute(double a, double b)
        {
            // Burada sıfıra bölünme kuralı uygulanmıyor çünkü Calculator sınıfında bu kontrol mevcut.

            return a / b;
        }
    }
}
