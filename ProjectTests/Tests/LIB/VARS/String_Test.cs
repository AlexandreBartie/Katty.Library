using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katty.Tools.Test.LIB.VARS
{

    [TestClass()]
    public class myStringsByGetFirst_Test : myStrings_Test
    {

        [TestMethod()]
        public void TST_GetFirst_010_Padrao()
        {

            input = "Alexandre Bartie";
            output = "A";

            // act & assert
            ActionGetFirst();

        }

        [TestMethod()]
        public void TST_GetFirst_020_TextoCaracterUnico()
        {

            input = "X";
            output = "X";

            // act & assert
            ActionGetFirst();

        }

        [TestMethod()]
        public void TST_GetFirst_030_TextoComBrancos()
        {

            input = " X ";
            output = " ";

            // act & assert
            ActionGetFirst();

        }

        [TestMethod()]
        public void TST_GetFirst_040_TextoVazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetFirst();

        }

        [TestMethod()]
        public void TST_GetFirst_050_TextoNulo()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetFirst();

        }

    }

    [TestClass()]
    public class myStringsByGetFirstExt_Test : myStrings_Test
    {

        [TestMethod()]
        public void TST_GetFirstExt_010_Padrao()
        {

            input = "Alexandre myBrick Bartie";
            output = "Alexan";

            // act & assert
            ActionGetFirstExt();

        }

        [TestMethod()]
        public void TST_GetFirstExt_020_TextoCaracterUnico()
        {

            input = "X";
            output = "X";

            // act & assert
            ActionGetFirstExt();

        }

        [TestMethod()]
        public void TST_GetFirstExt_030_TextoTamanhoCurto()
        {

            input = "abcde";
            output = "abcde";

            // act & assert
            ActionGetFirstExt();

        }

        [TestMethod()]
        public void TST_GetFirstExt_040_TextoTamanhoExato()
        {

            input = "abcdef";
            output = "abcdef";

            // act & assert
            ActionGetFirstExt();

        }

        [TestMethod()]
        public void TST_GetFirstExt_050_TextoTamanhoLongo()
        {

            input = "abcdefg";
            output = "abcdef";

            // act & assert
            ActionGetFirstExt();

        }

        [TestMethod()]
        public void TST_GetFirstExt_060_TextoComBrancos()
        {

            input = " X ";
            output = " X ";

            // act & assert
            ActionGetFirstExt();

        }
        [TestMethod()]
        public void TST_GetFirstExt_070_TextoVazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetFirstExt();

        }
        [TestMethod()]
        public void TST_GetFirstExt_080_TextoNulo()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetFirstExt();

        }

        [TestMethod()]
        public void TST_GetFirstExt_090_TamanhoZero()
        {

            input = "abcdefg";
            output = "";

            // act & assert
            ActionGetFirstExt(prmTamanho: 0);

        }

        [TestMethod()]
        public void TST_GetFirstExt_100_TamanhoNegativo()
        {

            input = "abcdefg";
            output = "g";

            // act & assert
            ActionGetFirstExt(prmTamanho: -1);

        }
        [TestMethod()]
        public void TST_GetFirstExt_110_Delimitador()
        {

            input = "abc|defg";
            output = "abc";

            // act & assert
            ActionGetFirstExt(prmDelimitador: "|");

        }
        [TestMethod()]
        public void TST_GetFirstExt_120_DelimitadorPrimeiroCaracter()
        {

            input = "|abcdefg";
            output = "";

            // act & assert
            ActionGetFirstExt(prmDelimitador: "|");

        }
        [TestMethod()]
        public void TST_GetFirstExt_130_DelimitadorUltimoCaracter()
        {

            input = "abcdefg|";
            output = "abcdefg";

            // act & assert
            ActionGetFirstExt(prmDelimitador: "|");

        }
        [TestMethod()]
        public void TST_GetFirstExt_140_DelimitadorNaoEncontrado()
        {

            input = "abcdefg";
            output = "abcdefg";

            // act & assert
            ActionGetFirstExt(prmDelimitador: "|");

        }
    }

    [TestClass()]
    public class myStringsByGetLast_Test : myStrings_Test
    {

        [TestMethod()]
        public void TST_GetLast_010_Padrao()
        {

            input = "Alexandre Bartie";
            output = "e";

            // act & assert
            ActionGetLast();

        }

        [TestMethod()]
        public void TST_GetLast_020_TextoCaracterUnico()
        {

            input = "X";
            output = "X";

            // act & assert
            ActionGetLast();

        }

        [TestMethod()]
        public void TST_GetLast_030_TextoComBrancos()
        {

            input = " X ";
            output = " ";

            // act & assert
            ActionGetLast();

        }

        [TestMethod()]
        public void TST_GetLast_040_TextoVazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetLast();

        }

        [TestMethod()]
        public void TST_GetLast_050_TextoNulo()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetLast();

        }
    }

    [TestClass()]
    public class myStringsByGetLastExt_Test : myStrings_Test
    {
        [TestMethod()]
        public void TST_GetLastExt_010_Padrao()
        {

            input = "Alexandre myBrick Bartie";
            output = "Bartie";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_020_TextoCaracterUnico()
        {

            input = "X";
            output = "X";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_030_TextoTamanhoCurto()
        {

            input = "abcde";
            output = "abcde";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_040_TextoTamanhoExato()
        {

            input = "abcdef";
            output = "abcdef";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_050_TextoTamanhoLongo()
        {

            input = "abcdefg";
            output = "bcdefg";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_060_TextoComBrancos()
        {

            input = " X ";
            output = " X ";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_070_TextoVazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_080_TextoNulo()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetLastExt();

        }

        [TestMethod()]
        public void TST_GetLastExt_090_TamanhoZero()
        {

            input = "abcdefg";
            output = "";

            // act & assert
            ActionGetLastExt(prmTamanho: 0);

        }

        [TestMethod()]
        public void TST_GetLastExt_100_TamanhoNegativo()
        {

            input = "abcdefg";
            output = "bcdefg";

            // act & assert
            ActionGetLastExt(prmTamanho: -1);

        }

        [TestMethod()]
        public void TST_GetLastExt_110_Delimitador()
        {

            input = "abc|defg";
            output = "defg";

            // act & assert
            ActionGetLastExt(prmDelimitador: "|");

        }
        [TestMethod()]
        public void TST_GetLastExt_120_DelimitadorPrimeiroCaracter()
        {

            input = "|abcdefg";
            output = "abcdefg";

            // act & assert
            ActionGetLastExt(prmDelimitador: "|");

        }
        [TestMethod()]
        public void TST_GetLastExt_130_DelimitadorUltimoCaracter()
        {

            input = "abcdefg|";
            output = "";

            // act & assert
            ActionGetLastExt(prmDelimitador: "|");

        }
        [TestMethod()]
        public void TST_GetLastExt_140_DelimitadorNaoEncontrado()
        {

            input = "abcdefg";
            output = "";

            // act & assert
            ActionGetLastExt(prmDelimitador: "|");

        }
    }
    [TestClass()]
    public class myStringsByGetFind_Test : myStrings_Test
    {
        [TestMethod()]
        public void TST_GetFind_010_BuscaPadrao()
        {

            input = "Alexandre Bartie";
            output = "sim";

            // act & assert
            ActionGetFind(prmBuscar: "xan");

        }

        [TestMethod()]
        public void TST_GetFind_020_BuscaPorDiferencaCaixa()
        {

            input = "Alexandre Bartie";
            output = "sim";

            // act & assert
            ActionGetFind(prmBuscar: "alex");

        }

        [TestMethod()]
        public void TST_GetFind_030_BuscaPorMatchParcial()
        {

            input = "Alexandre Bartie";
            output = "nao";

            // act & assert
            ActionGetFind(prmBuscar: "Bartier");

        }
        [TestMethod()]
        public void TST_GetFind_040_BuscaPorVazio()
        {

            input = "Alexandre Bartie";
            output = "nao";

            // act & assert
            ActionGetFind(prmBuscar: "");

        }
        [TestMethod()]
        public void TST_GetFind_050_BuscaPorNull()
        {

            input = "Alexandre Bartie";
            output = "nao";

            // act & assert
            ActionGetFind(prmBuscar: null);

        }
    }
    [TestClass()]
    public class myStringsByGetReverse_Test : myStrings_Test
    {
        [TestMethod()]
        public void TST_GetReverse_010_Padrao()
        {

            input = "Alexandre Bartie";
            output = "eitraB erdnaxelA";

            // act & assert
            ActionGetReverse();

        }

        [TestMethod()]
        public void TST_GetReverse_020_Vazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetReverse();

        }
        [TestMethod()]
        public void TST_GetReverse_030_Null()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetReverse();

        }
    }
    [TestClass()]
    public class myStringsByGetNoBlank_Test : myStrings_Test
    {

        [TestMethod()]
        public void TST_GetNoBlank_010_Padrao()
        {

            input = "Alexandre Bartie";
            output = "AlexandreBartie";

            // act & assert
            ActionGetNoBlank();

        }

        [TestMethod()]
        public void TST_GetNoBlank_020_MultiplosEspacos()
        {

            input = " Alexandre Bartie ";
            output = "AlexandreBartie";

            // act & assert
            ActionGetNoBlank();

        }

        [TestMethod()]
        public void TST_GetNoBlank_030_EspacosDobrados()
        {

            input = "  Alexandre  Bartie  ";
            output = "AlexandreBartie";

            // act & assert
            ActionGetNoBlank();

        }

        [TestMethod()]
        public void TST_GetNoBlank_040_Vazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetNoBlank();

        }
        [TestMethod()]
        public void TST_GetNoBlank_050_Null()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetNoBlank();

        }
    }

    [TestClass()]
    public class myStringsByGetSubstituir_Test : myStrings_Test
    {

        [TestMethod()]
        public void TST010_GetSubstituir_SubstituicaoPadrao()
        {

            input = "Alexandre Silva Bartie";
            output = "Alexandre Jonas Bartie";

            // act & assert
            ActionGetSubstituir(prmVelho: "Silva", prmNovo: "Jonas");

        }

        [TestMethod()]
        public void TST020_GetSubstituir_SubstituicoesMultiplas()
        {

            input = "Alexandre Silva Bartie Silva Soares";
            output = "Alexandre Jonas Bartie Jonas Soares";

            // act & assert
            ActionGetSubstituir(prmVelho: "Silva", prmNovo: "Jonas");

        }

        [TestMethod()]
        public void TST030_GetSubstituir_SubstituicoesEspacos()
        {

            input = "  Alexandre  Bartie  ";
            output = "**Alexandre**Bartie**";

            // act & assert
            ActionGetSubstituir(prmVelho: " ", prmNovo: "*");

        }

        [TestMethod()]
        public void TST040_GetSubstituir_SubstituicaoVazio()
        {

            input = "  Alexandre  Bartie  ";
            output = "AlexandreBartie";

            // act & assert
            ActionGetSubstituir(prmVelho: " ", prmNovo: "");

        }
        [TestMethod()]
        public void TST050_GetSubstituir_InputNull()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetSubstituir(prmVelho: "Silva", prmNovo: "Jonas");

        }
        [TestMethod()]
        public void TST050_GetSubstituir_VelhoNull()
        {

            input = "Alexandre Silva Bartie";
            output = "Alexandre Silva Bartie";

            // act & assert
            ActionGetSubstituir(prmVelho: null, prmNovo: "Jonas");

        }

        [TestMethod()]
        public void TST050_GetSubstituir_NovoNull()
        {

            input = "Alexandre Silva Bartie";
            output = "Alexandre  Bartie";

            // act & assert
            ActionGetSubstituir(prmVelho: "Silva", prmNovo: null);

        }
    }

    [TestClass()]
    public class myStringsByGetRepetir_Test : myStrings_Test
    {

        [TestMethod()]
        public void TST010_GetRepetir_ValorPadrao()
        {

            input = "X";
            output = "XXXXX";

            // act & assert
            ActionGetRepetir(prmVezes: 5);

        }

        [TestMethod()]
        public void TST020_GetRepetir_ValorChar()
        {

            input = "X";
            output = "X";

            // act & assert
            ActionGetRepetir(prmVezes: 1);

        }

        [TestMethod()]
        public void TST030_GetRepetir_ValorZero()
        {

            input = "X";
            output = "";

            // act & assert
            ActionGetRepetir(prmVezes: 0);

        }

        [TestMethod()]
        public void TST040_GetRepetir_ValorNegativo()
        {

            input = "X";
            output = "";

            // act & assert
            ActionGetRepetir(prmVezes: -1);

        }

    }

    [TestClass()]
    public class myStringsByGetMask_Test : myStrings_Test
    {
        [TestMethod()]
        public void TST_GetMask_010_MascaraPadrao()
        {

            mask = "###.###.###-##";

            input = "14029092845";
            output = "140.290.928-45";

            // act & assert
            ActionGetMask();

        }

        [TestMethod()]
        public void TST_GetMask_020_MascaraAjustaTamanho()
        {

            mask = "###.###.###-##";

            input = "4029092845";
            output = "40.290.928-45";

            // act & assert
            ActionGetMask();

        }

        [TestMethod()]
        public void TST_GetMask_030_MascaraCortaDados()
        {

            mask = "###.###.###-##";

            input = "814029092845";
            output = "140.290.928-45";

            // act & assert
            ActionGetMask();

        }

        [TestMethod()]
        public void TST_GetMask_040_MascaraAjustaTamanho_IncluiMarcacao()
        {

            mask = "###.###.###-##";

            input = "029092845";
            output = "0.290.928-45";

            // act & assert
            ActionGetMask();

        }
        [TestMethod()]
        public void TST_GetMask_050_MascaraAjustaTamanho_LimiteMarcacao()
        {

            mask = "###.###.###-##";

            input = "29092845";
            output = "290.928-45";

            // act & assert
            ActionGetMask();

        }

        [TestMethod()]
        public void TST_GetMask_060_MascaraComPrefixo()
        {

            mask = "CPF: ###.###.###-##";

            input = "14029092845";
            output = "CPF: 140.290.928-45";

            // act & assert
            ActionGetMask();

        }

        [TestMethod()]
        public void TST_GetMask_070_MascaraEntreApostrofes()
        {

            mask = @"'###.###.###-##'";

            input = "14029092845";
            output = "'140.290.928-45'";

            // act & assert
            ActionGetMask();

        }

        [TestMethod()]
        public void TST_GetMask_080_MascaraZerosEsquerda()
        {

            mask = "000.000.###-##";

            input = "192845";
            output = "000.001.928-45";

            // act & assert
            ActionGetMask();

        }
        [TestMethod()]
        public void TST_GetMask_090_MascaraZerosEsquerda_LimiteMarcacao()
        {

            mask = "000.000.###-##";

            input = "92845";
            output = "000.000.928-45";

            // act & assert
            ActionGetMask();

        }
    }
    public class myStrings_Test
    {

        public string mask;

        public string input;
        public string output;
        public string result;

        public void ActionGetFirst()
        {

            // assert
            result = myString.GetFirst(input);

            // assert
            ActionGeneric();

        }
        public void ActionGetFirstExt() => ActionGetFirstExt(prmTamanho: 6);

        public void ActionGetFirstExt(int prmTamanho)
        {

            // assert
            result = myString.GetFirst(input, prmTamanho);

            // assert
            ActionGeneric();

        }
        public void ActionGetFirstExt(string prmDelimitador)
        {

            // assert
            result = myString.GetFirst(input, prmDelimitador);

            // assert
            ActionGeneric();

        }
        public void ActionGetLast()
        {

            // assert
            result = myString.GetLast(input);

            // assert
            ActionGeneric();

        }
        public void ActionGetLastExt() => ActionGetLastExt(prmTamanho: 6);
        public void ActionGetLastExt(int prmTamanho)
        {

            // assert
            result = myString.GetLast(input, prmTamanho);

            // assert
            ActionGeneric();

        }
        public void ActionGetLastExt(string prmDelimitador)
        {

            // assert
            result = myString.GetLast(input, prmDelimitador);

            // assert
            ActionGeneric();

        }

        public void ActionGetMask()
        {

            // assert
            result = myFormat.TextToString(input, mask);

            // assert
            ActionGeneric();

        }

        public void ActionGetFind(string prmBuscar)
        {

            // assert
            result = myBool.IIf(myString.GetFind(input, prmBuscar), "sim", "nao");

            // assert
            ActionGeneric();

        }
        public void ActionGetReverse()
        {

            // assert
            result = myString.GetReverse(input);

            // assert
            ActionGeneric();

        }
        public void ActionGetNoBlank()
        {

            // assert
            result = myString.GetNoBlank(input);

            // assert
            ActionGeneric();

        }
        public void ActionGetRepetir(int prmVezes)
        {

            // assert
            result = myString.GetRepetir(input, prmVezes);

            // assert
            ActionGeneric();

        }

        public void ActionGetSubstituir(string prmVelho, string prmNovo)
        {

            // assert
            result = myString.GetSubstituir(input, prmVelho, prmNovo);

            // assert
            ActionGeneric();

        }
        public void ActionGeneric()
        {

            // assert
            if (output != result)
                Assert.Fail(string.Format("Expected: <{0}>, Actual: <{1}>", output, result));

        }

    }

}