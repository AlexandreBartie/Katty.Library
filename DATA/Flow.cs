using System;
using System.Collections.Generic;
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

            Flow = new myJSON(prmFlow: Brick.GetSpot(prmData, prmPreserve: true));

        }

        public string GetValor(string prmKey) => Flow.GetValor(prmKey);

    }
}
