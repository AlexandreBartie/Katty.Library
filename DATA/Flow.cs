using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Katty
{
    public class myFlow
    {

        public string key { get; set; }
        
        private myJSON Flow;

        public myFlow() { }

        public myFlow(string prmData) { Parse(prmData); }

        public void Parse(string prmData)
        {

            myBrickChaves Brick = new myBrickChaves();

            key = Brick.GetMain(prmData);

            Flow = new myJSON(prmData: Brick.GetSpot(prmData, prmPreserve: true));

        }

        public object Execute(object prmObject, string prmMethod) => new myReflection(prmObject).Invoke(prmMethod, prmArgs: Flow);

        public string GetValue(string prmKey) => Flow.GetValue(prmKey);

    }


}
