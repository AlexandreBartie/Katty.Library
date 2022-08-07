using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty.Tools.Test.LIB.TAGS
{
    [TestClass()]
    public class NewTAGS_Test : TestUnit
    {

        [TestMethod()]
        public void TST010_NewTags_EntradaPadrao()
        {

            // arrange
            input("      impacto[2] { ALTO, MEDIO, BAIXO }");
            input("tipo[REGRESSIVO] { PROGRESSIVO, REGRESSIVO }");
            input("     analista[2] { ALEXANDRE, LISIA, VITOR }");
            input("  situacao[ERRO] { PRONTO, EDICAO, ERRO, REFINAR }");

            output(@"impacto[MEDIO]: ALTO,MEDIO,BAIXO");
            output(@"tipo[REGRESSIVO]: PROGRESSIVO,REGRESSIVO");
            output(@"analista[LISIA]: ALEXANDRE,LISIA,VITOR");
            output(@" situacao[ERRO]: PRONTO,EDICAO,ERRO,REFINAR");

            // act & assert
            AssertNewTAG();

        }

        [TestMethod()]
        public void TST020_NewTags_EntradasRepetidas()
        {

            // arrange
            input("      impacto[2] { ALTO, MEDIO, BAIXO }");
            input("tipo[REGRESSIVO] { PROGRESSIVO, REGRESSIVO }");
            input("     analista[2] { ALEXANDRE, LISIA, VITOR }");
            input("  situacao[ERRO] { PRONTO, EDICAO, ERRO, REFINAR }");
            input("    tipo[MAGICO] { MAGICO, INTERESSADO }");
            input("     analista[2] { ALEXANDRE, JORGE }");

            output(@"impacto[MEDIO]: ALTO,MEDIO,BAIXO");
            output(@"tipo[REGRESSIVO]: PROGRESSIVO,REGRESSIVO");
            output(@"analista[LISIA]: ALEXANDRE,LISIA,VITOR");
            output(@" situacao[ERRO]: PRONTO,EDICAO,ERRO,REFINAR");

            // act & assert
            AssertNewTAG();

        }
        [TestMethod()]
        public void TST030_NewTags_EntradasPadraoInexistente()
        {

            // arrange
            input("       impacto { ALTO, MEDIO, BAIXO }");
            input("       tipo[0] { PROGRESSIVO, REGRESSIVO }");
            input("   analista[4] { ALEXANDRE, LISIA, VITOR }");
            input("situacao[PRONT] { PRONTO, EDICAO, ERRO, REFINAR }");


            output(@"impacto: ALTO,MEDIO,BAIXO");
            output(@"tipo: PROGRESSIVO,REGRESSIVO");
            output(@"analista: ALEXANDRE,LISIA,VITOR");
            output(@" situacao: PRONTO,EDICAO,ERRO,REFINAR");

            // act & assert
            AssertNewTAG();

        }
        private void AssertNewTAG()
        {

            myTags Tags = new myTags();

            foreach (TestLine line in Input)
                Tags.Add(line.txt);

            AssertTest(prmResult: Tags.txt);
        }

    }

    [TestClass()]
    public class SetTAGS_Test : TestUnit
    { 
        [TestMethod()]
        public void TST010_SetTags_SetPadrao()
        {

            // arrange
            input("      impacto[2] { ALTO, MEDIO, BAIXO }");
            input("tipo[REGRESSIVO] { PROGRESSIVO, REGRESSIVO }");
            input("     analista[2] { ALEXANDRE, LISIA, VITOR }");
            input("  situacao[ERRO] { PRONTO, EDICAO, ERRO, REFINAR }");

            output(@"impacto: 'MEDIO'");
            output(@"tipo: 'REGRESSIVO'");
            output(@"analista: 'LISIA'");
            output(@"situacao: 'ERRO'");

            // act & assert
            AssertSetTAG(prmTupla: "analista = ALEXANDRE");

        }

        [TestMethod()]
        public void TST020_SetTags_TagNaoEncontrada()
        {

            // arrange
            input("      impacto[2] { ALTO, MEDIO, BAIXO }");
            input("tipo[REGRESSIVO] { PROGRESSIVO, REGRESSIVO }");
            input("     analista[2] { ALEXANDRE, LISIA, VITOR }");
            input("  situacao[ERRO] { PRONTO, EDICAO, ERRO, REFINAR }");

            output(@"impacto: 'MEDIO'");
            output(@"tipo: 'REGRESSIVO'");
            output(@"analista: 'LISIA'");
            output(@"situacao: 'ERRO'");

            // act & assert
            AssertSetTAG(prmTupla: "anelesta = ALEXANDRE");

        }


        [TestMethod()]
        public void TST030_SetTags_ValueNaoEncontrada()
        {

            // arrange
            input("      impacto[2] { ALTO, MEDIO, BAIXO }");
            input("tipo[REGRESSIVO] { PROGRESSIVO, REGRESSIVO }");
            input("     analista[2] { ALEXANDRE, LISIA, VITOR }");
            input("  situacao[ERRO] { PRONTO, EDICAO, ERRO, REFINAR }");

            output(@"impacto: 'MEDIO'");
            output(@"tipo: 'REGRESSIVO'");
            output(@"analista: 'LISIA'");
            output(@"situacao: 'ERRO'");

            // act & assert
            AssertSetTAG(prmTupla: "analista = MAJOR");

        }

        private void AssertSetTAG(string prmTupla)
        {

            myTags Tags = new myTags();

            foreach (TestLine line in Input)
                Tags.Add(line.txt);

            AssertTest(prmResult: Tags.log);
        }

    }

}