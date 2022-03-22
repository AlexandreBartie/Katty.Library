using System;
using System.Collections.Generic;
using System.Text;

namespace Dooggy.LIBRARY
{
    public class AnaliseUTC : MatrizUTC
    {
        public string GetCompare(string prmGerado, string prmEsperado) => GetCompare(new LinesUTC(prmGerado), new LinesUTC(prmEsperado));
        public string GetCompare(LinesUTC prmGerado, LinesUTC prmEsperado)
        {
            string gerado = prmGerado.txt; string esperado = prmEsperado.txt;

            return string.Format("{4}Gerado:  <{1}>{4}{0}{4}Esperado:<{3}>{4}{2}{4}Sobreposição:{4}{5}", gerado, GetCompareLines(prmGerado), esperado, GetCompareLines(prmEsperado), Environment.NewLine, GetSobreposicao(prmGerado, prmEsperado));
        }

        private string GetCompareLines(LinesUTC prmTexto)
        {

            try
            {
                if (prmTexto.IsFull)
                {
                    string txt = string.Format("[{0}]", prmTexto.txt.Length);

                    foreach (string linha in prmTexto)
                        txt += string.Format(":{0}", linha.Length);

                    return txt;
                }
            }
            catch (Exception e)
            { return (string.Format("{0} -err: {1}", prmTexto, e.Message)); }

            return ("");
        }
   }

    public class MatrizUTC
    {
        public string GetSobreposicao(LinesUTC prmGerado, LinesUTC prmEsperado)
        {
            xMemo memo = new xMemo(); string txt;

            for (int cont = 1; cont <= myInt.GetMaior(prmGerado.Count, prmEsperado.Count); cont++)
            {

                txt = GetDiferencas(prmGerado.GetLine(cont), prmEsperado.GetLine(cont));

                if (myString.IsFull(txt))
                    memo.Add(String.Format("{0:D3} {1}", cont, txt));

            }
            return memo.memo;
        }

        private string GetDiferencas(string prmGerado, string prmEsperado)
        {
            char gerado; char esperado; string txt = "";

            if (prmGerado != prmEsperado)
            {
                for (int cont = 1; cont <= myInt.GetMaior(prmGerado.Length, prmEsperado.Length); cont++)
                {
                    gerado = myString.GetChar(prmGerado, cont, prmPadrao: '#');

                    esperado = myString.GetChar(prmEsperado, cont, prmPadrao: '#');

                    if (gerado == esperado)
                        txt += ".";
                    else
                        txt += esperado;
                }
            }
            return txt;
        }

    }

}
