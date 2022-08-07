using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Katty.Tools.Test.LIB.FILES
{
    [TestClass()]
    public class Diretorio_Test : Files_Test
    {

        [TestMethod()]
        public void TST_Diretorio_010_DiretorioINI()
        {

            input = @"INI\";
            output = "ArqDadosAtendimentoAoAluno.ini";

            // act & assert
            ActionDiretorio(input);

        }

        [TestMethod()]
        public void TST_Diretorio_020_DiretorioTST()
        {

            input = @"TST\";
            output = "ArqDadosAtendimentoAoAluno.ini,ArqDadosAtendimentoAoAluno.txt,ArqDadosConsultaCursos.txt,LoginAdm.txt";

            // act & assert
            ActionDiretorio(input);

        }

        [TestMethod()]
        public void TST_Diretorio_030_FiltroTXT()
        {

            input = @"TST\";
            output = "ArqDadosAtendimentoAoAluno.txt,ArqDadosConsultaCursos.txt,LoginAdm.txt";

            // act & assert
            ActionDiretorio(input, prmFiltro: "*.txt");

        }

        [TestMethod()]
        public void TST_Diretorio_040_DiretorioInexistente()
        {

            input = @"TXT\";
            output = "";

            // act & assert
            ActionDiretorio(input, prmFiltro: "*.txt");

        }

        private void ActionDiretorio(string prmPath) => ActionDiretorio(prmPath, prmFiltro: "*.*");
        private void ActionDiretorio(string prmPath, string prmFiltro)
        {

            dir = GetDiretorio(prmPath);

            // assert
            result = dir.files.GetFiltro(prmFiltro).lista;

            // assert
            ActionGeneric();

        }

    }

    [TestClass()]
    public class Arquivos_Test : Files_Test
    {

        [TestMethod()]
        public void TST_Arquivos_010_DiretorioINI()
        {

            input = @"INI\";
            output = "#24";

            // act & assert
            ActionArquivos(input);

        }

        [TestMethod()]
        public void TST_Arquivos_020_DiretorioTST()
        {

            input = @"TST\";
            output = "#24,#2,#4,#2";

            // act & assert
            ActionArquivos(input);

        }

        [TestMethod()]
        public void TST_Arquivos_030_FiltroTXT()
        {

            input = @"TST";
            output = "#2,#4,#2";

            // act & assert
            ActionArquivos(input, prmFiltro: "*.txt");

        }

        [TestMethod()]
        public void TST_Arquivos_040_DiretorioInexistente()
        {

            input = @"TXT\";
            output = "";

            // act & assert
            ActionArquivos(input, prmFiltro: "*.txt");

        }

        private void ActionArquivos(string prmPath) => ActionArquivos(prmPath, prmFiltro: "*.*");
        private void ActionArquivos(string prmPath, string prmFiltro)
        {

            dir = GetDiretorio(prmPath);

            // assert
            result = dir.files.GetFiltro(prmFiltro).qtde_linhas;

            // assert
            ActionGeneric();

        }

    }

    public class Files_Test
    {

        public string input;
        public string output;
        public string result;

        public Diretorio dir;

        public Diretorio GetDiretorio(string prmPath) => new Diretorio(GetPath(prmPath));

        public string GetPath(string prmPath) => Environment.CurrentDirectory + @"\..\..\..\Source\DATA\TestData\" + prmPath;

        public void ActionGeneric()
        {

            // assert
            if (output != result)
                Assert.Fail(string.Format("Actual: <{0}>, Expected: <{1}>", result, output));

        }

    }

}
