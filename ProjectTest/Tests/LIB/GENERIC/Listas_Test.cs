using Katty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Katty.Tools.Test.LIB.GENERIC
{
    [TestClass()]
    public class myList_Test
    {
        string input;
        string output;

        myList Lista = new myList();

        [TestMethod()]
        public void TST010_FlowCSV_Padrao()
        {

            // arrange
            input = "1234,2345,3,5,78";
            output = "4,4,1,1,2";

            // act & assert
            ActionLista();

        }

        [TestMethod()]
        public void TST020_FlowCSV_ComEspacos()
        {

            // arrange
            input = " 1234 , 2345 , 3 , 5 , 78 ";
            output = "4,4,1,1,2";

            // act & assert
            ActionLista();

        }
        [TestMethod()]
        public void TST030_FlowCSV_ComVazios()
        {

            // arrange
            input = " 1234 ,  , 3 ,, 78 ";
            output = "4,0,1,0,2";

            // act & assert
            ActionLista();

        }
        [TestMethod()]
        public void TST040_FlowCSV_Unico()
        {

            // arrange
            input = " 923348 ";
            output = "6";

            // act & assert
            ActionLista();

        }

        [TestMethod()]
        public void TST050_FlowCSV_Vazio()
        {

            // arrange
            input = "";
            output = "0";

            // act & assert
            ActionLista();

        }

        [TestMethod()]
        public void TST060_FlowCSV_Espaco()
        {

            // arrange
            input = "   ";
            output = "0";

            // act & assert
            ActionLista();

        }

        [TestMethod()]
        public void TST070_FlowCSV_Nulo()
        {

            // arrange
            input = null;
            output = "";

            // act & assert
            ActionLista();

        }

        [TestMethod()]
        public void TST080_FlowCSV_SeparadorModificado()
        {

            // arrange
            input = " 12,3 $ 456,78 $ 3,23 ";
            output = "4$6$4";

            // act & assert
            ActionLista(prmSeparador: "$");

        }

        private void ActionLista() => ActionLista(prmSeparador: ",");
        private void ActionLista(string prmSeparador)
        {

            // assert
            Lista.Parse(input, prmSeparador);

            string result = Lista.log;

            // assert
            if (output != result)
                Assert.Fail(string.Format("Expected: <{0}>, Actual: <{1}>, Lista: <{2}>", output, result, input));

        }
    }

}
