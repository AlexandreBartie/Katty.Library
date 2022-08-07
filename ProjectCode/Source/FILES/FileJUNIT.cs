using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Katty
{
    public class myFileJUNIT : myFileTXT
    {

        private TestCasesJUnit Cases;

        private TestJSONJUnit _JSON;

        public List<TestCaseJUnit> TestCases { get => Cases.TestCases; }

        public string separador { get => ","; }

        public TestJSONJUnit JSON
        {
            get
            {
                if (_JSON == null)
                    _JSON = new TestJSONJUnit(this);
                return (_JSON);
            }

            set { _JSON = value; }

        }

        public override bool Open(string prmPath, string prmName)
        {

            Cases = null;

            if (base.Open(prmPath, prmName))
            {

                Cases = new TestCasesJUnit(this);

                foreach (string line in base.lines)
                {

                    Cases.AddLine(line);

                }

            }

            return (base.IsOK);

        }

    }
    public class TestCasesJUnit
    {

        private myFileJUNIT File;

        internal List<TestCaseJUnit> TestCases;

        private TestCaseJUnit TestCaseCurrent;

        public TestCasesJUnit(myFileJUNIT prmFile)
        {

            File = prmFile;

            TestCases = new List<TestCaseJUnit>();

        }

        public void AddLine(string prmLine)
        {
            if (IsTestCase(prmLine))

                AddCase(prmLine);

            else

                AddFlow(prmLine);

        }

        private void AddCase(string prmLine)
        {

            TestCaseCurrent = new TestCaseJUnit(prmLine, File);

            TestCases.Add(TestCaseCurrent);

        }

        private void AddFlow(string prmLine) 
        {

            TestCaseCurrent.Flows.Add(prmLine);

        }

        public string memo()
        {

            string lista = "";
            string aux = "";

            foreach (TestCaseJUnit test_case in TestCases)
            {

                lista += aux + (test_case.nome);

                aux = Environment.NewLine;

            }

            return lista;
        }

        private bool IsTestCase(string prmLine) => (!prmLine.StartsWith(","));

    }
    public class TestCaseJUnit
    {

        public myFileJUNIT File;

        public TestDataJUnit Flows;

        public TestParametersJUnit Parametros;

        public string nome { get => Parametros.nome; }

        public string separador { get => File.separador; }

        public TestCaseJUnit(string prmLine, myFileJUNIT prmFile)
        {

            File = prmFile;

            Flows = new TestDataJUnit(this);

            Parametros = new TestParametersJUnit(this, prmLine);

        }

    }
    public class TestParametersJUnit
    {

        private TestCaseJUnit TestCase;

        public string nome;

        private myMemo Lista;

        public TestParametersJUnit(TestCaseJUnit prmTestCase, string prmLine)
        {

            TestCase = prmTestCase;

            Lista = new myMemo(prmLine, TestCase.separador);

            nome = Lista.GetRemove();

        }
        public string GetJSON(myMemo prmFlow)
        {

            string lista = "";

            string aux = "";

            int cont = 0;

            foreach (string atributo in Lista)
            {

                cont++;

                lista += aux + string.Format("'{0}': '{1}'", atributo, prmFlow.Item(cont));

                aux = ",";

            }

            return ("{ "+ lista + " }");

        }
        public string txt => Lista.Export(TestCase.separador);
 
    }
    public class TestDataJUnit
    {

        private TestCaseJUnit TestCase;

        public List<myMemo> Dados;

        public TestDataJUnit(TestCaseJUnit prmTestCase)
        {

            TestCase = prmTestCase;

            Dados = new List<myMemo>();

        }

        public void Add(string prmLine)
        {

            myParseCSV item = new myParseCSV(prmLine);

            item.GetRemove();

            Dados.Add(item);

        }

    }
    public class TestJSONJUnit
    {

        private myFileJUNIT File;

        public List<myMemo> Dados;

        public TestJSONJUnit(myFileJUNIT prmFile)
        {

            File = prmFile;

            Dados =  new List<myMemo>();

            GetJSON();

        }

        private void GetJSON()
        {

            foreach (TestCaseJUnit teste in File.TestCases)
            {

                foreach (myMemo Flow in teste.Flows.Dados)
                {

                    Debug.WriteLine(  teste.Parametros.GetJSON(Flow));

                }

            }

        }
        public string memo()
        {

            string lista = "";
            string aux = "";

            foreach (myMemo Flow in Dados)
            {

                lista += aux + (Flow.Export(File.separador));

                aux = Environment.NewLine;

            }

            return lista;
        }


    }
}
