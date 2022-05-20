using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{

    public class myBrickColchetes : myBrickGenerico
    {
        public myBrickColchetes() : base(prmDelimitadorInicial: "[", prmDelimitadorFinal: "]", prmConector: "=") { }

        public myBrickColchetes(string prmConector) : base(prmDelimitadorInicial: "[", prmDelimitadorFinal: "]", prmConector) { }
    }

    public class myBrickChaves : myBrickGenerico
    {
        public myBrickChaves() : base(prmDelimitadorInicial: "{", prmDelimitadorFinal: "}", prmConector: "=") { }
    }

    public class myBrickParenteses : myBrickGenerico
    {
        public myBrickParenteses() : base(prmDelimitadorInicial: "(", prmDelimitadorFinal: ")", prmConector: "@") { }
    }

    public class myBrickTags : myBrickGenerico
    {
        public myBrickTags() : base(prmDelimitadorInicial: "<", prmDelimitadorFinal: ">", prmConector: "|") { }
    }

    public class myBrickGenerico
    {

        private string delimitadorInicial;
        private string delimitadorFinal;

        private string conector;

        public myBrickGenerico(string prmDelimitadorInicial, string prmDelimitadorFinal, string prmConector)
        {
            delimitadorInicial = prmDelimitadorInicial;
            delimitadorFinal = prmDelimitadorFinal;

            conector = prmConector;
        }

        public bool HasSpot(string prmText) => myString.IsFull(GetSpot(prmText));

        public string GetExtract(string prmText, bool prmIsMain) { if (prmIsMain) return GetMain(prmText); return GetSpot(prmText); }

        public string GetMain(string prmText) => myBrick.GetmyBrickRemove(prmText, delimitadorInicial, delimitadorFinal);
        public string GetSpot(string prmText) => myBrick.GetmyBrick(prmText, delimitadorInicial, delimitadorFinal).Trim();


        public string GetPrefixo(string prmText) => GetPrefixo(prmText, prmTRIM: false);
        public string GetPrefixo(string prmText, bool prmTRIM) => myBrick.GetmyBrickAntes(prmText, delimitadorInicial, prmTRIM);

        public string GetSufixo(string prmText) => GetSufixo(prmText, prmTRIM: false);
        public string GetSufixo(string prmText, bool prmTRIM) => myBrick.GetmyBrickDepois(prmText, delimitadorFinal, prmTRIM);

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
    public static class myBrick
    {

        public static string GetmyBrick(string prmTexto, string prmDelimitador) => GetmyBrick(prmTexto, prmDelimitador, prmDelimitador);
        public static string GetmyBrick(string prmTexto, string prmDelimitador, bool prmPreserve) => GetmyBrick(prmTexto, prmDelimitador, prmDelimitador, prmPreserve);
        public static string GetmyBrick(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal) => GetmyBrick(prmTexto, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: false);
        public static string GetmyBrick(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmPreserve)
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

        public static string GetmyBrickRemove(string prmText, string prmDelimitadorInicial, string prmDelimitadorFinal) => GetmyBrickRemove(prmText, prmDelimitadorInicial, prmDelimitadorFinal, prmTRIM: false);
        public static string GetmyBrickRemove(string prmText, string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmTRIM)
        {
            string retorno = GetmyBrick(prmText, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: true);

            string parte_inicial = myBrick.GetmyBrickAntes(prmText, retorno);
            string parte_final = myBrick.GetmyBrickDepois(prmText, retorno);

            if (prmTRIM)
                return (parte_inicial.Trim() + " " + parte_final.Trim()).Trim();

            return (parte_inicial + parte_final);
        }

        public static string GetmyBrickAntes(string prmTexto, string prmDelimitador) => GetmyBrickAntes(prmTexto, prmDelimitador, prmTRIM: false);
        public static string GetmyBrickAntes(string prmTexto, string prmDelimitador, bool prmTRIM)
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
        public static string GetmyBrickDepois(string prmTexto, string prmDelimitador) => GetmyBrickDepois(prmTexto, prmDelimitador, prmTRIM: false);
        public static string GetmyBrickDepois(string prmTexto, string prmDelimitador, bool prmTRIM)
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
        public static string GetmyBrickTroca(string prmTexto, string prmDelimitador, string prmDelimitadorNovo) => GetmyBrickTroca(prmTexto, prmDelimitador, prmDelimitador, prmDelimitadorNovo);
        public static string GetmyBrickTroca(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, string prmDelimitadorNovo) => GetmyBrickTroca(prmTexto, prmDelimitadorInicial, prmDelimitadorFinal, prmDelimitadorNovo, prmDelimitadorNovo);
        public static string GetmyBrickTroca(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, string prmDelimitadorInicialNovo, string prmDelimitadorFinalNovo)
        {

            string texto = prmTexto;

            while (true)
            {

                string myBrick = GetmyBrick(texto, prmDelimitadorInicial, prmDelimitadorFinal);

                if (myBrick == "")
                    break;

                string trecho_velho = prmDelimitadorInicial + myBrick + prmDelimitadorFinal;

                string trecho_novo = prmDelimitadorInicialNovo + myBrick + prmDelimitadorFinalNovo;

                texto = texto.Replace(trecho_velho, trecho_novo);

            }

            return (texto);

        }

    }
    public static class myBrickPrefixo
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
                    retorno = myBrick.GetmyBrick(prmTexto, prmPrefixo, prmDelimitador);
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