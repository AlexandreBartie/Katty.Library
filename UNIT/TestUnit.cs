using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Katty
{

    public class UTControl
    {

        public LinesUTC inputList;
        public LinesUTC outputList;
        private LinesUTC resultList;

        private AnaliseUTC Analise;

        private string _log;

        public void input() => input(prmText: "");
        public void input(string prmText) => input(prmText, prmCondicao: true);
        public void input(string prmText, bool prmCondicao) { if (prmCondicao) inputList.Add(prmText); }

        public void inputText(string prmText) => inputText(prmText, prmCondicao: true);
        public void inputText(string prmText, bool prmCondicao) { if (prmCondicao) inputList.AddText(prmText); }

        public void output() => output(prmText: "");
        public void output(string prmText) => outputList.Add(prmText);

        public void outputText() => output();
        public void outputText(string prmText) => outputText(prmText, prmEnter: false);
        public void outputText(string prmText, bool prmEnter) { outputList.AddText(prmText); if (prmEnter) output(); }

        public string GetRaw() => inputList.raw;
        public string GetInput() => inputList.txt;
        public string GetOutput() => outputList.txt;
        public string GetResult() => resultList.txt;
        public string log => _log;

        public UTControl()
        {
            Analise = new AnaliseUTC(); Setup();
        }

        private void Setup()
        {
            inputList = new LinesUTC();
            outputList = new LinesUTC();
            resultList = new LinesUTC();
        }

        public void AssertTest(string prmResult)
        {

            resultList.Add(prmResult);

            _log = Analise.GetCompare(prmGerado: resultList, prmEsperado: outputList);

            // assert
            if (!outputList.IsMatch(resultList.txt))
                Assert.Fail(log);
        }

    }

    public class LinesUTC : List<string>
    {

        public bool IsFull => (this.Count > 0);
        public bool IsMatch(string prmText) => (txt == prmText);

        public string raw => GetRAW();
        public string txt => GetTXT();

        public LinesUTC()
        { }

        public LinesUTC(string prmText)
        {
            Add(prmText);
        }
        public string GetLine(int prmIndice)
        {
            if (prmIndice <= this.Count)
                return this[prmIndice-1];
            return "";
        }

        public void Add() => AddLine(prmText: "");
        public new void Add(string prmText) => AddLine(prmText);

        private void AddLine(string prmText)
        {          
            foreach (string line in new xLinhas(prmText))
                AddText(line, prmMerge: false);
        }
        public void AddText(string prmText) => AddText(prmText, prmMerge: true);

        private void AddText(string prmText, bool prmMerge)
        {
            if (prmMerge && this.Count != 0)
                base[base.Count - 1] += prmText;
            else
                if (prmText != null)
                    base.Add(prmText);
        }
        
        private string GetRAW()
        {
            xLinhas linhas = new xLinhas();

            foreach (string texto in this)
                linhas.Add(texto);

            return linhas.txt;
        }
        private string GetTXT() => raw + Environment.NewLine;
    }

//    public class TestCase
//    {
//        //private String selector;

//        //public TestCase(String selector)
//        //{
//        //    this.selector = selector;
//        //}

//        /// Run whatever code you need to get ready for the test to run.
//        protected void setUp() { }

//        /// Release whatever resources you used for the test.
//        protected void tearDown() { }
//        /// Run the selected method and register the result.
///*        public void Run(TestResult result)
//        {
//            //try
//            //{
//               // Run();
//            }
//            //catch (Throwable e)
//            //{
//            //    result.error(this, e);
//            //    return;
//            //}
//            //result.pass(this);
//        }*/
//    }
    
/*    public class TestRobotSuite
    {
        public string name;

        private List<TestCase> testCases = new List<TestCase>();

    public TestRobotSuite(String name)
        {
            this.name = name;
        }

        public TestRobotSuite addTestCase(TestCase testCase)
        {
            testCases.Add(testCase);
            return this;
        }

        public TestResult run()
        {
            TestResult result = new TestResult(name);
            foreach (TestCase Teste in  testCases)
            {
                Teste.Run(result);
            }
            return result;
        }
    }

    public class TestResult
    {
        public  String name;

        public List<String> errors = new List<String>();
        public List<String> passed = new List<String>();

        public TestResult(String name)
        {
            this.name = name;
        }

        public void error(TestCase testCase, Exception error)
        {
            errors.Add(String.Format("%s: %s", testCase, error));
        }

        public void pass(TestCase testCase)
        {
            passed.Add(testCase.ToString());
        }

        public int count()
        {
            return passed.Count + errors.Count;
        }
    }*/
}
