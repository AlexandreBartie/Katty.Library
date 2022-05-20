using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class TestUnitAnalyze : TestUnitMatrix
    {
        public string GetCompare(string prmResult, string prmExpected) => GetCompare(prmResult, prmExpected, prmExt: false);
        public string GetCompare(string prmResult, string prmExpected, bool prmExt) => GetCompare(new TestLines(prmResult), new TestLines(prmExpected), prmExt);
        public string GetCompare(TestLines prmResult, TestLines prmExpected, bool prmExt)
        {
            string result = prmResult.txt; string expected = prmExpected.output(prmExt);

            string format = "{4}Result:  <{1}>{4}{0}{4}Expected:<{3}>{4}{2}{4}Differences:{4}{5}";

            return string.Format(format, result, GetCompareLines(prmResult), expected, GetCompareLines(prmExpected), Environment.NewLine, GetAnalyses(prmResult, prmExpected));
        }

        private string GetCompareLines(TestLines prmTexto)
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

    public class TestUnitMatrix
    {
        public string GetAnalyses(TestLines prmGerado, TestLines prmEsperado)
        {
            myMemo memo = new myMemo(); string txt;

            for (int cont = 1; cont <= myInt.GetMaior(prmGerado.Count, prmEsperado.Count); cont++)
            {

                txt = GetDifferences(prmGerado.GetLine(cont), prmEsperado.GetLine(cont));

                if (myString.IsFull(txt))
                    memo.Add(String.Format("{0:D3} {1}", cont, txt));

            }
            return memo.memo;
        }

        private string GetDifferences(string prmGerado, string prmEsperado)
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
