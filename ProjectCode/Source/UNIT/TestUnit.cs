using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Katty
{

    public class TestUnit
    {

        public TestLines Input;
        public TestLines Output;

        private TestCheck Check;

        public myFlow Flow => Check.Flow;

        public void input() => input(prmText: "");
        public void input(string prmText) => input(prmText, prmCondicao: true);
        public void input(string prmText, bool prmCondicao) => input(prmText, prmArg: null, prmCondicao: true);
        public void input(string prmText, string prmArg) => input(prmText, prmArg, prmCondicao: true);
        public void input(string prmText, string prmArg, bool prmCondicao) { if (prmCondicao) Input.Add(prmText, prmArg); }


        public void inputText(string prmText) => inputText(prmText, prmCondicao: true);
        public void inputText(string prmText, bool prmCondicao) { if (prmCondicao) Input.AddText(prmText); }

        public void output() => output(prmText: "");
        public void output(string prmText) => Output.Add(prmText);

        public void outputText() => output();
        public void outputText(string prmText) => outputText(prmText, prmEnter: false);
        public void outputText(string prmText, bool prmEnter) { Output.AddText(prmText); if (prmEnter) output(); }

        public string GetResult() => Check.GetResult();

        public string log => Check.GetDifferences();

        public TestUnit()
        {
            Check = new TestCheck(this);  Setup();
        }

        private void Setup()
        {
            Input = new TestLines();
            Output = new TestLines();
        }

        public void AssertTestNoFail(string prmResult) => AssertTest(prmResult, prmExt: false, prmFail: false);
        public void AssertTestExt(string prmResult) => AssertTest(prmResult, prmExt: true, prmFail: true);
        public void AssertTest(string prmResult) => AssertTest(prmResult, prmExt: false, prmFail: true);
        public void AssertTest(string prmResult, bool prmExt, bool prmFail)
        {

            Check = new TestCheck(this);

            if (Check.IsFail(prmResult, prmExt, prmFail))
                Assert.Fail(log);
        }

    }

    public class TestCheck
    {

        private TestUnit Unit;

        private TestUnitAnalyze Analyse;

        private TestLines Result;
        private TestLines Expected => Unit.Output;

        public string GetResult() => Result.txt;
        public string GetDifferences() => Analyse.dif;

        public myFlow Flow => GetFlow(); private myFlow _Flow;

        public TestCheck(TestUnit prmUnit)
        {
            Unit = prmUnit; Analyse = new TestUnitAnalyze();
        }

        public bool IsFail(string prmResult, bool prmExt, bool prmFail)
        {

            Result = new TestLines(prmResult);

            Analyse.GetCompare(prmResult: Result, prmExpected: Expected, prmExt);

            // assert
            return (!Expected.IsMatch(Result.txt) && prmFail);
        }

        private myFlow GetFlow()
        {
            if (_Flow == null) 
                _Flow = new myFlow(prmData: Unit.Input.txt); 
           
            return _Flow;
        }

    }

    public class TestLines : List<TestLine> //List<string>
    {

        public bool IsFull => (this.Count > 0);
        public bool IsMatch(string prmText) => (txt == prmText);

        public string txt => GetTXT();
        public string ext => txt + Environment.NewLine;
        public string output(bool prmEXT) { if (prmEXT) return ext; return txt; }

        public TestLines()
        { }

        public TestLines(string prmText)
        {
            Add(prmText);
        }
        public string GetLine(int prmIndice)
        {
            if (prmIndice <= this.Count)
                return this[prmIndice - 1].txt;
            return "";
        }

        public void Add() => Add(prmText: "");
        public void Add(string prmText) => Add(prmText, prmArg: null);
        public void Add(string prmText, string prmArg) => AddLine(prmText, prmArg);

        private void AddLine(string prmText, string prmArg)
        {
            foreach (string line in new myLines(prmText))
                AddText(line, prmArg, prmMerge: false);
        }
        public void AddText(string prmText) => AddText(prmText, prmArg: null, prmMerge: true);

        private void AddText(string prmText, string prmArg, bool prmMerge)
        {
            if (prmMerge && this.Count != 0)
                base[base.Count - 1].txt += prmText;
            else
                if (prmText != null)
                base.Add(new TestLine(prmText, prmArg));
        }

        private string GetTXT()
        {
            myLines Lines = new myLines();

            foreach (TestLine Line in this)
                Lines.Add(Line.txt);

            return Lines.memo;
        }

        public List<string> GetList()
        {
            List<string> Lines = new List<string>();

            foreach (TestLine Line in this)
                Lines.Add(Line.txt);

            return Lines;
        }

    }

    public class  TestLine
    {
        public string txt;

        public string arg;

        public TestLine(string prmText, string prmArg)
        { txt = prmText; arg = prmArg; }

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
