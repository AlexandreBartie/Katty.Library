using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

namespace Katty
{
    public class xLista : List<string>
    {

        public string separador = ",";

        public xLista()
        {

        }

        public xLista(string prmLista)
        {
            Parse(prmLista);
        }

        public xLista(string prmLista, string prmSeparador)
        {
            separador = prmSeparador;

            Parse(prmLista);

        }
        public virtual void Parse(string prmLista) => Parse(prmLista, separador);
        public virtual void Parse(string prmLista, string prmSeparador)
        {

            separador = prmSeparador;

            if (!myString.IsNull(prmLista))
                foreach (string item in prmLista.Split(separador))
                {
                    this.Add(item.Trim());
                }

        }
        public int qtde { get => this.Count; }

        public bool IsFull { get => !IsVazio; }

        public bool IsUnico { get => (qtde == 1); }

        public bool IsVazio { get => (qtde == 0); }

        public string first { get => Item(1); }

        public string last { get => Item(qtde); }

        public string last_exact { get { if (IsUnico) return ""; return last; } }

        public bool IsRange(int prmIndice) => ((prmIndice >= 1) && (prmIndice <= qtde));

        public string log { get => GetLOG(); }

        public bool Excluir(int prmIndice)
        {
            if (IsRange(prmIndice))
            {

                this.RemoveAt(prmIndice - 1);

                return (true);
            }

            return (false);

        }
        public void Set(int prmIndice, string prmTexto)
        {
            if (prmIndice <= qtde)
                (this[prmIndice - 1]) = prmTexto;
        }

        public string Get(int prmIndice) => Get(prmIndice, prmPadrao: "");
        public string Get(int prmIndice, string prmPadrao)
        {
            if (myInt.IsIntervalo(prmIndice, 1, qtde))
                return (this[prmIndice - 1]);
            return prmPadrao;
        }
        public void Add(int prmIndice, string prmTexto)
        {
            if (prmIndice <= qtde)
                (this[prmIndice - 1]) = prmTexto;
            else
                Add(prmTexto);
        }
        public string Item(int prmIndice)
        {
            if (!IsVazio)
                return (this[prmIndice - 1]);
            else
                return ("");
        }
        public bool IsContem(string prmTexto) => (GetContem(prmTexto) != 0);

        public string GetRemove() => GetRemove(prmIndice: 1);

        public string GetRemove(int prmIndice)
        {
            if (IsRange(prmIndice))
            {

                string valor = Item(prmIndice);

                Excluir(prmIndice);

                return (valor);
            }

            return ("");

        }
        public int GetID(string prmTexto)
        {
            return (this.IndexOf(prmTexto));
        }
        public string GetFind(string prmTexto)
        {
            foreach (string vlItem in this)
            {
                if (myString.IsStartsWith(vlItem, prmTexto))
                { return (vlItem); }
            }
            return ("");
        }
        public bool IsFind(string prmTexto)
        {
            foreach (string vlItem in this)
            {
                if (myString.IsMatch(vlItem, prmTexto))
                { return true; }
            }
            return (false);
        }

        public int GetContem(string prmTexto) => GetContain(prmTexto, prmInverter: false);
        public int GetContido(string prmTexto) => GetContain(prmTexto, prmInverter: true);
        private int GetContain(string prmTexto, bool prmInverter)
        {
            int cont = 0;

            foreach (string vlItem in this)
            {
                cont++;
                if (myString.IsContain(vlItem, prmTexto, prmInverter))
                { return (cont); }
            }
            return (0);
        }
        public string GetFindx(string prmTexto)
        {
            return GetFind(prmTexto).Replace(prmTexto, "");
        }
        private string GetLOG()
        {

            string aux = "";

            string resultado = "";

            foreach (string item in this)
            {

                resultado += aux + item.Length.ToString();

                aux = separador;

            }

            return (resultado);
        }

        public string memo => Export(prmSeparador: Environment.NewLine);
        public string txt => Export(separador);
        public string csv => Export(", ");

        public string Export(string prmSeparador)
        {
            string lista = ""; string aux = "";

            foreach (string item in this)
            {
                lista += (aux + item); aux = prmSeparador;
            }
            return lista;
        }

    }
    public class xMemo : xLista
    {
        public xMemo()
        { }
        public xMemo(string prmSeparador)
        { separador = prmSeparador; }

        public xMemo(string prmTexto, string prmSeparador)
        { separador = prmSeparador; Parse(prmTexto); }

        public string memo_ext => memo + Environment.NewLine;

    }
    public class xLinhas : xMemo
    {

        public xLinhas()
        { Setup(); }

        public xLinhas(string prmTexto)
        {

            Setup();

            Parse(prmTexto);
        
        }

        private void Setup() => separador = Environment.NewLine;

    }

}
