using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katty.Tools.Tests.LIB.PARSE.JSON
{
    [TestClass()]
    public class JSON_SimpleFlow_Test : TestUnit
    {

        [TestMethod()]
        public void TST010_FlowSimples_Padrao()
        {

            // arrange
            input(@"{ 'Nome': 'Alexandre', 'email': 'alexandre_bartie@hotmail.com' }");
            output(@"[Nome]: 'Alexandre', [email]: 'alexandre_bartie@hotmail.com'");

            // act & assert
            AssertJSON();

        }

        [TestMethod()]
        public void TST020_FlowSimples_ValoresComAspas()
        {

            // arrange
            input(@"{ 'tag': 'Aluno', 'sql': 'SELECT * from Alunos WHERE situacao = ok' }");
            output(@"[tag]: 'Aluno', [sql]: 'SELECT * from Alunos WHERE situacao = ok'");

            // act & assert
            AssertJSON();

        }

        [TestMethod()]
        public void TST030_FlowSimples_ValoresComBarraNormal()
        {

            // arrange
            input(@"{ 'path': 'c:\\MassaTestes\\', 'branch': '1084', 'porta': '1521' }");
            output(@"[path]: 'c:\MassaTestes\', [branch]: '1084', [porta]: '1521'");

            // act & assert
            AssertJSON();

        }

        [TestMethod()]
        public void TST040_FlowSimples_ValoresComBarraDobrada()
        {

            // arrange
            input(@"{ 'path': 'c:\\MassaTestes\\', 'branch': '1084', 'porta': '1521' }");
            output(@"[path]: 'c:\MassaTestes\', [branch]: '1084', [porta]: '1521'");

            // act & assert
            AssertJSON();

        }

        [TestMethod()]
        public void TST050_FlowSimples_ValoresComBarraInvertida()
        {

            // arrange
            input(@"{ 'path': 'c:/MassaTestes/', 'branch': '1084', 'porta': '1521' }");
            output(@"[path]: 'c:/MassaTestes/', [branch]: '1084', [porta]: '1521'");

            // act & assert
            AssertJSON();

        }


        [TestMethod()]
        public void TST060_FlowSimples_ErroFormatacao()
        {

            // arrange
            input(@"{'COD_MATRICULA': ####.##.#####-#' }");
            output(@"");

            // act & assert
            AssertJSON();

        }

        private void AssertJSON()
        {

            // act
            myJSON Dados = new myJSON(Input.txt); Dados.Save();

            // assert
            AssertTest(Dados.lines);

        }
    }

    [TestClass()]
    public class JSON_MultipleFlow_Test : TestUnit
    {

        [TestMethod()]
        public void TST010_FlowMultiplo_Padrao()
        {

            // arrange
            input(@"{ 'Nome': 'Alexandre', 'email': 'alexandre_bartie@hotmail.com' }");
            input(@"{ 'Nome': 'Bartie', 'email': 'bartie.devops@outlook.com' }");

            outputText(@"[ ");
            outputText(@"{ ""Nome"": ""Alexandre"", ""email"": ""alexandre_bartie@hotmail.com"" }, ");
            outputText(@"{ ""Nome"": ""Bartie"", ""email"": ""bartie.devops@outlook.com"" } ");
            outputText(@"]");

            // act & assert
            AssertJSON();

        }

        [TestMethod()]
        public void TST020_FlowMultiplo_FlowCombinado()
        {

            // arrange
            input("{ 'Nome': 'Alexandre', 'email': 'alexandre_bartie@hotmail.com' }", prmArg: @"{ 'Nome': 'Renato' }");
            input("{ 'Nome': 'Bartie', 'email': 'bartie.devops@outlook.com' }", prmArg: @"{ 'Nome': 'José' }");

            outputText(@"[ ");
            outputText(@"{ ""Nome"": ""Renato"", ""email"": ""alexandre_bartie@hotmail.com"" }, ");
            outputText(@"{ ""Nome"": ""José"", ""email"": ""bartie.devops@outlook.com"" } ");
            outputText(@"]");

            // act & assert
            AssertJSON();

        }

        [TestMethod()]
        public void TST030_FlowMultiplo_FlowCombinadoExtendido()
        {

            // arrange
            input(@"{ 'Nome': 'Alexandre', 'email': 'alexandre_bartie@hotmail.com' }", prmArg: @"{ 'Nome': 'Renato', 'sobrenome': 'Andrade' }");
            input(@"{ 'Nome': 'Bartie', 'email': 'bartie.devops@outlook.com' }", prmArg: @"{ 'Nome': 'José', 'sobrenome': 'da Silva' }");

            outputText(@"[ ");
            outputText(@"{ ""Nome"": ""Renato"", ""email"": ""alexandre_bartie@hotmail.com"", ""sobrenome"": ""Andrade"" }, ");
            outputText(@"{ ""Nome"": ""José"", ""email"": ""bartie.devops@outlook.com"", ""sobrenome"": ""da Silva"" } ");
            outputText(@"]");

            // act & assert
            AssertJSON();

        }

        private void AssertJSON()
        {

            // act
            myJSON Dados = new myJSON();

            foreach (TestLine Line in Input)
                Dados.Add(Line.txt, Line.arg);

            Dados.Save();

            // assert
            AssertTest(Dados.flows);

        }
    }

}
