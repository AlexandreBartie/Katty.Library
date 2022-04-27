using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myMasks : myTuplas
    {

        public myMasks(string prmLista) : base(prmTuplas: new myTuplas(prmLista, prmSeparador: ",", prmConector: ":")) { }

        public myMasks(myMasks prmLista) : base(prmLista) { }
 
        public string TextToString(string prmKey, string prmText) => myFormat.TextToString(prmText, GetFormat(prmKey));

        public string GetFormat(string prmKey) => GetFormat(prmKey, prmPadrao: "");
        public string GetFormat(string prmKey, string prmPadrao) => GetValue(prmKey, prmPadrao);

    }
}
