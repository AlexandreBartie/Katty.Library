using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty.Tools.Test.UNIT
{

    [TestClass()]
    public class TestUnit_Test : TestUnit
    {

        [TestMethod()]
        public void TST010_TestUnitByLine_EntradaUnicaLinha()
        {

            inputText("Linha 1");
            inputText("Linha 2");
            inputText("Linha 3");

            output("Linha 1Linha 2Linha 3");

            // act & assert
            AssertTest(prmResult: Input.txt);

        }

        [TestMethod()]
        public void TST020_TestUnitByLine_EntradaMultiplasLinhas()
        {

            input("Linha 1");
            input("Linha 2");
            input("linha 3");

            output("Linha 1");
            output("Linha 2");
            output("linha 3");

            // act & assert
            AssertTest(prmResult: Input.txt);

        }

    }
}
