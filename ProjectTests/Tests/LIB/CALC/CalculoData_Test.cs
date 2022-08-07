using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katty.Tools.Test.LIB.CALC
{

    [TestClass()]
    public class DynamicDateByCalc_Test : DynamicDate_Test
    {

        [TestMethod()]
        public void TST010_DynamicDateByCalc_Hoje()
        {

            input = "D=0";
            output = "05/06/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST020_DynamicDateByCalc_DiaFixo()
        {

            input = "D=15";
            output = "15/06/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST030_DynamicDateByCalc_DiaMais()
        {

            input = "D+1";
            output = "06/06/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST040_DynamicDateByCalc_DiaMenos()
        {

            input = "D-1";
            output = "04/06/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST050_DynamicDateByCalc_DiaMaisViraMes()
        {

            input = "D+28";
            output = "03/07/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }
        [TestMethod()]
        public void TST060_DynamicDateByCalc_DiaMenosViraMes()
        {

            input = "D-10";
            output = "26/05/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }
        [TestMethod()]
        public void TST070_DynamicDateByCalc_DiaFixoEstouro()
        {

            input = "D=31";
            output = "30/06/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }
        [TestMethod()]
        public void TST080_DynamicDateByCalc_MesFixo()
        {

            input = "M=2";
            output = "05/02/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST090_DynamicDateByCalc_MesMais()
        {

            input = "M+1";
            output = "05/07/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST100_DynamicDateByCalc_MesMenos()
        {

            input = "M-1";
            output = "05/05/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST110_DynamicDateByCalc_MesMaisViraAno()
        {

            input = "M+7";
            output = "05/01/2022";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST120_DynamicDateByCalc_MesMenosViraAno()
        {

            input = "M-6";
            output = "05/12/2020";

            // act & assert
            ActionDynamicDateByCalc();

        }


        [TestMethod()]
        public void TST130_DynamicDateByCalc_MesEstouro()
        {

            input = "M=13";
            output = "05/12/2021";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST140_DynamicDateByCalc_AnoFixo()
        {

            input = "A=2024";
            output = "05/06/2024";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST150_DynamicDateByCalc_AnoMais()
        {

            input = "A+1";
            output = "05/06/2022";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST160_DynamicDateByCalc_AnoMenos()
        {

            input = "A-1";
            output = "05/06/2020";

            // act & assert
            ActionDynamicDateByCalc();

        }
        [TestMethod()]
        public void TST170_DynamicDateByCalc_AnoMenos18()
        {

            input = "A-18";
            output = "05/06/2003";

            // act & assert
            ActionDynamicDateByCalc();

        }

        [TestMethod()]
        public void TST180_DynamicDateByCalc_AnoEstouro()
        {

            input = "A=4500";
            output = "05/06/2500";

            // act & assert
            ActionDynamicDateByCalc();

        }

    }

    [TestClass()]
    public class DynamicDateByView_Test : DynamicDate_Test
    {

        [TestMethod()]
        public void TST_DynamicDateByView_010_FormatacaoPadrao()
        {

            input = "D+1|M=3|A-20";
            output = "06/03/2001";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_020_UltimoDiaMesAnterior()
        {

            input = "D=1|D-1";
            output = "31/05/2021";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_030_UltimoDiaMesPosterior()
        {

            input = "D=1|D-1|M+2";
            output = "31/07/2021";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_040_UltimoDiaMesFevereiroAnoCorrente()
        {

            input = "D=31|M=2";
            output = "28/02/2021";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_050_UltimoDiaMesFevereiroAnoSeguinte()
        {

            input = "D=31|M=2|A+1";
            output = "28/02/2022";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_060_UltimoDiaMesFevereiroAnoBissexto()
        {

            input = "D=31|M=2|A=2024";
            output = "29/02/2024";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_070_PenultimoDiaProximoMes()
        {

            input = "D=1|D-2|M+2";
            output = "30/07/2021";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_080_3MesesAntes()
        {

            input = "M-3:MMAAAA";
            output = "032021";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_090_3MesesDepois()
        {

            input = "M+3:MMAAAA";
            output = "092021";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_100_CaixaBaixa()
        {

            input = "d=31|m=2|a=2024:aaaammdd";
            output = "20240229";

            // act & assert
            ActionDynamicDateByView();

        }

        [TestMethod()]
        public void TST_DynamicDateByView_110_EspacosEmBranco()
        {

            input = "   d  =  31  |  m  =  2  |  a  =  2024  : aaaa mm dd ";
            output = " 2024 02 29 ";

            // act & assert
            ActionDynamicDateByView();

        }

    }


    [TestClass()]
    public class DynamicDateByStatic_Test : DynamicDate_Test
    {

        [TestMethod()]
        public void TST_DynamicDateByStatic_010_FormatacaoPadrao()
        {

            input = "";
            output = "05/06/2021";

            // act & assert
            ActionDynamicDateByStatic();

        }
        [TestMethod()]
        public void TST_DynamicDateByStatic_010_MesAno()
        {

            input = "MMAAAA";
            output = "062021";

            // act & assert
            ActionDynamicDateByStatic();

        }
        [TestMethod()]
        public void TST_DynamicDateByStatic_010_SiglaMesAno()
        {

            input = "MMM-AAAA";
            output = "Jun-2021";

            // act & assert
            ActionDynamicDateByStatic();

        }

    }

    public class DynamicDate_Test
    {


        public DateTime ancora = new DateTime(2021, 6, 5);

        public string input;
        public string output;
        public string result;


        public void ActionDynamicDateByCalc()
        {
            // assert
            result = myDate.Calc(ancora, prmSintaxe: input).ToString("dd/MM/yyyy");

            // assert
            ActionGeneric();

        }

        public void ActionDynamicDateByView()
        {

            // assert
            result = myDate.View(ancora, prmSintaxe: input);

            // assert
            ActionGeneric();

        }
        public void ActionDynamicDateByStatic()
        {
            // assert
            result = myDate.Static(ancora, prmFormato: input);

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

