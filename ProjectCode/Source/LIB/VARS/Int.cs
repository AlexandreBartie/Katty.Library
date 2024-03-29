﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public static class myInt
    {

        public static int IIf(bool prmCondicao, int prmValorPOS, int prmValorNEG)
        {

            if (prmCondicao)
                return (prmValorPOS);

            return (prmValorNEG);

        }

        public static bool IsPositivo(int prmValor)
        {

            if (prmValor >= 0)
                return (true);

            return (false);

        }

        public static bool IsZero(int prmValor) => (prmValor == 0);
        public static bool IsNoZero(int prmValor) => (!IsZero(prmValor));

        public static int GetInverter(bool prmCondicao, int prmValor)
        {

            if (prmCondicao)
                return (prmValor);

            return (-prmValor);

        }
        public static int GetNumero(string prmValor) => GetNumero(prmValor, prmPadrao: -1);
        public static int GetNumero(string prmValor, int prmPadrao)
        {
            try
            {
                if (IsNumero(prmValor))
                    return (Convert.ToInt32(prmValor));
            }
            catch
            { }
            return (prmPadrao);

        }
        public static bool IsNumero(string prmValor)
        {
            try
            {
                if (myString.IsFull(prmValor))
                    { int x = (Convert.ToInt32(prmValor)); return true;  }

            }
            catch
            { }
            return false;

        }
        public static int GetPositivo(int prmValor) => (System.Math.Abs(prmValor));

        public static int GetNegativo(int prmValor) => (-GetPositivo(prmValor));

        public static int GetMaior(int prmValorA, int prmValorB)
        {
            if (prmValorA > prmValorB)
                return (prmValorA);

            return (prmValorB);
        }
        public static int GetIntervalo(int prmValor, int prmMinimo, int prmMaximo)
        {

            if (prmValor > prmMaximo)
                return (prmMaximo);

            if (prmValor < prmMinimo)
                return (prmMinimo);

            return (prmValor);

        }
        public static bool IsIntervalo(int prmValor, int prmMinimo, int prmMaximo)
        {
            return ((prmMinimo <= prmValor) && (prmValor <= prmMaximo));
        }

    }
}
