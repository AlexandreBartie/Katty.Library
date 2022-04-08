using System;
using System.Collections.Generic;
using System.Text;

namespace Dooggy.LIBRARY
{
    public static class myDouble
    {
        public static double GetAverage(double prmValor, int prmQtde)
        {
            if (myInt.IsNoZero(prmQtde))
                return prmValor / prmQtde;

            return 0;
        }
        public static double GetBigger(double prmValorA, double prmValorB)
        {
            if (prmValorA > prmValorB)
                return (prmValorA);

            return (prmValorB);
        }

    }
}
