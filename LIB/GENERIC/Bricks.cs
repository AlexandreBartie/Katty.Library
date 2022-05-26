using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{

    public class myBrickColchetes : myBrickGeneric
    {
        public myBrickColchetes() : base(prmDelimitadorInicial: "[", prmDelimitadorFinal: "]", prmConector: "=") { }
    }

    public class myBrickChaves : myBrickGeneric
    {
        public myBrickChaves() : base(prmDelimitadorInicial: "{", prmDelimitadorFinal: "}", prmConector: "=") { }
    }

    public class myBrickParenteses : myBrickGeneric
    {
        public myBrickParenteses() : base(prmDelimitadorInicial: "(", prmDelimitadorFinal: ")", prmConector: "@") { }
    }

    public class myBrickTags : myBrickGeneric
    {
        public myBrickTags() : base(prmDelimitadorInicial: "<", prmDelimitadorFinal: ">", prmConector: "|") { }
    }

    public class myBrickGeneric
    {

        private string delimitadorInicial;
        private string delimitadorFinal;

        private string conector;

        public myBrickGeneric(string prmDelimitadorInicial, string prmDelimitadorFinal, string prmConector)
        {
            delimitadorInicial = prmDelimitadorInicial;
            delimitadorFinal = prmDelimitadorFinal;

            conector = prmConector;
        }
        public void SetConector(string prmConector) { conector = prmConector; }

        public bool HasSpot(string prmText) => myString.IsFull(GetSpot(prmText));

        public string GetExtract(string prmText, bool prmIsMain) { if (prmIsMain) return GetMain(prmText); return GetSpot(prmText); }

        public string GetMain(string prmText) => myBrick.Remove(prmText, delimitadorInicial, delimitadorFinal).Trim();

        public string GetSpot(string prmText) => GetSpot(prmText, prmPreserve: false);
        public string GetSpot(string prmText, bool prmPreserve) => myBrick.Get(prmText, delimitadorInicial, delimitadorFinal, prmPreserve).Trim();


        public string GetPrefixo(string prmText) => GetPrefixo(prmText, prmTRIM: false);
        public string GetPrefixo(string prmText, bool prmTRIM) => myBrick.GetBefore(prmText, delimitadorInicial, prmTRIM);

        public string GetSufixo(string prmText) => GetSufixo(prmText, prmTRIM: false);
        public string GetSufixo(string prmText, bool prmTRIM) => myBrick.GetAfter(prmText, delimitadorFinal, prmTRIM);

        public string GetPrefixoConector(string prmText) => GetPrefixoConector(prmText, conector);
        public string GetPrefixoConector(string prmText, string prmConector) => myString.GetFirst(prmText, prmConector).Trim();

        public string GetSufixoConector(string prmText) => GetSufixoConector(prmText, prmNull: false);
        public string GetSufixoConector(string prmText, bool prmNull) => GetSufixoConector(prmText, conector, prmNull);
        public string GetSufixoConector(string prmText, string prmConector) => GetSufixoConector(prmText, prmConector, prmNull: false);
        public string GetSufixoConector(string prmText, string prmConector, bool prmNull)
        {
            if (prmNull && !myString.GetFind(prmText, prmConector))
                return null;

            return myString.GetLast(prmText, prmConector).Trim();
        }

    }
    public static class myBrick
    {

        public static string Get(string prmTexto, string prmDelimitador) => Get(prmTexto, prmDelimitador, prmDelimitador);
        public static string Get(string prmTexto, string prmDelimitador, bool prmPreserve) => Get(prmTexto, prmDelimitador, prmDelimitador, prmPreserve);
        public static string Get(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal) => Get(prmTexto, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: false);
        public static string Get(string prmTexto, string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmPreserve)
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

        public static string Remove(string prmText, string prmDelimitadorInicial, string prmDelimitadorFinal) => Remove(prmText, prmDelimitadorInicial, prmDelimitadorFinal, prmTRIM: false);
        public static string Remove(string prmText, string prmDelimitadorInicial, string prmDelimitadorFinal, bool prmTRIM)
        {
            string retorno = Get(prmText, prmDelimitadorInicial, prmDelimitadorFinal, prmPreserve: true);

            string parte_inicial = myBrick.GetBefore(prmText, retorno);
            string parte_final = myBrick.GetAfter(prmText, retorno);

            if (prmTRIM)
                return (parte_inicial.Trim() + " " + parte_final.Trim()).Trim();

            return (parte_inicial + parte_final);
        }

        public static string GetBefore(string prmTexto, string prmDelimitador) => GetBefore(prmTexto, prmDelimitador, prmTRIM: false);
        public static string GetBefore(string prmTexto, string prmDelimitador, bool prmTRIM)
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
        public static string GetAfter(string prmTexto, string prmDelimitador) => GetAfter(prmTexto, prmDelimitador, prmTRIM: false);
        public static string GetAfter(string prmTexto, string prmDelimitador, bool prmTRIM)
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

        public static string GetChange(string prmTexto, string prmDelimitador, string prmDelimitadorNovo)
        {

            string texto = prmTexto;

            while (true)
            {

                string myBrick = Get(texto, prmDelimitador);

                if (myBrick == "")
                    break;

                string trecho_now = prmDelimitador + myBrick + prmDelimitador;

                string trecho_new = prmDelimitadorNovo + myBrick + prmDelimitadorNovo;

                texto = texto.Replace(trecho_now, trecho_new);

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
                    retorno = myBrick.Get(prmTexto, prmPrefixo, prmDelimitador);
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