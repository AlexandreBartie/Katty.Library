using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myTupla
    {
        private string conector = "=";
        private string conector_interno = ":";

        private string _name;
        private string _value;

        private string _alias;
        private string _format;

        public string name => _name;
        public string alias => _alias;

        private string format => _format;
        public string value => _value;
        public string valueEx => GetValueEx();

        public string name_sql => GetSQL();
        public string var_sql => String.Format("#({0})", name);
        public string value_sql => GetValue();
        public string mask => GetMask();
        public string log => GetLog();

        public bool TemKey => myString.IsFull(name);
        public bool TemValue => !IsNull;
        public bool TemVariavel => TemValue || TemAlias;
        private bool TemDados => TemKey & TemValue;

        private bool TemAlias => (alias != "");
        private bool TemFormat => (format != "");

        private bool TemDetalhe => TemAlias || TemFormat;
        public bool IsNull => myString.IsNull(value);

        public bool IsMatch(string prmName) => (TemKey && myString.IsMatch(name, prmName));
        public bool IsAlias(string prmName) => myString.IsMatch(GetAlias(), prmName);
        public bool IsMatchEx(string prmName) => IsMatch(prmName) || IsAlias(prmName);

        public myTupla(string prmTexto)
        {
            Parse(prmTexto);
        }
        public myTupla(string prmTexto, string prmConector)
        {
            conector = prmConector;

            Parse(prmTexto);
        }

        public void Parse(string prmTexto) { ParseDefinicao(prmTexto); ParseDetalhamento(prmTexto); }

        private void ParseDefinicao(string prmTexto)
        {

            BlocoColchetes Bloco = new BlocoColchetes(conector);

            //
            // Get DEFINICAO da Tupla (isolar "name" <conector> "valor")
            //

            string definicao = Bloco.GetRemove(prmTexto);

            //
            // Identifica "name" e "valor"
            //

            _name = Bloco.GetPrefixoConector(definicao);

            _value = Bloco.GetSufixoConector(definicao, prmNull: true);
        }

        private void ParseDetalhamento(string prmTexto)
        {

            BlocoColchetes Bloco = new BlocoColchetes(conector_interno);

            //
            // Get DETALHE Tupla (estão entre os delimitadores '[' <conector> ']' )
            //

            string detalhe = Bloco.GetParametro(prmTexto);

            //
            // Identifica "alias" e "format"
            //

            _alias = Bloco.GetPrefixoConector(detalhe);

            _format = Bloco.GetSufixoConector(detalhe);

        }

        public bool SetValue(string prmValue) { _value = prmValue; return true; }

        public bool SetValue(myTupla prmTupla)
        {
            if (prmTupla.IsMatch(name))
                return SetValue(prmTupla.value);

            return (false);
        }

        private string GetLog()
        {
            string log = "";

            if (TemKey)
                log += name + @":= '" + value + "'";

            if (TemDetalhe)
                log += " [" + GetDetalhe() + "]";

            return log;
        }

        private string GetSQL()
        {
            if (TemVariavel)
                return string.Format("{0} as {1}", var_sql, name);

            return name;
        }

        private string GetValue()
        {
            if (TemValue) return value;

            if (TemAlias) return alias;

            return "''";
        }

        private string GetValueEx()
        {
            if (TemValue)
                return value;

            return format;
        }
        private string GetMask()
        {
            if (TemFormat) return string.Format("{0} = {1}", name, format);

            return "";
        }

        private string GetDetalhe()
        {
            string txt = "";

            if (TemDetalhe)
            {
                if (TemAlias && TemFormat)
                    txt = alias + ":" + format;
                else if (TemAlias)
                    txt = alias;
                else
                    txt = format;
            }
            return txt;
        }

        private string GetAlias()
        {
            if (TemAlias)
                return alias;

            return name;
        }

    }

    public class myTuplas : List<myTupla>
    {

        private string separador = ",";

        private string conector = "=";

        public string key;

        public string group;

        public int qtde => this.Count;
        public bool IsFull => !IsEmpty;
        public bool IsEmpty => (Count == 0);

        public string log { get => GetLOG(); }
        public string sql { get => GetSQL(); }
        public string mask { get => GetMask(); }

        public string names { get => GetTXT(); }

        public bool IsMatch(string prmKey) => (myString.IsMatch(key, prmKey));
        public bool IsGroup(string prmGroup) => (myString.IsMatch(group, prmGroup));

        public myTuplas()
        {
        }
        public myTuplas(string prmTuplas)
        {
            Parse(prmTuplas);
        }
        public myTuplas(string prmTuplas, string prmSeparador)
        {
            Parse(prmTuplas, prmSeparador);
        }
        public myTuplas(string prmTuplas, string prmSeparador, string prmConector)
        {
            Parse(prmTuplas, prmSeparador, prmConector);
        }
        public myTuplas(myTuplas prmTuplas)
        {
            Parse(prmTuplas);
        }
        public myTuplas SetKey(string prmKey, string prmGroup) { key = prmKey; group = prmGroup; return this; }

        public myTuplas SetSeparador(string prmSeparador) => SetSintaxe(prmSeparador, prmConector: conector);
        public myTuplas SetConector(string prmConector) => SetSintaxe(prmSeparador: separador, prmConector);
        public myTuplas SetSintaxe(string prmSeparador, string prmConector) { separador = prmSeparador; conector = prmConector; return this; }

        public myTuplas Parse(string prmLista) => Parse(prmLista, prmSeparador: separador);
        public myTuplas Parse(string prmLista, string prmSeparador) => Parse(prmLista, prmSeparador, prmConector: conector);
        public myTuplas Parse(string prmLista, string prmSeparador, string prmConector) { SetSintaxe(prmSeparador, prmConector); return Import(prmLista); }

        private myTuplas Import(string prmLista)
        {
            if (myString.IsFull(prmLista))
            {
                foreach (string item in new xLista(prmLista, separador))
                    AddTupla(new myTupla(item, conector));
            }
            return (this);
        }
        private myTuplas Parse(myTuplas prmTuplas)
        {
            foreach (myTupla tupla in prmTuplas)
                AddTupla(tupla);

            return (this);
        }
        public void AddTupla(myTupla prmTupla)
        {
            if (prmTupla.TemKey)

                if (!SetValue(prmTupla))
                    this.Add(prmTupla);
        }

        public void SetValues(string prmValues)
        {

            if (myString.IsFull(prmValues))
            {
                int cont = 0;
                foreach (string item in new xLista(prmValues, separador))
                {
                    if (this.Count == cont) break;

                    this[cont].SetValue(item);

                    cont++;
                }

            }

        }
        public bool SetValue(myTupla prmTupla)
        {
            if (prmTupla.TemKey)
            {

                foreach (myTupla Tupla in this)
                {
                    if (Tupla.SetValue(prmTupla))
                        return true;
                }

            }
            return (false);
        }
        public string GetValue(string prmName) => GetValue(prmName, prmPadrao: "");
        public string GetValue(string prmName, string prmPadrao)
        {
            foreach (myTupla Tupla in this)
            {
                if (Tupla.IsMatchEx(prmName))
                    return Tupla.valueEx;
            }
            return (prmPadrao);
        }
        
        public myTuplas GetVariavel()
        {
            myTuplas tuplas = new myTuplas().SetSintaxe(separador, conector);

            foreach (myTupla tupla in this)
            {
                if (tupla.TemVariavel)
                    tuplas.Add(tupla);
            }
            return tuplas;
        }
        public bool IsFind(string prmName)
        {
            foreach (myTupla tupla in this)
            {
                if (tupla.IsMatch(prmName))
                    return true;
            }

            return (false);
        }

        private string GetTXT()
        {
            xLista text = new xLista();

            foreach (myTupla tupla in this)
            {
                text.Add(tupla.name);
            }
            return (text.txt);
        }
        private string GetSQL()
        {
            xLista text = new xLista();

            foreach (myTupla tupla in this)
            {
                text.Add(tupla.name_sql);
            }
            return (text.csv);
        }
        private string GetLOG()
        {
            xMemo text = new xMemo();

            foreach (myTupla tupla in this)
                text.Add(tupla.log);

            return (text.csv);
        }
        private string GetMask()
        {
            xMemo text = new xMemo();

            foreach (myTupla tupla in this)
            {
                text.Add(tupla.mask);
            }
            return (text.csv);
        }

    }

    public class myTuplasBox : List<myTuplas>
    {

        public string name;

        public string header => name + "," + columns;
        public string columns => GetNames();
        public string columns_sql => GetSQL();
        public string mask => GetMask();
        public int qtde => GetQtde();

        public myTuplas AddItem(string prmKey) => AddItem(prmKey, prmGroup: "main");
        public myTuplas AddItem(string prmKey, string prmGroup) => AddItem(new myTuplas().SetKey(prmKey, prmGroup));
        public myTuplas AddItem(myTuplas Tuplas)
        {
            Add(Tuplas); return Tuplas;
        }

        public myTuplasBox Main => Filter(prmGroup: "main");
        public myTuplasBox Filter(string prmGroup)
        {
            myTuplasBox Box = new myTuplasBox();

            foreach (myTuplas Tuplas in this)
            {
                if (Tuplas.IsGroup(prmGroup))
                    Box.Add(Tuplas);
            }
            return Box;
        }
        public void SetValues(string prmValues)
        {
            myTuplas Inputs = new myTuplas(prmValues);
            
            foreach (myTupla tupla in Inputs)
                SetValue(tupla);
        }
        private bool SetValue(myTupla prmTupla)
        {
            foreach (myTuplas Tuplas in this)
                if (Tuplas.SetValue(prmTupla)) return true;

            return false;
        }
        private string GetNames()
        {
            xLista text = new xMemo();

            foreach (myTuplas Tuplas in this)
                if (Tuplas.IsFull)
                    text.Add(Tuplas.names);

            return text.txt;
        }
        private string GetSQL()
        {
            xLista text = new xMemo();

            foreach (myTuplas Tuplas in this)
                if (Tuplas.IsFull)
                    text.Add(Tuplas.sql);

            return text.csv;
        }
        private string GetMask()
        {
            xLista text = new xMemo();

            foreach (myTuplas Tuplas in this)
                if (Tuplas.IsFull)
                    text.Add(Tuplas.mask);

            return text.csv;
        }

        private int GetQtde()
        {
            int qtde = 0;

            foreach (myTuplas Tuplas in this)
                if (Tuplas.IsFull)
                    qtde += Tuplas.qtde;

            return qtde;
        }

    }

}
