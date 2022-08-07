using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katty.Tools.Test.LIB.GENERIC
{
    [TestClass()]
    public class myBrick_Test
    {

        string input;
        string output;
        string result;

        [TestMethod()]
        public void TST_Get_020_Inexistente()
        {

            input = "Alexandre << *1234* >> Bartie";
            output = "";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_030_Espaco()
        {

            input = "Alexandre <<* *>> Bartie";
            output = " ";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_040_Vazio()
        {

            input = "Alexandre <<**>> Bartie";
            output = "";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_050_TextoVazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_060_Null()
        {

            input = null;
            output = "";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_070_DelimitadorUnico()
        {

            input = "Alexandre ##D+0## Bartie";
            output = "D+0";

            // act & assert
            ActionGet(prmDelimitador: "##", prmPreserve: false);

        }

        [TestMethod()]
        public void TST_Get_080_DelimitadorPreservado()
        {

            input = "Alexandre ##D+0## Bartie";
            output = "##D+0##";

            // act & assert
            ActionGet(prmDelimitador: "##", prmPreserve: true);

        }

        [TestMethod()]
        public void TST_Get_090_DelimitadoresSobrepostosCompleto()
        {

            input = "Alexandre <<***>> Bartie";
            output = "*";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_100_DelimitadoresSobrepostosIncompleto()
        {

            input = "Alexandre <<*>> Bartie";
            output = "";

            // act & assert
            ActionGet();

        }

        [TestMethod()]
        public void TST_Get_110_DelimitadoresSobrepostosCompletoPreservado()
        {

            input = "Alexandre <<***>> Bartie";
            output = "<<***>>";

            // act & assert
            ActionGet(prmPreserve: true);

        }

        [TestMethod()]
        public void TST_Get_120_DelimitadoresSobrepostosIncompletoPreservado()
        {

            input = "Alexandre <<*>> Bartie";
            output = "<<**>>";

            // act & assert
            ActionGet(prmPreserve: true);

        }

        [TestMethod()]
        public void TST_Get_130_DelimitadorNaoEncontrado()
        {

            input = "Alexandre ##D+0## Bartie";
            output = "";

            // act & assert
            ActionGet(prmDelimitador: "|");

        }

        [TestMethod()]
        public void TST_Get_140_DelimitadorFinalNaoEncontrado()
        {

            input = "Alexandre [D+0[ Bartie";
            output = "";

            // act & assert
            ActionGet(prmDelimitadorInicial: "[", prmDelimitadorFinal: "]");

        }

        [TestMethod()]
        public void TST_GetRemove_010_DelimitadorRemovido()
        {

            input = "Alexandre #D+10# Bartie";
            output = "Alexandre  Bartie";

            // act & assert
            ActionGetRemove(prmDelimitador: "#");

        }

        [TestMethod()]
        public void TST_GetRemove_020_DelimitadorRemovidoTrim()
        {

            input = "Alexandre #D+10# Bartie";
            output = "Alexandre Bartie";

            // act & assert
            ActionGetRemove(prmDelimitador: "#", prmTRIM: true);

        }

        [TestMethod()]
        public void TST_GetRemove_030_DelimitadorUnidoRemovido()
        {

            input = "Alexandre ##D+0## Bartie";
            output = "Alexandre  Bartie";

            // act & assert
            ActionGetRemove(prmDelimitador: "##");

        }

        [TestMethod()]
        public void TST_GetBefore_010_Padrao()
        {

            input = "Alexandre #D+0# Bartie";
            output = "Alexandre ";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetBefore_020_PosicaoInicio()
        {

            input = "#D+0# Alexandre Bartie";
            output = "";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetBefore_030_PosicaoInicioTRIM()
        {

            input = "#D+0# Alexandre Bartie";
            output = "";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#", prmTRIM: true);

        }

        [TestMethod()]
        public void TST_GetBefore_040_PosicaoFinal()
        {

            input = "Alexandre Bartie #D+0#";
            output = "Alexandre Bartie ";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetBefore_050_PosicaoFinalTRIM()
        {

            input = "Alexandre Bartie #D+0#";
            output = "Alexandre Bartie";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#", prmTRIM: true);

        }

        [TestMethod()]
        public void TST_GetBefore_060_Vazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetBefore_070_Null()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetBefore(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetBefore_080_myBrickNull()
        {

            input = "Alexandre Bartie";
            output = "Alexandre Bartie";

            // act & assert
            ActionGetBefore(prmmyBrick: null);

        }

        [TestMethod()]
        public void TST_GetBefore_090_myBrickVazio()
        {

            input = "Alexandre Bartie";
            output = "Alexandre Bartie";

            // act & assert
            ActionGetBefore(prmmyBrick: "");

        }

        [TestMethod()]
        public void TST_GetBefore_100_myBrickNaoEncontrado()
        {

            input = "Alexandre Bartie";
            output = "Alexandre Bartie";

            // act & assert
            ActionGetBefore(prmmyBrick: "#");

        }

        [TestMethod()]
        public void TST_GetAfter_010_Padrao()
        {

            input = "Alexandre #D+0# Bartie";
            output = " Bartie";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetAfter_020_PosicaoInicio()
        {

            input = "#D+0# Alexandre Bartie";
            output = " Alexandre Bartie";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetAfter_030_PosicaoInicioTRIM()
        {

            input = "#D+0# Alexandre Bartie";
            output = "Alexandre Bartie";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#", prmTRIM: true);

        }

        [TestMethod()]
        public void TST_GetAfter_040_PosicaoFinal()
        {

            input = "Alexandre Bartie #D+0#";
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetAfter_050_PosicaoFinalTRIM()
        {

            input = "Alexandre Bartie #D+0#";
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#", prmTRIM: true);

        }

        [TestMethod()]
        public void TST_GetAfter_060_Vazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetAfter_070_Null()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: "#D+0#");

        }

        [TestMethod()]
        public void TST_GetAfter_080_myBrickNull()
        {

            input = "Alexandre Bartie";
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: null);

        }

        [TestMethod()]
        public void TST_GetAfter_090_myBrickVazio()
        {

            input = "Alexandre Bartie";
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: "");

        }

        [TestMethod()]
        public void TST_GetAfter_100_myBrickNaoEncontrado()
        {

            input = "Alexandre Bartie";
            output = "";

            // act & assert
            ActionGetAfter(prmmyBrick: "#");

        }

        [TestMethod()]
        public void TST_GetChange_010_Padrao()
        {

            input = "Alexandre ##D+0## Bartie";
            output = "Alexandre 'D+0' Bartie";

            // act & assert
            ActionGetChange(prmDelimitador: "##", prmDelimitadorNovo: "'");

        }
        [TestMethod()]
        public void TST_GetChange_020_Multiplos()
        {

            input = "##um## Alexandre ##D+0## Bartie ##zero##";
            output = "'um' Alexandre 'D+0' Bartie 'zero'";

            // act & assert
            ActionGetChange(prmDelimitador: "##", prmDelimitadorNovo: "'");

        }

        private void ActionGet() => ActionGet(prmPreserve: false);
        private void ActionGet(bool prmPreserve) => ActionGet(prmDelimitadorInicial: "<<*", prmDelimitadorFinal: "*>>", prmPreserve);
        private void ActionGet(string prmDelimitador) => ActionGet(prmDelimitador, prmPreserve: false);
        private void ActionGet(string prmDelimitador, bool prmPreserve) => ActionGet(prmDelimitador, prmDelimitador, prmPreserve);
        private void ActionGet(string prmDelimitadorInicial, string prmDelimitadorFinal) => ActionGet(prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: false);
        private void ActionGet(string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmPreserve)
        {

            // assert
            result = myBrick.Get(input, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve);

            // assert
            ActionGeneric();

        }

        private void ActionGetRemove() => ActionGetRemove(prmDelimitadorInicial: "<<*", prmDelimitadorFinal: "*>>");
        private void ActionGetRemove(string prmDelimitador) => ActionGetRemove(prmDelimitador, prmTRIM: false);
        private void ActionGetRemove(string prmDelimitador, bool prmTRIM) => ActionGetRemove(prmDelimitador, prmDelimitador, prmTRIM);

        private void ActionGetRemove(string prmDelimitadorInicial, string prmDelimitadorFinal) => ActionGetRemove(prmDelimitadorInicial, prmDelimitadorFinal, prmTRIM: false);
        private void ActionGetRemove(string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmTRIM)
        {

            // assert
            result = myBrick.Remove(input, prmDelimitadorInicial, prmDelimitadorFinal, prmTRIM);

            // assert
            ActionGeneric();

        }

        private void ActionGetBefore(string prmmyBrick) => ActionGetBefore(prmmyBrick, prmTRIM: false);
        private void ActionGetBefore(string prmmyBrick, bool prmTRIM)
        {

            // assert
            result = myBrick.GetBefore(input, prmmyBrick, prmTRIM);

            // assert
            ActionGeneric();

        }

        private void ActionGetAfter(string prmmyBrick) => ActionGetAfter(prmmyBrick, prmTRIM: false);
        private void ActionGetAfter(string prmmyBrick, bool prmTRIM)
        {

            // assert
            result = myBrick.GetAfter(input, prmmyBrick, prmTRIM);

            // assert
            ActionGeneric();

        }

        private void ActionGetChange(string prmDelimitador, string prmDelimitadorNovo) 
        {

            // assert
            result = myBrick.GetChange(input, prmDelimitador, prmDelimitadorNovo);

            // assert
            ActionGeneric();

        }
        private void ActionGeneric()
        {

            // assert
            if (output != result)
                Assert.Fail(string.Format("Expected: {0}, Actual: {1}", output, result));

        }
    }



    [TestClass()]
    public class myPrefixo_Test
    {

        string input;
        string output;
        string result;


        [TestMethod()]
        public void TST_GetPrefixo_010_Padrao()
        {

            input = ">Alex: Bartie";
            output = "Alex";

            // act & assert
            ActionGetPrefixo();

        }

        [TestMethod()]
        public void TST_GetPrefixo_020_SemPrefixo()
        {

            input = "Alex: Bartie";
            output = "";

            // act & assert
            ActionGetPrefixo();

        }

        [TestMethod()]
        public void TST_GetPrefixo_030_SemDelimitador()
        {

            input = ">Alex Bartie";
            output = "Alex Bartie";

            // act & assert
            ActionGetPrefixo();

        }
        [TestMethod()]
        public void TST_GetPrefixo_040_Preservado()
        {

            input = ">Alex: Bartie";
            output = ">Alex:";

            // act & assert
            ActionGetPrefixo(prmPreserve: true);

        }

        [TestMethod()]
        public void TST_GetPrefixo_050_ParametrosBrancos()
        {

            input = ">Alex   :   ";
            output = "Alex";

            // act & assert
            ActionGetPrefixo();

        }

        [TestMethod()]
        public void TST_GetPrefixo_050_ApenasBrancos()
        {

            input = ">  :  ";
            output = "";

            // act & assert
            ActionGetPrefixo();

        }

        [TestMethod()]
        public void TST_GetPrefixo_060_Vazio()
        {

            input = "";
            output = "";

            // act & assert
            ActionGetPrefixo();

        }

        [TestMethod()]
        public void TST_GetPrefixo_070_Null()
        {

            input = null;
            output = "";

            // act & assert
            ActionGetPrefixo();

        }

        private void ActionGetPrefixo() => ActionGetPrefixo(prmPreserve: false);
        private void ActionGetPrefixo(bool prmPreserve) => ActionGetPrefixo(prmSinal: ">", prmDelimitador: ":", prmPreserve);
        private void ActionGetPrefixo(string prmSinal, string prmDelimitador, bool prmPreserve)
        {

            // assert
            result = myBrickPrefixo.GetPrefixo(input, prmSinal, prmDelimitador, prmPreserve);

            // assert
            ActionGeneric();

        }

        private void ActionGeneric()
        {

            // assert
            if (output != result)
                Assert.Fail(string.Format("Expected: {0}, Actual: {1}", output, result));

        }

    }
}