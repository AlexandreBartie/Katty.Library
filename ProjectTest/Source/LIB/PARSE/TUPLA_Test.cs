using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty.Tools.Tests.LIB.PARSE.TUPLA
{

    [TestClass()]
    public class TUPLA_Test : TestUnit
    {

        myTupla Tupla;

        [TestMethod()]
        public void TST010_Tupla_EntradaPadrao()
        {

            // arrange
            input(@"Nome=Alexandre Bartie");

            output(@"Nome:= 'Alexandre Bartie'");

            //// act
            Tupla = new myTupla(Input.txt);

            // act & assert
            AssertTUPLA();

        }

        [TestMethod()]
        public void TST020_Tupla_EntradaDetalhes()
        {

            // arrange
            input(@"Nome=Alexandre Bartie[  cod_name : X(10) ] ");

            output(@"Nome:= 'Alexandre Bartie' [cod_name:X(10)]");

            //// act
            Tupla = new myTupla(Input.txt);

            // act & assert
            AssertTUPLA();

        }

        private void AssertTUPLA()
        {
            AssertTest(prmResult: Tupla.log);
        }

    }

    [TestClass()]
    public class TUPLAS_Test : TestUnit
    {

        myTuplas Tuplas;

        [TestMethod()]
        public void TST010_Tuplas_UnicoPar()
        {

            // arrange
            input(@"Nome=Alexandre Bartie");

            output(@"Nome:= 'Alexandre Bartie'");

            //// act
            Tuplas = new myTuplas(Input.txt);

            // act & assert
            AssertTUPLAS();

        }

        [TestMethod()]
        public void TST020_Tuplas_VariosPares()
        {

            // arrange
            input(@"Nome=Alexandre Bartie, nascimento=05/06/1971, email=alexandre_bartie@hotmail.com");

            output(@"Nome:= 'Alexandre Bartie', nascimento:= '05/06/1971', email:= 'alexandre_bartie@hotmail.com'");

            //// act
            Tuplas = new myTuplas(Input.txt);

            // act & assert
            AssertTUPLAS();

        }
        [TestMethod()]
        public void TST030_Tuplas_EntradaVazia()
        {

            // arrange
            input(@"");

            output(@"");

            //// act
            Tuplas = new myTuplas(Input.txt);

            // act & assert
            AssertTUPLAS();

        }
        [TestMethod()]
        public void TST040_Tuplas_EntradaNull()
        {

            // arrange
            input(null);

            output("");

            //// act
            Tuplas = new myTuplas(Input.txt);

            // act & assert
            AssertTUPLAS();

        }

        [TestMethod()]
        public void TST050_Tuplas_ListaNomes()
        {

            // arrange
            input(@"Nome, nascimento, email");

            output(@"Nome:= '', nascimento:= '', email:= ''");

            //// act
            Tuplas = new myTuplas(Input.txt);

            // act & assert
            AssertTUPLAS();

        }
        [TestMethod()]
        public void TST060_Tuplas_ListaNomes()
        {

            // arrange
            input(@"Nome, nascimento, email");

            output(@"Nome:= '', nascimento:= '', email:= ''");

            //// act
            Tuplas = new myTuplas();

            Tuplas.Parse("Nome"); Tuplas.Parse("nascimento"); Tuplas.Parse("email");

            // act & assert
            AssertTUPLAS();

        }
        [TestMethod()]
        public void TST070_Tuplas_VariasTuplas()
        {

            // arrange
            input(@"Nome, nascimento, email");

            output(@"Nome:= 'Alexandre Bartie', nascimento:= '05/06/1971', email:= 'alexandre_bartie@hotmail.com'");

            //// act
            Tuplas = new myTuplas();

            Tuplas.Parse("Nome=Alexandre Bartie"); Tuplas.Parse("nascimento=05/06/1971"); Tuplas.Parse("email=alexandre_bartie@hotmail.com");

            // act & assert
            AssertTUPLAS();

        }
        [TestMethod()]
        public void TST080_Tuplas_AtualizarParcialmente()
        {

            // arrange
            input(@"Nome, nascimento, email");

            output(@"Nome:= 'Renato Andrade', nascimento:= '', email:= 'renato.andrade@gmail.com'");

            //// act
            Tuplas = new myTuplas(Input.txt);

            Tuplas.Parse("Nome=Renato Andrade, email=renato.andrade@gmail.com");

            // act & assert
            AssertTUPLAS();

        }
        [TestMethod()]
        public void TST090_Tuplas_AtualizarPosicionalmenteValores()
        {

            // arrange
            input(@"Nome, nascimento, email");

            output(@"Nome:= 'Renato Andrade', nascimento:= '', email:= 'renato.andrade@gmail.com'");

            //// act
            Tuplas = new myTuplas(Input.txt);

            Tuplas.SetValues("Renato Andrade, , renato.andrade@gmail.com");

            // act & assert
            AssertTUPLAS();

        }
        private void AssertTUPLAS()
        {
            AssertTest(prmResult: Tuplas.log);
        }
    }

}
