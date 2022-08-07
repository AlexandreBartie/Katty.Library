using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myMasks : myTuplas
    {
        public myMasks() : base() { }
        public myMasks(string prmLista) : base(prmTuplas: new myTuplas(prmLista, prmSeparador: ",", prmConector: ":")) { }
        public myMasks(myMasks prmLista) : base(prmLista) { }
 
        public myMasks SetGroup(string prmKey, string prmGroup) { SetKey(prmKey, prmGroup); return this; }

        public string GetMask(string prmKey) => GetMask(prmKey, prmPadrao: "");
        public string GetMask(string prmKey, string prmPadrao) => GetValue(prmKey, prmPadrao);

        public string GetFormat(string prmKey, string prmText) => myFormat.TextToString(prmText, GetMask(prmKey));

    }
    public class myBoxMasks : List<myMasks>
    {

        public string name;

        public string header => name + "," + columns;
        public string columns => GetNames();
        public string columns_sql => GetSQL();
        public string masks => GetMasks();
        public int qtde => GetQtde();

        public myMasks AddItem(string prmKey) => AddItem(prmKey, prmGroup: "main");
        public myMasks AddItem(string prmKey, string prmGroup) => AddItem(new myMasks().SetGroup(prmKey, prmGroup));
        public myMasks AddItem(myMasks prmMasks)
        {
            Add(prmMasks); return prmMasks;
        }

        public myBoxMasks Main => Filter(prmGroup: "main");
        public myBoxMasks Filter(string prmGroup)
        {
            myBoxMasks Box = new myBoxMasks();

            foreach (myMasks Masks in this)
            {
                if (Masks.IsGroup(prmGroup))
                    Box.Add(Masks);
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
            myList text = new myMemo();

            foreach (myTuplas Tuplas in this)
                if (Tuplas.IsFull)
                    text.Add(Tuplas.names);

            return text.txt;
        }
        private string GetSQL()
        {
            myList text = new myMemo();

            foreach (myTuplas Tuplas in this)
                if (Tuplas.IsFull)
                    text.Add(Tuplas.sql);

            return text.csv;
        }
        private string GetMasks()
        {
            myList text = new myMemo();

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
