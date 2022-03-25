using System;
using System.Collections.Generic;
using System.Text;

namespace Dooggy.LIBRARY
{
    public static class myBool
    {

        public static string IIf(bool prmCondicao, string prmTrue) => IIf(prmCondicao, prmTrue, prmFalse: "");
        public static string IIf(bool prmCondicao, string prmTrue, string prmFalse)
        {

            if (prmCondicao)
                return (prmTrue);

            return (prmFalse);

        }

        public static string GetYesNo(bool prmOpcao) { if (prmOpcao) return "SIM"; return "NAO"; }

    }
}
