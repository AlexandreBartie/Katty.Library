using System;
using System.Collections.Generic;
using System.Text;
using Katty;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katty.Tools.Test.LIB.GENERIC
{
    [TestClass()]
    public class myDominio_Test : TestUnit
    {

        [TestMethod()]
        public void TST_010_GetDominio_Normal()
        {

            input("impacto { ALTO,MEDIO,BAIXO }");
            output("impacto: ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_020_GetDominio_SemKey()
        {

            input("{ ALTO,MEDIO,BAIXO }");
            output("ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_030_GetDominio_NormalValorPadrao()
        {

            input("impacto[ALTO] { ALTO,MEDIO,BAIXO }");
            output("impacto[ALTO]: ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_040_GetDominio_NormalValorPadraoInexistente()
        {

            input("impacto[MED] { ALTO,MEDIO,BAIXO }");
            output("impacto: ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_050_GetDominio_NormalValorPadraoPosicional()
        {

            input("impacto[2] { ALTO,MEDIO,BAIXO }");
            output("impacto[MEDIO]: ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_060_GetDominio_NormalValorPadraoPosicionalErrado()
        {

            input("impacto[4] { ALTO,MEDIO,BAIXO }");
            output("impacto: ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_070_GetDominio_SemLista()
        {

            input("impacto");
            output("impacto");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_080_GetDominio_SemListaValorPadrao()
        {

            input("impacto[ALTO]");
            output("impacto");

            // act & assert
            ActionGetDominio();

        }

        [TestMethod()]
        public void TST_090_GetDominio_Espacamentos()
        {

            input("       impacto  [   ALTO    ]   {   ALTO   ,   MEDIO   ,   BAIXO   }  ");
            output("impacto[ALTO]: ALTO,MEDIO,BAIXO");

            // act & assert
            ActionGetDominio();

        }
        [TestMethod()]
        public void TST_100_GetDominio_Vazio()
        {

            input("");
            output("");

            // act & assert
            ActionGetDominio();

        }
        [TestMethod()]
        public void TST_110_GetDominio_Nulos()
        {

            input(null);
            output("");

            // act & assert
            ActionGetDominio();

        }
        private void ActionGetDominio()
        {

            // assert
            myDominio dominio = new myDominio(Input.txt);

            // assert
            AssertTest(prmResult: dominio.log);

        }
    }

    [TestClass()]
    public class myDominios_Test : TestUnit
    {

        [TestMethod()]
        public void TST_010_GetDominios_TagsOpcoes()
        {

            input(" impacto { ALTO, MEDIO, BAIXO }");
            input("   tipo  { PROGRESSIVO, REGRESSIVO }");
            input("analista { ALEXANDRE, LISIA, VITOR }");
            input("situacao { PRONTO, EDICAO, ERRO, REFINAR }");

            output("impacto: ALTO,MEDIO,BAIXO");
            output("tipo: PROGRESSIVO,REGRESSIVO");
            output("analista: ALEXANDRE,LISIA,VITOR");
            output("situacao: PRONTO,EDICAO,ERRO,REFINAR");

            // act & assert
            ActionGetDominios();

        }

        [TestMethod()]
        public void TST_020_GetDominios_TagsOpcoesPadrao()
        {

            input("      impacto[2] { ALTO, MEDIO, BAIXO }");
            input("tipo[REGRESSIVO] { PROGRESSIVO, REGRESSIVO }");
            input("     analista[2] { ALEXANDRE, LISIA, VITOR }");
            input("  situacao[ERRO] { PRONTO, EDICAO, ERRO, REFINAR }");

            output("impacto[MEDIO]: ALTO,MEDIO,BAIXO");
            output("tipo[REGRESSIVO]: PROGRESSIVO,REGRESSIVO");
            output("analista[LISIA]: ALEXANDRE,LISIA,VITOR");
            output("situacao[ERRO]: PRONTO,EDICAO,ERRO,REFINAR");

            // act & assert
            ActionGetDominios();

        }

        [TestMethod()]
        public void TST_030_GetDominios_TagsRepetidas()
        {

            input(" impacto { ALTO, MEDIO, BAIXO }");
            input("    tipo { PROGRESSIVO, REGRESSIVO }");
            input("analista { ALEXANDRE, LISIA, VITOR }");
            input("    tipo { FACIL, MEDIO, DIFICIL }");
            input("situacao { PRONTO, EDICAO, ERRO, REFINAR }");

            output("impacto: ALTO,MEDIO,BAIXO");
            output("tipo: FACIL,MEDIO,DIFICIL");
            output("analista: ALEXANDRE,LISIA,VITOR");
            output("situacao: PRONTO,EDICAO,ERRO,REFINAR");
           
            // act & assert
            ActionGetDominios();

        }

        private void ActionGetDominios()
        {

            // assert
            myDominios dominios = new myDominios();

            dominios.AddItens(Input.GetList());

            // assert
            AssertTest(prmResult: dominios.log);

        }

    }

}
