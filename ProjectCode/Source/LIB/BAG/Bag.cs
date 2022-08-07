using Katty;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BlueRocket
{
    public class myRegisterBag
    {
        public myRegisterKey Parent;

        public myRegisterKey Local => Parent.Node(key);

        public myRegisterChecks Checks;

        private string key;

        public myRegisterBag(string prmKey, myRegisterKey prmParent)
        {
            Parent = prmParent; key = prmKey;

            Checks = new myRegisterChecks(this);
        }
        public void Clear() => Local.Clear(key);

        public void Add(string prmName, object prmValue) => Local.SetData(prmName, prmValue);

    }
    public class myRegisterCheck
    {
        private myRegisterBag Base;

        public string name;

        public bool defaultYes;
        public bool IsChecked => Get();

        private ToolStripMenuItem Menu;

        public bool IsMatch(string prmName) => myString.IsMatch(name, prmName);

        public myRegisterCheck(string prmName, myRegisterBag prmBase)
        {
            name = prmName; Base = prmBase;
        }
        public myRegisterCheck(string prmName, bool prmDefaultYes, myRegisterBag prmBase)
        {
            name = prmName; defaultYes = prmDefaultYes; Base = prmBase;
        }

        public void Link(ToolStripMenuItem prmMenu) { Menu = prmMenu; Menu.Checked = Get(); }
        public void Check() => Set(myMenu.InvertCheck(Menu));

        public void Set(bool prmChecked) => Base.Local.SetData(name, prmChecked);
        private bool Get() => Base.Local.GetBoolean(name, defaultYes);
    }
    public class myRegisterChecks : List<myRegisterCheck>
    {

        private myRegisterBag Base;

        public myRegisterChecks(myRegisterBag prmBase)
        {
            Base = prmBase;
        }

        public myRegisterCheck Get(string prmName)
        {
            foreach (myRegisterCheck Check in this)
                if (Check.IsMatch(prmName))
                    return Check;

            return new myRegisterCheck(prmName, Base);
        }

    }
}
