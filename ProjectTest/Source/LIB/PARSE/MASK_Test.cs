using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty.Tools.Test.LIB.PARSE.MASK
{
    [TestClass()]
    public class myMask_Test : TestUnit
    {

        [TestMethod()]
        public void TST010_Mask_MascaraPadrao()
        {

            input("198402018831");
            output("1984.02.01883-1");

            // act & assert
            ActionMask(prmLista: "COD_MATRICULA: ####.##.#####-#", prmChave: "COD_MATRICULA");

        }

        [TestMethod()]
        public void TST020_Mask_MascaraSemEspacos()
        {
            // arrange
            input( "11959056700");
            output("11-959-056-700");

            // act & assert
            ActionMask(prmLista: "PHONE: ##-###-###-###", prmChave: "PHONE");

        }


        [TestMethod()]
        public void TST030_Mask_MascaraMaiorValor()
        {
            // arrange
            input("198402018831");
            output("1984.02.01883-1");

            // act & assert
            ActionMask(prmLista: "COD_MATRICULA: ##-#####.##.#####-#", prmChave: "COD_MATRICULA");

        }

        [TestMethod()]
        public void TST040_Mask_MascaraMenorValor()
        {
            // arrange
            input("198402018831");
            output("84.02.01883-1");

            // act & assert
            ActionMask(prmLista: "COD_MATRICULA: ##.##.#####-#", prmChave: "COD_MATRICULA");

        }

        [TestMethod()]
        public void TST050_Mask_MascarasMultiplas()
        {
            // arrange
            input("14029092845");
            output("140.290.928-45");

            // act & assert
            ActionMask(prmLista: "COD_MATRICULA: ####.##.#####-#, CPF: ###.###.###-##, CNPJ: ###.###.###-##", prmChave: "CPF");

        }

        [TestMethod()]
        public void TST060_Mask_MascaraVazia()
        {
            // arrange
            input("14029092845");
            output( "14029092845");

            // act & assert
            ActionMask(prmLista: "", prmChave: "CPF");

        }

        [TestMethod()]
        public void TST070_Mask_KeyInexistente()
        {
            // arrange
            input("14029092845");
            output("14029092845");

            // act & assert
            ActionMask(prmLista: "COD_MATRICULA: ####.##.#####-#, CPF: ###.###.###-##, CNPJ: ###.###.###-##", prmChave: "RG");

        }
        private void ActionMask(string prmLista, string prmChave)
        {

            AssertTest(prmResult: new myMasks(prmLista).GetFormat(prmChave, Input.txt));

        }

    }
}
