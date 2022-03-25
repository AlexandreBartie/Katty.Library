using System;
using System.Collections.Generic;
using System.Text;

namespace Dooggy.LIBRARY
{
    public class myDominio
    {

        public string name;

        public xLista Opcoes;

        public string padrao;


        public string name_ext => name + padrao_ext;

        public string padrao_ext => myBool.IIf(TemPadrao, string.Format("[{0}]", padrao));

        public string log => GetLog();
        public string txt => Opcoes.txt;

        public bool IsEqual(string prmName) => myString.IsEqual(name, prmName);

        public bool TemName => myString.IsFull(name);
        public bool TemPadrao => myString.IsFull(padrao);
        public bool TemOpcoes => Opcoes.IsFull;

        public myDominio(string prmLista)
        {
            Parse(prmLista);
        }
        
        public myDominio(string prmName, string prmLista)
        {
            Parse(prmName, prmLista);
        }

        private void Parse(string prmSintaxe)
        {
            string opcoes;

            name = new BlocoChaves().GetPrefixo(prmSintaxe, prmTRIM: true);

            opcoes = new BlocoChaves().GetParametro(prmSintaxe);

            Parse(name, opcoes);
        }

        private void Parse(string prmName, string prmLista)
        {
           // Definição da lista de opções - executado primeiro, para garantir a existência do padrão ...

            if (myString.IsFull(prmLista))
                SetOpcoes(prmLista);
            else
                SetOpcoes();

            // Definição do nome e do padrão (deve 

            name = new BlocoColchetes().GetPrefixo(prmName, prmTRIM: true);

            padrao = GetValidDefault(prmDefault: new BlocoColchetes().GetParametro(prmName));

        }

        private string GetValidDefault(string prmDefault)
        {
            if (myInt.IsNumero(prmDefault))
                return Opcoes.Get(prmIndice: myInt.GetNumero(prmDefault));

            if (Opcoes.IsFind(prmDefault))
                return prmDefault;

            return "";
        }
        
        public bool IsFind(string prmItem) => Opcoes.IsFind(prmItem);

        private void SetOpcoes() => SetOpcoes(prmLista: null);
        public void SetOpcoes(string prmLista) => Opcoes = new xLista(prmLista);

        private string GetLog()
        {
            if (TemName)
                if (TemOpcoes)
                    return (string.Format("{0}: {1}", name_ext, txt));
                else
                    return name;

            return txt;
        }

    }

    public class myDominios : List<myDominio>
    {
        public string log => GetLOG();

        public void AddItens(List<string> prmLista)
        {
            foreach (string linha in prmLista)
                AddItem(linha);
        }
        public void AddItem(string prmLista) => AddNew(new myDominio(prmLista));
        public void AddItem(string prmKey, string prmLista) => AddNew(new myDominio(prmKey, prmLista));

        private void AddNew(myDominio prmItem)
        {
            string name = prmItem.name;

            if (IsFind(name))
                FindKey(name).SetOpcoes(prmItem.txt);
            else
                Add(prmItem);
        }

        public myDominio FindKey(string prmKey)
        {
            foreach (myDominio item in this)
                if (item.IsEqual(prmKey))
                    return item;
            return null;
        }

        public bool IsFind(string prmKey)
        {
            foreach (myDominio item in this)
                if (item.IsEqual(prmKey))
                    return true;
            return false;
        }

        private string GetLOG()
        {
            xLinhas memo = new xLinhas();

            foreach (myDominio item in this)
                memo.Add(item.log);

            return memo.txt;
        }

    }

}
