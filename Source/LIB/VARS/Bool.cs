using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
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

        public static bool GetYesNo(string prmOpcao) => (myString.IsMatch(prmOpcao, "Yes"));
        public static string GetYesNo(bool prmOpcao) { if (prmOpcao) return "Yes"; return "No"; }

    }
}
