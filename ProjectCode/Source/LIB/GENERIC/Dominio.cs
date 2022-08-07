using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myDominio
    {

        public string name;

        public myList Opcoes;

        public string padrao;

        private string name_ext => GetNameExtendido();
        public string log => GetLog();
        public string txt => Opcoes.txt;

        public bool IsMatch(string prmName) => myString.IsMatch(name, prmName);
        public bool IsPadrao(string prmValue)
        {
            if (TemPadrao)
                return myString.IsMatch(padrao, prmValue);
            return false;
        }
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

            name = new myBrickChaves().GetPrefixo(prmSintaxe, prmTRIM: true);

            opcoes = new myBrickChaves().GetSpot(prmSintaxe);

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

            name = new myBrickColchetes().GetPrefixo(prmName, prmTRIM: true);

            padrao = GetValidDefault(prmDefault: new myBrickColchetes().GetSpot(prmName));

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
        public void SetOpcoes(string prmLista) => Opcoes = new myList(prmLista);

        private string GetLog()
        {
            if (TemName)
                if (TemOpcoes)
                    return (string.Format("{0,25}: {1}", name_ext, txt));
                else
                    return name;

            return txt;
        }

        private string GetNameExtendido()
        {
            if (TemPadrao)
                return string.Format("{0}[{1}]", name, padrao);
            return name;
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
        public void AddItem(string prmName, string prmLista) => AddNew(new myDominio(prmName, prmLista));

        private void AddNew(myDominio prmItem)
        {
            string name = prmItem.name;

            if (IsFind(name))
                FindKey(name).SetOpcoes(prmItem.txt);
            else
                Add(prmItem);
        }

        public myDominio FindKey(string prmName)
        {
            foreach (myDominio item in this)
                if (item.IsMatch(prmName))
                    return item;
            return null;
        }

        public bool IsFind(string prmName)
        {
            foreach (myDominio item in this)
                if (item.IsMatch(prmName))
                    return true;
            return false;
        }
       
        private string GetLOG()
        {
            myLines memo = new myLines();

            foreach (myDominio item in this)
                memo.Add(item.log);

            return memo.txt;
        }

    }

}
