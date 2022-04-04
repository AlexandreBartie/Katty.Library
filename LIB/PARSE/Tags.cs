using System;
using System.Collections.Generic;
using System.Text;

namespace Dooggy.LIBRARY
{
    public class myTag : List<myTagOption>
    {
        public myDominio Dominio;

        private string _value;

        public string value => GetValue();

        public string name => Dominio.name;
        public string txt => Dominio.log;
        public string log => string.Format("{0}: '{1}'", name, value);

        public bool IsFull => (myString.IsFull(_value));
        public bool IsPadrao => !IsFull;
        public bool IsMatch(string prmName) => Dominio.IsMatch(prmName);
 

        public myTag(myDominio prmDominio)
        {
            Parse(prmDominio);
        }
        private void Parse(myDominio prmDominio)
        {
            Dominio = prmDominio;

            foreach (string item in prmDominio.Opcoes)
                Add(new myTagOption(prmValue: item, this));
        }
        public bool IsFind(string prmName)
        {
            foreach (myTagOption Tag in this)
                if (Tag.IsMatch(prmName))
                    return true;
            return false;
        }

    public bool SetValue(string prmValue)
        {
            if (Dominio.IsFind(prmValue))
                { _value = prmValue; return true; }
            return false;
        }

        public void SetAtivado(string prmOption, bool prmAtivo)
        {
            foreach (myTagOption Option in this)
                if (Option.IsMatch(prmOption))
                { Option.SetAtivo(prmAtivo); break; }
        }

        private string GetValue()
        {
            if (IsPadrao)
                return Dominio.padrao;

            return _value;
        }

    }

    public class myTags : List<myTag>
    {

        public myTagOptions Ativos => GetAtivos();

        public string txt => GetTXT();
        public string log => GetLOG();

        public bool Add(string prmSintaxe) => Add(new myDominio(prmSintaxe));
        public bool Add(myDominio prmDominio)
        {
            if (IsFind(prmDominio.name))
                return false;

            base.Add(new myTag(prmDominio)); return true;
        }
        public void SetAtivado(string prmName, string prmOption, bool prmAtivo)
        {
            foreach (myTag Tag in this)
                if (Tag.IsMatch(prmName))
                { Tag.SetAtivado(prmOption, prmAtivo); break; }
        }

        public bool IsFind(string prmName)
        {
            foreach (myTag Tag in this)
                if (Tag.IsMatch(prmName))
                    return true;
            return false;
        }
        public myTag FindKey(string prmName)
        {
            foreach (myTag Tag in this)
                if (Tag.IsMatch(prmName))
                    return Tag;
            return null;
        }
        private myTagOptions GetAtivos()
        {
            myTagOptions itens = new myTagOptions();

            foreach (myTag Tag in this)
                foreach (myTagOption item in Tag)
                    if (item.ativo)
                        itens.Add(item);
            return itens;
        }
        private string GetTXT()
        {

            xMemo txt = new xMemo();

            foreach (myTag Tag in this)
                txt.Add(Tag.txt);

            return txt.memo;

        }
        private string GetLOG()
        {

            xMemo log = new xMemo();

            foreach (myTag Tag in this)
                log.Add(Tag.log);

            return log.memo;

        }
    }

    public class myTagOption
    {

        public myTag Tag;

        public string value;

        public bool ativo;

        private string name => Tag.Dominio.name;
        public bool IsPadrao => Tag.Dominio.IsPadrao(value);

        public bool IsMatch(string prmName, string prmValue) => IsMatchName(prmName) && IsMatchValue(prmValue);
        public bool IsMatch(string prmValue) => IsMatchValue(prmValue);

        private bool IsMatchName(string prmName) => myString.IsMatch(name, prmName);
        private bool IsMatchValue(string prmValue) => myString.IsMatch(value, prmValue);

        public myTagOption(string prmValue, myTag prmTag)
        {
            Tag = prmTag;

            value = prmValue;
        }
        public void SetAtivo(bool prmAtivo) => ativo = prmAtivo;

    }

    public class myTagOptions : List<myTagOption>
    {
        public bool IsFind(string prmTag, string prmOption)
        {
            foreach (myTagOption Option in this)
                if (Option.IsMatch(prmTag, prmOption))
                    return true;
            return false;
        }

    }
}
