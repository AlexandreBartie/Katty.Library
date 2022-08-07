using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Katty.Tools.Test.LIB.VARS
{
    [TestClass()]
    public class CultureByDouble_Test : xVars_Test
    {
        [TestMethod()]
        public void TST010_CultureByDouble_ValorPadrao_enUS()
        {

            input = @"#,###,###.00";
            output = "87,123.45";

            // act & assert
            ActionFormatDouble(prmNumber: 87123.45, prmRegionalizacao: new CultureInfo("en-US"));

        }
        [TestMethod()]
        public void TST020_CultureByDouble_ValorPadrao_ptBR()
        {

            input = @"#,###,###.00";
            output = "87.123,45";

            // act & assert
            ActionFormatDouble(prmNumber: 87123.45, prmRegionalizacao: new CultureInfo("pt-BR"));

        }
        [TestMethod()]
        public void TST030_CultureByDouble_FormatoCSV_enUS()
        {

            input = @"#,###,###.00";
            output = "87,123.45";

            // act & assert
            ActionFormatDouble(prmNumber: 87123.45, prmRegionalizacao: new CultureInfo("en-US"), prmCSV: true);

        }
        [TestMethod()]
        public void TST040_CultureByDouble_FormatoCSV_ptBR()
        {

            input = @"#,###,###.00";
            output = @"""87.123,45""";

            // act & assert
            ActionFormatDouble(prmNumber: 87123.45, prmRegionalizacao: new CultureInfo("pt-BR"), prmCSV: true);

        }
        [TestMethod()]
        public void TST050_CultureByDouble_ValorZeroFormatoCSV_enUS()
        {

            input = @"#,###,##0.00";
            output = @"0.00";

            // act & assert
            ActionFormatDouble(prmNumber: 0, prmRegionalizacao: new CultureInfo("en-US"), prmCSV: true);

        }
        [TestMethod()]
        public void TST060_CultureByDouble_ValorZeroFormatoCSV_ptBR()
        {

            input = @"#,###,##0.00";
            output = @"""0,00""";

            // act & assert
            ActionFormatDouble(prmNumber: 0, prmRegionalizacao: new CultureInfo("pt-BR"), prmCSV: true);

        }

    }
}
