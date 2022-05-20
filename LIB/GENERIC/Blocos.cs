using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{

    public class BlocoColchetes : BlocoGenerico
    {
        public BlocoColchetes() : base(prmDelimitadorInicial: "[", prmDelimitadorFinal: "]", prmConector: "=") { }

        public BlocoColchetes(string prmConector) : base(prmDelimitadorInicial: "[", prmDelimitadorFinal: "]", prmConector) { }
    }

    public class BlocoChaves : BlocoGenerico
    {
        public BlocoChaves() : base(prmDelimitadorInicial: "{", prmDelimitadorFinal: "}", prmConector: "=") { }
    }

    public class BlocoParenteses : BlocoGenerico
    {
        public BlocoParenteses() : base(prmDelimitadorInicial: "(", prmDelimitadorFinal: ")", prmConector: "@") { }
    }

    public class BlocoTags : BlocoGenerico
    {
        public BlocoTags() : base(prmDelimitadorInicial: "<", prmDelimitadorFinal: ">", prmConector: "|") { }
    }

    public class BlocoGenerico
    {

        private string delimitadorInicial;
        private string delimitadorFinal;

        private string conector;

        public BlocoGenerico(string prmDelimitadorInicial, string prmDelimitadorFinal, string prmConector)
        {
            delimitadorInicial = prmDelimitadorInicial;
            delimitadorFinal = prmDelimitadorFinal;

            conector = prmConector;
        }

        public bool HasSpot(string prmText) => myString.IsFull(GetSpot(prmText));

        public string GetExtract(string prmText, bool prmIsMain) { if (prmIsMain) return GetMain(prmText); return GetSpot(prmText); }

        public string GetMain(string prmText) => Bloco.GetBlocoRemove(prmText, delimitadorInicial, delimitadorFinal);
        public string GetSpot(string prmText) => Bloco.GetBloco(prmText, delimitadorInicial, delimitadorFinal).Trim();


        public string GetPrefixo(string prmText) => GetPrefixo(prmText, prmTRIM: false);
        public string GetPrefixo(string prmText, bool prmTRIM) => Bloco.GetBlocoAntes(prmText, delimitadorInicial, prmTRIM);

        public string GetSufixo(string prmText) => GetSufixo(prmText, prmTRIM: false);
        public string GetSufixo(string prmText, bool prmTRIM) => Bloco.GetBlocoDepois(prmText, delimitadorFinal, prmTRIM);

        public string GetPrefixoConector(string prmTexto) => GetPrefixoConector(prmTexto, conector);
        public string GetPrefixoConector(string prmTexto, string prmConector) => myString.GetFirst(prmTexto, prmConector).Trim();

        public string GetSufixoConector(string prmTexto) => GetSufixoConector(prmTexto, prmNull: false);
        public string GetSufixoConector(string prmTexto, bool prmNull) => GetSufixoConector(prmTexto, conector, prmNull);
        public string GetSufixoConector(string prmTexto, string prmConector) => GetSufixoConector(prmTexto, prmConector, prmNull: false);
        public string GetSufixoConector(string prmTexto, string prmConector, bool prmNull)
        {
            if (prmNull && !myString.GetFind(prmTexto, prmConector))
                return null;

            return myString.GetLast(prmTexto, prmConector).Trim();
        }
    }
    public static class Bloco
    {

        public static string GetBloco(string prmTexto, string prmDelimitador) => GetBloco(prmTexto, prmDelimitador, prmDelimitador);
        public static string GetBloco(string prmTexto, string prmDelimitador, bool prmPreserve) => GetBloco(prmTexto, prmDelimitador, prmDelimitador, prmPreserve);
        public static string GetBloco(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal) => GetBloco(prmTexto, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: false);
        public static string GetBloco(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmPreserve)
        {

            string retorno = "";

            if (myString.IsFull(prmTexto))
            {

                int inicio = prmTexto.IndexOf(prmDelimitadorInicial);

                int limite = inicio + prmDelimitadorInicial.Length;

                int final = prmTexto.IndexOf(prmDelimitadorFinal, limite);

                if ((inicio >= 0) & (final >= inicio))
                    retorno = (prmTexto.Substring(limite, final - limite));

            }

            if (prmPreserve)
                retorno = prmDelimitadorInicial + retorno + prmDelimitadorFinal;

            return (retorno);

        }

        public static string GetBlocoRemove(string prmText, string prmDelimitadorInicial, string prmDelimitadorFinal) => GetBlocoRemove(prmText, prmDelimitadorInicial, prmDelimitadorFinal, prmTRIM: false);
        public static string GetBlocoRemove(string prmText, string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmTRIM)
        {
            string retorno = GetBloco(prmText, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: true);

            string parte_inicial = Bloco.GetBlocoAntes(prmText, retorno);
            string parte_final = Bloco.GetBlocoDepois(prmText, retorno);

            if (prmTRIM)
                return (parte_inicial.Trim() + " " + parte_final.Trim()).Trim();

            return (parte_inicial + parte_final);
        }

        public static string GetBlocoAntes(string prmTexto, string prmDelimitador) => GetBlocoAntes(prmTexto, prmDelimitador, prmTRIM: false);
        public static string GetBlocoAntes(string prmTexto, string prmDelimitador, bool prmTRIM)
        {

            if (myString.IsFull(prmTexto))
            {

                string retorno = prmTexto;

                if (myString.IsFull(prmDelimitador))
                {

                    int indice = prmTexto.IndexOf(prmDelimitador);

                    if (indice != -1)
                        retorno = (myString.GetFirst(prmTexto, prmTamanho: indice));
                }

                if (prmTRIM)
                    retorno = retorno.Trim();

                return (retorno);

            }

            return ("");

        }
        public static string GetBlocoDepois(string prmTexto, string prmDelimitador) => GetBlocoDepois(prmTexto, prmDelimitador, prmTRIM: false);
        public static string GetBlocoDepois(string prmTexto, string prmDelimitador, bool prmTRIM)
        {

            if (myString.IsFull(prmTexto) && myString.IsFull(prmDelimitador))
            {

                int indice = prmTexto.IndexOf(prmDelimitador);

                if (indice != -1)
                {

                    string retorno = (myString.GetLast(prmTexto, prmTamanho: prmTexto.Length - prmDelimitador.Length - indice));

                    if (prmTRIM)
                        retorno = retorno.Trim();

                    return (retorno);
                }


            }

            return ("");

        }
        public static string GetBlocoTroca(string prmTexto, string prmDelimitador, string prmDelimitadorNovo) => GetBlocoTroca(prmTexto, prmDelimitador, prmDelimitador, prmDelimitadorNovo);
        public static string GetBlocoTroca(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, string prmDelimitadorNovo) => GetBlocoTroca(prmTexto, prmDelimitadorInicial, prmDelimitadorFinal, prmDelimitadorNovo, prmDelimitadorNovo);
        public static string GetBlocoTroca(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, string prmDelimitadorInicialNovo, string prmDelimitadorFinalNovo)
        {

            string texto = prmTexto;

            while (true)
            {

                string bloco = GetBloco(texto, prmDelimitadorInicial, prmDelimitadorFinal);

                if (bloco == "")
                    break;

                string trecho_velho = prmDelimitadorInicial + bloco + prmDelimitadorFinal;

                string trecho_novo = prmDelimitadorInicialNovo + bloco + prmDelimitadorFinalNovo;

                texto = texto.Replace(trecho_velho, trecho_novo);

            }

            return (texto);

        }

    }
    public static class BlocoPrefixo
    {
        public static bool IsPrefixo(string prmTexto, string prmPrefixo) => (myString.GetFirst(prmTexto, prmTamanho: prmPrefixo.Length) == prmPrefixo);
        public static bool IsPrefixo(string prmTexto, string prmPrefixo, string prmDelimitador) => (GetPrefixo(prmTexto, prmPrefixo, prmDelimitador) != "");

        public static string GetPrefixo(string prmTexto, string prmPrefixo) => GetPrefixo(prmTexto, prmPrefixo, prmDelimitador: "");
        public static string GetPrefixo(string prmTexto, string prmPrefixo, string prmDelimitador) => GetPrefixo(prmTexto, prmPrefixo, prmDelimitador, prmPreserve: false);
        public static string GetPrefixo(string prmTexto, string prmPrefixo, string prmDelimitador, bool prmPreserve)
        {
            string retorno = "";

            if (myString.IsFull(prmTexto) && IsPrefixo(prmTexto, prmPrefixo))
            {

                if (myString.GetFind(prmTexto, prmDelimitador))
                    retorno = Bloco.GetBloco(prmTexto, prmPrefixo, prmDelimitador);
                else
                    retorno = myString.GetLast(prmTexto, prmTamanho: -prmPrefixo.Length);

                if (prmPreserve)
                    retorno = prmPrefixo + retorno + prmDelimitador;

            }
            return (retorno.Trim());
        }

        public static string GetPrefixoRemove(string prmTexto, string prmPrefixo) => GetPrefixoRemove(prmTexto, prmPrefixo, prmDelimitador: "");
        public static string GetPrefixoRemove(string prmTexto, string prmPrefixo, bool prmTRIM) => GetPrefixoRemove(prmTexto, prmPrefixo, prmDelimitador: "", prmTRIM);
        public static string GetPrefixoRemove(string prmTexto, string prmPrefixo, string prmDelimitador) => GetPrefixoRemove(prmTexto, prmPrefixo, prmDelimitador, prmTRIM: false);
        public static string GetPrefixoRemove(string prmTexto, string prmPrefixo, string prmDelimitador, bool prmTRIM)
        {

            string prefixo;

            prefixo = GetPrefixo(prmTexto, prmPrefixo, prmDelimitador);

            prefixo = myString.GetLast(prmTexto, prmTamanho: myInt.GetNegativo(prefixo.Length + 2));

            if (prmTRIM)
                return prefixo.Trim();

            return prefixo;

        }

    }


}