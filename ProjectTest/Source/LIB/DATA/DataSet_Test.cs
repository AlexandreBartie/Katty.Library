using Katty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty.Tools.Test.LIB.DATA
{
    [TestClass()]
    public class CAT_010_DataMatrixSet : DataMatrix_Test
    {

        [TestMethod()]
        public void TST010_DataMatrixSet_AddView()
        {

            input("New View");

            output(@"New View");

            // act & assert
            CheckResult_DataMatrixSet();
        }

        [TestMethod()]
        public void TST020_DataMatrixSet_AddViews()
        {

            input("New View A");
            input("New View B");

            output("New View A");
            output("New View B");

            // act & assert
            CheckResult_DataMatrixSet();
        }

    }

    [TestClass()]
    public class CAT_020_DataMatrixInput : DataMatrix_Test
    {

        [TestMethod()]
        public void TST010_DataMatrixInput_AddInput()
        {

            input("cod_user[id]");

            output(@"cod_user:id");

            // act & assert
            CheckResult_DataMatrixInput();
        }

        [TestMethod()]
        public void TST020_DataMatrixInput_AddInputs()
        {

            input("cod_user[id], nom_user[name]");

            output("cod_user:id, nom_user:name");

            // act & assert
            CheckResult_DataMatrixInput();
        }


        [TestMethod()]
        public void TST030_DataMatrixInput_AddInputsFormatted()
        {

            input("cod_user[id:####], nom_user[name:X(30)]");

            output("cod_user:id:####, nom_user:name:X(30)");

            // act & assert
            CheckResult_DataMatrixInput();
        }

    }

    [TestClass()]
    public class CAT_030_DataMatrixFlow : DataMatrix_Test
    {

        [TestMethod()]
        public void TST010_DataMatrixFlow_AddFlow()
        {

            input(@"id=21836, name=Alexandre");

            output(@"id:= '21836', name:= 'Alexandre'");

            // act & assert
            CheckResult_DataMatrixFlow(prmView: "Main");
        }

        [TestMethod()]
        public void TST020_DataMatrixFlow_AddFlows()
        {

            input(@"id=21836, name=Alexandre, lastName=Bartie");
            input(@"id=12432, name=Sergio, nick=Malandro");


            output(@"id:= '21836', name:= 'Alexandre', lastName:= 'Bartie'");
            output(@"id:= '12432', name:= 'Sergio', nick:= 'Malandro'");

            // act & assert
            CheckResult_DataMatrixFlow(prmView: "Main");
        }

    }

    public class DataMatrix_Test : TestUnit
    {
        public void CheckResult_DataMatrixSet()
        {

            DataMatrix Matrix = new DataMatrix(Input.txt);

            // assert
            AssertTest(prmResult: Matrix.log);

        }

        public void CheckResult_DataMatrixInput()
        {

            DataView View = new DataView("Main");

            View.AddInputs(Input.txt);

            // assert
            AssertTest(prmResult: View.log);

        }

        public void CheckResult_DataMatrixFlow(string prmView)
        {

            DataView View = new DataView("Main");

            View.AddFlows(Input.txt);

            // assert
            AssertTest(prmResult: View.log);

        }

    }

}
