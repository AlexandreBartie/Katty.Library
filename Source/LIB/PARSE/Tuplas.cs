using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myTupla
    {
        private myTuplaSintaxe Main;
        private myTuplaSintaxe Alias;

        public string name => Main.name;
        public string value => Main.value;

        public string alias => Alias.name;
        public string format => Alias.value;

        public string valueEx => GetValueEx();

        public string name_sql => GetSQL();
        public string var_sql => GetVariable();
        public string value_sql => GetValue();

        public string log => GetLog();
        public string mask => GetMask();
        public string input => GetInput();


        public bool TemKey => myString.IsFull(name);
        public bool TemValue => !IsNull;
        public bool TemVariavel => TemValue || TemAlias;
        private bool TemDados => TemKey & TemValue;

        private bool TemAlias => (alias != "");
        private bool TemFormat => (format != "");

        private bool TemDetalhe => TemAlias || TemFormat;
        public bool IsNull => myString.IsNull(value);

        public bool IsMatch(string prmName) => (Main.IsMatch(prmName));
        public bool IsMatchEx(string prmName) => Main.IsMatch(prmName) || Alias.IsMatch(prmName);

        public bool IsAlias(string prmName) => myString.IsMatch(GetAlias(), prmName);

        public myTupla(string prmTexto)
        { 
            Setup(prmTexto, prmConector: "=");
        }
        public myTupla(string prmTexto, string prmConector)
        { 
            Setup(prmTexto, prmConector);
        }
        
        private void Setup(string prmTexto, string prmConector)
        {
            Main = new myTuplaSintaxe(prmConector, prmIsMain: true);
            Alias = new myTuplaSintaxe(prmConector: ":", prmIsMain: false);

            Parse(prmTexto);
        }

        public void Parse(string prmTexto) { Main.Parse(prmTexto); Alias.Parse(prmTexto); }

        public bool SetValue(string prmValue) { Main.value = prmValue; return true; }

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

        private string GetVariable() => String.Format("#({0})", name);

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
            if (TemFormat) return string.Format("{0} : {1}", name, format);

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

        private string GetInput()
        {
            myList txt = new myList();

            if (TemKey)
                txt.Add(name);

            if (TemAlias)
                txt.Add(alias);

            if (TemFormat)
                txt.Add(format);

            return txt.Export(":");
        }

        private string GetAlias()
        {
            if (TemAlias)
                return alias;

            return name;
        }

    }
    internal class myTuplaSintaxe
    {
        private string conector;

        internal string name;
        internal string value;
        internal bool IsMain { get; }

        private myBrickColchetes myBrick;

        internal bool TemKey => myString.IsFull(name);
        internal bool IsMatch(string prmName) => (TemKey && myString.IsMatch(name, prmName));

        internal myTuplaSintaxe(string prmConector, bool prmIsMain)
        {
            conector = prmConector; IsMain = prmIsMain;
        }

        internal void Parse(string prmText)
        {
            myBrick = new myBrickColchetes();
                
            myBrick.SetConector(conector);

            //
            // Main: 'name <conector> valor' or Spot: '[name <conector> valor]'
            //

            string param = myBrick.GetExtract(prmText, IsMain);

            //
            // Identifica "name" e "valor"
            //

            name = myBrick.GetPrefixoConector(param);

            value = myBrick.GetSufixoConector(param, prmNull: IsMain);
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
        public void SetKey(string prmKey, string prmGroup) { key = prmKey; group = prmGroup; }

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
                foreach (string item in new myList(prmLista, separador))
                    AddTupla(new myTupla(item, conector));
            }
            return (this);
        }
        public myTuplas Parse(myTuplas prmTuplas)
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
                foreach (string item in new myList(prmValues, separador))
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
                    if (Tupla.IsMatch(prmTupla.name))
                    {
                        Tupla.SetValue(prmTupla.value); return true;
                    }
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
            myList text = new myList();

            foreach (myTupla tupla in this)
            {
                text.Add(tupla.name);
            }
            return (text.txt);
        }
        private string GetSQL()
        {
            myList text = new myList();

            foreach (myTupla tupla in this)
            {
                text.Add(tupla.name_sql);
            }
            return (text.csv);
        }
        private string GetLOG()
        {
            myMemo text = new myMemo();

            foreach (myTupla tupla in this)
                text.Add(tupla.log);

            return (text.csv);
        }
        private string GetMask()
        {
            myMemo text = new myMemo();

            foreach (myTupla tupla in this)
            {
                text.Add(tupla.mask);
            }
            return (text.csv);
        }

    }

}
