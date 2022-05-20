using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class DataMatrix
    {
        public DataViews Views { get; }

        public string log => GetLog();

        public DataMatrix()
        {
            Views = new DataViews();
        }

        public DataMatrix(string prmList)
        {
            Views = new DataViews().Add(prmList);
        }

        public void AddViews(string prmLista) => Views.Add(prmLista);

        private string GetLog()
        {
            myLines memo = new myLines();

            foreach (DataView View in Views)
                memo.Add(View.name);

            return memo.memo;
        }

    }

    public class DataViews : myCollection<DataView>
    {
        public string log => GetLog();

        public DataViews Add(string prmLista)
        {
            foreach (string name in new myList(prmLista))
                Add(new DataView(name));

            return this;
        }

        private string GetLog()
        {
            myLines memo = new myLines();

            foreach (DataView View in this)
                memo.Add(View.log);

            return memo.memo;
        }

    }
    public class DataInputs : myCollection<DataInput>
    {
        public DataView View { get; }
     
        public string log => GetLog();

        public DataInputs(DataView prmView)
        { View = prmView; }

        public DataInputs Add(myTuplas prmTuplas)
        {
            foreach (myTupla Tupla in prmTuplas)
                Add(new DataInput(View, Tupla));

            return this;
        }

        private string GetLog()
        {
            myList list = new myList();

            foreach (DataInput Input in this)
                list.Add(Input.log);

            return list.csv;
        }

    }
    public class DataFlows : myCollection<DataFlow>
    {
        public DataView View { get; }

        public string log => GetLog();

        public DataFlows(DataView prmView)
        { View = prmView; }

        public DataFlows Add(string prmFlows)
        {
            foreach (string line in new myLines(prmFlows))
                Add(new myTuplas(line));

            return this;
        }
        public DataFlows Add(myTuplas prmFlows)
        {
            Add(new DataFlow(View, prmFlows));

            return this;
        }
        private string GetLog()
        {
            myMemo txt = new myMemo();

            foreach (DataFlow Flow in this)
                txt.Add(Flow.log);

            return txt.memo;
        }
    }

    public class DataView
    {

        public string name { get; }

        public string log => GetLog();

        public void AddInputs(string prmLista) => AddInputs(new myTuplas(prmLista));
        public void AddInputs(myTuplas prmLista) => Inputs.Add(prmLista);

        public void AddFlows(string prmLista) => Flows.Add(prmLista);
        public void AddFlows(myTuplas prmLista) => Flows.Add(prmLista);

        public DataInputs Inputs { get; }
        public DataFlows Flows { get; }

        public DataView(string prmName)
        {

            Inputs = new DataInputs(this);
            Flows = new DataFlows(this);

            name = prmName;
        }

        private string GetLog()
        {
            myLines memo = new myLines();

            memo.Add(Inputs.log, prmNotEmpty: true);
            memo.Add(Flows.log, prmNotEmpty: true);

            return memo.memo;
        }
    }
    public class DataInput
    {
        public DataView View { get; }

        public myTupla Tupla { get; }

        public string name => Tupla.name;
        public string alias => Tupla.alias;
        public string format => Tupla.format;
        public string log => Tupla.input;

        public DataInput(DataView prmView, myTupla prmTupla)
        {
            View = prmView;

            Tupla = prmTupla;
        }

    }
    public class DataFlow
    {
        public DataView Group { get; }
        public myTuplas Tuplas { get; }

        public string log => Tuplas.log;

        public DataFlow(DataView prmGroup, myTuplas prmTuplas)
        {
            Group = prmGroup;

            Tuplas = prmTuplas;
        }

    }

}
