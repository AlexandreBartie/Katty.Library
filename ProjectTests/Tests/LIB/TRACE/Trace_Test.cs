using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty.Tools.Test.LIB.TRACE
{
    [TestClass()]
    public class CAT_010_TraceByFormatTextString_Test : TestUnit
    {

        TraceLog Trace;

        [TestMethod()]
        public void TST010_Trace_SetApp()
        {

            input("APP.SetApp { 'prmAppName': 'xxx', 'prmAppVersion': '2345' }");
            output("No");

            // act & assert
            AssertTRACE();

        }

        [TestMethod()]
        public void TST020_Trace_SetPath()
        {

            input("PATH.SetPath { 'prmContext': 'test', 'prmPath': 'c:\test\' }");
            output("Yes");

            // act & assert
            AssertTRACE();

        }
        private void AssertTRACE()
        {

            Trace = new TraceLog();

            switch (Flow.key)
            {
                case "APP.SetApp":
                    Flow.Execute(Trace.LogApp, "SetApp");
                    break;

                case "PATH.SetPath":
                    Flow.Execute(Trace.LogPath, "SetPath");
                    break;

                default:
                    return;

            }


            AssertTest(prmResult: myBool.GetYesNo(Trace.Msg.IsHidden));
        }

    }

}
