using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{

    public static class myRandom
    {
        public static string Next(string prmFormat)
        {

            string prefixo = ""; string parametro; int tamanho; BlocoColchetes Bloco;

            Bloco = new BlocoColchetes();

            if (Bloco.TemParametro(prmFormat))
            {
                prefixo = Bloco.GetPrefixo(prmFormat);
                parametro = Bloco.GetParametro(prmFormat);
            }
            else
                parametro = prmFormat;

            tamanho = myInt.GetNumero(parametro, prmPadrao: 0);

            return prefixo + GetRandom(tamanho);
        }

        private static string GetRandom(int prmTamanho)
        {
            string ret = ""; int number; var rand = new Random();

            for (int cont = 0; cont < prmTamanho; cont++)
            {
                number = rand.Next(0, 9); ret += number.ToString();
            }
            return (ret);
        }

    }

    public static class myRandomDate
    {
        public static string Next() => Next(prmFormat: "");
        public static string Next(string prmFormat) => Next(prmDate: DateTime.Now, prmFormat);
        public static string Next(DateTime prmDate, string prmFormat)
        {

            string prefixo = ""; string parametro; int tamanho; BlocoColchetes Bloco;

            Bloco = new BlocoColchetes();

            if (Bloco.TemParametro(prmFormat))
            {
                prefixo = Bloco.GetPrefixo(prmFormat);
                parametro = Bloco.GetParametro(prmFormat);
            }
            else
                parametro = prmFormat;

            tamanho = myInt.GetNumero(parametro, prmPadrao: 0);

            return prefixo + prmDate.ToString(GetFormat(tamanho));
        }

        private static string GetFormat(int prmTamanho)
        {
            string formato = "fffssmmhhddMMyyyy";

            if (prmTamanho != 0)
                return myString.GetFirst(formato, prmTamanho);

            return (formato);
        }

    }
}
