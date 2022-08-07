using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Katty.Tools.Test.LIB.VARS
{
    [TestClass()]
    public class CAT_010_VarsByFormatTextString_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_FormatTextString_FormatoPadrao()
        {

            input = "ALEXANDRE BARTIE";
            output = "ALEXA";

            // act & assert
            ActionFormatText("X(5)");

        }
        [TestMethod()]
        public void TST020_FormatTextString_SaidaComBrancos()
        {

            input = "ALEXANDRE BARTIE";
            output = "ALEXANDRE ";

            // act & assert
            ActionFormatText("X(10)");

        }
        [TestMethod()]
        public void TST030_FormatTextString_SaidaReduzida()
        {

            input = "ALEX";
            output = "ALEX";

            // act & assert
            ActionFormatText("X(10)");

        }
        [TestMethod()]
        public void TST040_FormatTextString_SaidaInvertida()
        {

            input = "ALEXANDRE BARTIE";
            output = "BARTIE";

            // act & assert
            ActionFormatText("X(-6)");

        }
        [TestMethod()]
        public void TST050_FormatTextString_FormatoNaoPadrao()
        {

            input = "ALEXANDRE BARTIE";
            output = "AL";

            // act & assert
            ActionFormatText("x(2)");

        }
        [TestMethod()]
        public void TST060_FormatTextString_FormatoZero()
        {

            input = "ALEXANDRE BARTIE";
            output = "";

            // act & assert
            ActionFormatText("x(0)");

        }
        [TestMethod()]
        public void TST070_FormatTextString_FormatoNaoInformado()
        {

            input = "ALEXANDRE BARTIE";
            output = "ALEXANDRE BARTIE";

            // act & assert
            ActionFormatText("x()");

        }
        [TestMethod()]
        public void TST080_FormatTextString_DelimitadorNaoEncontrado()
        {

            input = "ALEXANDRE BARTIE";
            output = "ALEXANDRE BARTIE";

            // act & assert
            ActionFormatText("X");

        }
        [TestMethod()]
        public void TST090_FormatTextString_FormatoNaoNumerico()
        {

            input = "ALEXANDRE BARTIE";
            output = "ALEXANDRE BARTIE";

            // act & assert
            ActionFormatText("X(ff)");

        }
    }
    [TestClass()]
    public class CAT_020_VarsByFormatTextSubstring_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_FormatTextSubstring_PosicaoMaisTamanho()
        {

            input = "ALEXANDRE BARTIE";
            output = "ANDRE";

            // act & assert
            ActionFormatText("X(5+5)");

        }
        [TestMethod()]
        public void TST020_FormatTextSubstring_PosicaoInicialFinal()
        {

            input = "ALEXANDRE BARTIE";
            output = "EXA";

            // act & assert
            ActionFormatText("X(3*5)");

        }
    }
    [TestClass()]
    public class CAT_030_VarsByFormatTextEffect_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_FormatTextEffect_FormatoUpper()
        {

            input = "Alexandre Bartie";
            output = "ALEXANDRE BARTIE";

            // act & assert
            ActionFormatText("X@Upper");

        }
        [TestMethod()]
        public void TST020_FormatTextEffect_FormatoLower()
        {

            input = "Alexandre Bartie";
            output = "alexandre bartie";

            // act & assert
            ActionFormatText("X@Lower");

        }

        [TestMethod()]
        public void TST030_FormatTextEffect_FormatoUpperCombinado()
        {

            input = "Alexandre Bartie";
            output = "ANDRE";

            // act & assert
            ActionFormatText("X(5+5)@Upper");

        }
        [TestMethod()]
        public void TST040_FormatTextEffect_FormatoLowerCombinado()
        {

            input = "Alexandre Bartie";
            output = "exandre bart";

            // act & assert
            ActionFormatText("X(3*14)@Lower");

        }
    }
    [TestClass()]
    public class CAT_040_VarsByFormatTextMask_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_FormatMask_FormatoPadrao()
        {
            // arrange

            input = "198402018831";
            output = "1984.02.01883-1";

            // act & assert
            ActionFormatMask("####.##.#####-#");

        }

        [TestMethod()]
        public void TST020_Mask_MascaraMaiorValor()
        {
            // arrange

            input = "198402018831";
            output = "1984.02.01883-1";

            // act & assert
            ActionFormatMask("##-#####.##.#####-#");

        }

        [TestMethod()]
        public void TST030_Mask_MascaraMenorValor()
        {
            // arrange
            input = "198402018831";
            output = "84.02.01883-1";

            // act & assert
            ActionFormatMask("##.##.#####-#");

        }
        [TestMethod()]
        public void TST040_Mask_MascaraVazia()
        {
            // arrange
            input = "198402018831";
            output = "198402018831";

            // act & assert
            ActionFormatMask("");

        }
        [TestMethod()]
        public void TST050_Mask_MascaraNula()
        {
            // arrange
            input = "198402018831";
            output = "198402018831";

            // act & assert
            ActionFormatMask(null);

        }
    }
    [TestClass()]
    public class CAT_050_VarsByFormatDate_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_VarDate_FormatoPadrao()
        {

            input = "DD/MM/AAAA";
            output = "08/05/2022";

            // act & assert
            ActionFormatDate();

        }
        [TestMethod()]
        public void TST020_VarDate_FormatoString()
        {

            input = "DDMMAAAA";
            output = "08052022";

            // act & assert
            ActionFormatDate();

        }

        [TestMethod()]
        public void TST030_VarDate_FormatoStringInvertido()
        {

            input = "AAAAMMDD";
            output = "20220508";

            // act & assert
            ActionFormatDate();

        }

        [TestMethod()]
        public void TST040_VarDate_FormatoVazio()
        {

            input = "";
            output = "08/05/2022 01:13:47";

            // act & assert
            ActionFormatDate();

        }

        [TestMethod()]
        public void TST050_VarDate_FormatoNull()
        {

            input = null;
            output = "08/05/2022 01:13:47";

            // act & assert
            ActionFormatDate();

        }
    }
    [TestClass()]
    public class CAT_060_VarsByFormatTime_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_VarTime_FormatoPadrao()
        {

            input = "hh:mm:ss";
            output = "01:13:47"; //20, 13, 47, 188

            // act & assert
            ActionFormatTime();

        }
        [TestMethod()]
        public void TST020_VarTime_FormatoString()
        {

            input = "hhmmss";
            output = "011347";

            // act & assert
            ActionFormatTime();

        }

        [TestMethod()]
        public void TST030_VarTime_FormatoStringInvertido()
        {

            input = "ssmmhh";
            output = "471301";

            // act & assert
            ActionFormatTime();

        }

        [TestMethod()]
        public void TST040_VarTime_FormatoVazio()
        {

            input = "";
            output = "08/05/2022 01:13:47";

            // act & assert
            ActionFormatTime();

        }

        [TestMethod()]
        public void TST050_VarTime_FormatoNull()
        {

            input = null;
            output = "08/05/2022 01:13:47";

            // act & assert
            ActionFormatTime();

        }
        [TestMethod()]
        public void TST060_VarTime_FormatoMileSegundos()
        {

            input = "fff";
            output = "908";

            // act & assert
            ActionFormatTime();

        }
    }
    [TestClass()]
    public class CAT_070_VarsByFormatDouble_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_VarDouble_DecimalPadrao()
        {

            input = @"#######.00";
            output = "123.45";

            // act & assert
            ActionFormatDouble(prmNumber: 123.45);

        }

        [TestMethod()]
        public void TST020_VarDouble_NumeroInteiro()
        {

            input = @"#######.00";
            output = "123.00";

            // act & assert
            ActionFormatDouble(prmNumber: 123);

        }

        [TestMethod()]
        public void TST030_VarDouble_NumeroUnicoDecimal()
        {

            input = @"#######.00";
            output = "123.40";

            // act & assert
            ActionFormatDouble(prmNumber: 123.4);

        }

        [TestMethod()]
        public void TST040_VarDouble_NumeroEstouroDecimal()
        {

            input = @"#######.00";
            output = "123.46";

            // act & assert
            ActionFormatDouble(prmNumber: 123.456);

        }

        [TestMethod()]
        public void TST050_VarDouble_NumeroMilhoes()
        {

            input = @"#######.00";
            output = "12143123.45";

            // act & assert
            ActionFormatDouble(prmNumber: 12143123.45);

        }

        [TestMethod()]
        public void TST060_VarDouble_NumeroMilhoesFormatado()
        {

            input = @"#,###,###.00";
            output = "12,143,123.45";

            // act & assert
            ActionFormatDouble(prmNumber: 12143123.45);

        }

        [TestMethod()]
        public void TST070_VarDouble_DecimalNegativo()
        {

            input = @"#######.00";
            output = "-123.40";

            // act & assert
            ActionFormatDouble(prmNumber: -123.4);

        }

        [TestMethod()]
        public void TST080_VarDouble_MultiplosFormatos()
        {

            input = @"#######.00;(#######.00)";
            output = "(123.40)";

            // act & assert
            ActionFormatDouble(prmNumber: -123.4);

        }
        [TestMethod()]
        public void TST090_VarDouble_NumeroDecimalFormatoVazio()
        {

            input = "";
            output = "123.4";

            // act & assert
            ActionFormatDouble(prmNumber: 123.4);

        }
        [TestMethod()]
        public void TST100_VarDouble_NumeroInteiroFormatoVazio()
        {

            input = "";
            output = "123";

            // act & assert
            ActionFormatDouble(prmNumber: 123);

        }
        [TestMethod()]
        public void TST110_VarDouble_NumeroNegativoFormatoVazio()
        {

            input = "";
            output = "-123";

            // act & assert
            ActionFormatDouble(prmNumber: -123);

        }
        [TestMethod()]
        public void TST120_VarDouble_NumeroZeroFormatoVazio()
        {

            input = "";
            output = "0";

            // act & assert
            ActionFormatDouble(prmNumber: 0);

        }
        [TestMethod()]
        public void TST130_VarDouble_NumeroDecimalFormatoNull()
        {

            input = null;
            output = "123.45";

            // act & assert
            ActionFormatDouble(prmNumber: 123.45);

        }

    }
    [TestClass()]
    public class CAT_080_VarsByFormatRandom_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_VarRandom_FormatoSemPrefixo()
        {

            input = "5";
            output = "90847";

            // act & assert
            ActionFormatRandom();

        }

        [TestMethod()]
        public void TST020_VarRandom_FormatoComPrefixo()
        {

            input = "CEP:[5]";
            output = "CEP:90847";

            // act & assert
            ActionFormatRandom();

        }

        [TestMethod()]
        public void TST030_VarRandom_FormatoExtendido()
        {

            input = "Fone: (35) [11]";
            output = "Fone: (35) 90847130108";

            // act & assert
            ActionFormatRandom();

        }

        [TestMethod()]
        public void TST040_VarRandom_FormatoVazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionFormatRandom();

        }

    }
    [TestClass()]
    public class CAT_090_VarsByFormatRandomDate_Test : xVars_Test
    {

        [TestMethod()]
        public void TST010_VarRandomDate_FormatoSemPrefixo()
        {

            input = "5";
            output = "90847";

            // act & assert
            ActionFormatRandomDate();

        }

        [TestMethod()]
        public void TST020_VarRandomDate_FormatoComPrefixo()
        {

            input = "CEP:[5]";
            output = "CEP:90847";

            // act & assert
            ActionFormatRandomDate();

        }

        [TestMethod()]
        public void TST030_VarRandomDate_FormatoExtendido()
        {

            input = "Fone: (35) [11]";
            output = "Fone: (35) 90847130108";

            // act & assert
            ActionFormatRandomDate();

        }

        [TestMethod()]
        public void TST040_VarRandomDate_FormatoVazio()
        {

            input = "";
            output = "90847130108052022";

            // act & assert
            ActionFormatRandomDate();

        }

    }

    public class xVars_Test
    {

        public DateTime date = new DateTime(2022, 05, 08, 01, 13, 47, 908);

        public string input;
        public string output;
        public string result;

        public void ActionFormatText(string prmFormat)
        {

            // assert
            result = myFormat.TextToString(input, prmFormat);

            // assert
            ActionGeneric();

        }
        public void ActionFormatMask(string prmMask)
        {

            // assert
            result = myFormat.TextToString(input, prmMask);

            // assert
            ActionGeneric();

        }
        public void ActionFormatDate()
        {

            // assert
            result = myFormat.DateToString(date, input);

            // assert
            ActionGeneric();

        }
        public void ActionFormatTime()
        {

            // assert
            result = myFormat.TimeToString(date, input);

            // assert
            ActionGeneric();

        }
        public void ActionFormatRandom()
        {

            // assert
            result = myRandom.Next(input);

            // assert
            ActionGeneric(prmCheck: false);

        }
        public void ActionFormatRandomDate()
        {

            // assert
            result = myRandomDate.Next(date, input);

            // assert
            ActionGeneric();

        }

        public void ActionFormatDouble(double prmNumber) => ActionFormatDouble(prmNumber, prmRegionalizacao: CultureInfo.InvariantCulture);
        public void ActionFormatDouble(double prmNumber, CultureInfo prmRegionalizacao) => ActionFormatDouble(prmNumber, prmRegionalizacao, prmCSV: false);
        public void ActionFormatDouble(double prmNumber, CultureInfo prmRegionalizacao, bool prmCSV)
        {

            // assert
            result = myFormat.DoubleToString(prmNumber, input, prmRegionalizacao, prmCSV);

            // assert
            ActionGeneric();

        }

        public void ActionGeneric() => ActionGeneric(prmCheck: true);
        public void ActionGeneric(bool prmCheck)
        {
            // assert
            if ((output != result) && prmCheck)
                Assert.Fail(string.Format("{2} Expected: <{0}> {2}   Actual: <{1}>", output, result, Environment.NewLine));

        }

    }
}
