﻿using Dooggy;
using Dooggy.Factory;
using Dooggy.Lib.Files;
using Dooggy.Lib.Vars;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dooggy.Factory.Data
{
    public class TestDataFile
    {

        private TestDataLocal Dados;

        private TestDataFileIO File;

        public TestTrace Trace => Dados.Trace;

        private string extensao_padrao;

        public TestDataFile(TestDataLocal prmDados, string prmExtensao)
        {

            Dados = prmDados;

            File = new TestDataFileIO(this);

            extensao_padrao = prmExtensao;

        }

        public string Open(string prmNome, string prmPath) => Open(prmNome, prmPath, prmExtensao: extensao_padrao);
        public string Open(string prmNome, string prmPath, string prmExtensao) => File.Open(prmPath, GetArquivo(prmNome, prmExtensao));

        public bool Save(string prmNome, string prmPath, string prmConteudo) => Save(prmNome, prmPath, prmConteudo, prmExtensao: extensao_padrao);
        public bool Save(string prmNome, string prmPath, string prmConteudo, string prmExtensao) => Save(prmNome, prmPath, prmConteudo, prmExtensao, prmEncoding: "");
        public bool Save(string prmNome, string prmPath, string prmConteudo, string prmExtensao, string prmEncoding) => File.Save(prmPath, GetArquivo(prmNome, prmExtensao), prmConteudo, prmEncoding);

        private string GetArquivo(string prmNome, string prmExtensao) => prmNome + "." + prmExtensao;

    }

    public class TestDataFileIO
    {

        private TestDataFile DataFile;

        private FileTXT File;

        private TestDataEncoding Encode;

        public TestTrace Trace { get => DataFile.Trace; }


        public TestDataFileIO(TestDataFile prmDataFile)
        {

            DataFile = prmDataFile;

            Encode = new TestDataEncoding(prmDataFile);

        }
        public string Open(string prmPath, string prmArquivo)
        {

            File = new FileTXT();

            if (File.Open(prmPath, prmArquivo))
            {

                Trace.LogFile.DataFileOpen(prmArquivo, prmPath);

                return File.txt();

            }

            Trace.LogFile.FailDataFileOpen(prmArquivo, prmPath);

            return ("");

        }
        public bool Save(string prmPath, string prmArquivo, string prmConteudo, string prmEncoding)
        {

            File = new FileTXT();

            if (File.IsValidName(prmArquivo))
            {

                if (File.Save(prmPath, prmArquivo, prmConteudo, prmEncoding: Encode.Find(prmEncoding)))
                {

                    Trace.LogFile.DataFileSave(prmArquivo, prmPath, prmEncoding);

                    return (true);

                }

                Trace.LogFile.DataFileSave(prmArquivo, prmPath);

            }
            else
                Trace.LogFile.DataFileMute(prmArquivo, prmPath, prmEncoding);

            return (false);
        }

    }

    public class TestDataEncoding
    {

        private TestDataFile File;

        public TestTrace Trace { get => File.Trace; }

        public TestDataEncoding(TestDataFile prmFile)
        {

            File = prmFile;

        }
        public Encoding Find(string prmTipoEncoding)
        {

            string tipo = prmTipoEncoding.Trim().ToLower();

            switch (tipo)
            {
                case "":
                case "utf8":
                    return (Encoding.UTF8);

                case "utf7":
                    return (Encoding.UTF7);

                case "utf32":
                    return (Encoding.UTF32);

                case "unicode":
                    return (Encoding.Unicode);

                case "ascii":
                    return (Encoding.ASCII);

            }

            return (FindByCodePage(tipo));

        }

        private Encoding FindByCodePage(string prmEncoding)

        {

            int code_page = myInt.GetNumero(prmEncoding);

            if (code_page != -1)

            {

                foreach (EncodingInfo info in System.Text.Encoding.GetEncodings())
                {
                    if (info.CodePage == code_page)
                        return info.GetEncoding();
                }

                Encoding encode = TryInstallCodePage(prmCodePage: code_page);

                if (encode != null) return (encode);

            }

            Trace.LogFile.FailDataFileEncoding(prmEncoding);

            return (Encoding.Default);

        }

        private Encoding TryInstallCodePage(int prmCodePage)
        {

            Encoding encode = CodePagesEncodingProvider.Instance.GetEncoding(prmCodePage);

            return (encode);

        }

    }

}
