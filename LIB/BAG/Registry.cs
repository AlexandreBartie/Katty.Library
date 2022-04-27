using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myRegisterRoot
    {

        private myRegisterKey root;

        public myRegisterRoot(string prmRoot)
        {
            root = new myRegisterKey(GetTypeRoot()).Node(prmRoot);
        }

        public myRegisterKey Get() => root;

        public myRegisterKey Node(string prmKey) => root.Node(prmKey);

        public void Clear(string prmKey) => root.Clear(prmKey);

        private RegistryKey GetTypeRoot() => Registry.CurrentUser;

    }
    public class myRegisterKey : myRegisterKeyFormats
    {

        public string name => key.Name;

        public myRegisterKey(RegistryKey prmKey) : base(prmKey) { }

        public myRegisterKey Node(string prmKey)
        {
            return new myRegisterKey(GetSubKey(prmKey));
        }

        public void SetData(string prmName, object prmValue) => key.SetValue(prmName, prmValue);
        public void SetData(string prmName, bool prmChecked) => SetData(prmName, myBool.GetYesNo(prmChecked));

        public RegistryKey GetSubKey(string prmName) => key.CreateSubKey(prmName);

        public object GetValue(string prmName) => key.GetValue(prmName);

        public void Clear(string prmSubKey)
        {
            if (key.OpenSubKey(prmSubKey) != null)
                key.DeleteSubKeyTree(prmSubKey);
        }
    }
    public class myRegisterKeyFormats
    {

        internal RegistryKey key;

        public string[] SubKeys => key.GetSubKeyNames();
        public string[] SubNames => key.GetValueNames();

        public myRegisterKeyFormats(RegistryKey prmKey)
        {
            key = prmKey;
        }

        public string GetString(string prmName) => GetString(prmName, prmDefault: "");
        public string GetString(string prmName, string prmDefault)
        {
            string ret = (string)key.GetValue(prmName);

            ret = myString.GetFull(ret, prmDefault);

            if (!myString.IsFull(ret))
                ret = prmDefault;

            return ret;

        }

        public bool GetBoolean(string prmName) => GetBoolean(prmName, prmDefault: false);
        public bool GetBoolean(string prmName, bool prmDefault) => GetBoolean(prmName, myBool.GetYesNo(prmDefault));
        public bool GetBoolean(string prmName, string prmDefault) => myString.IsMatch(GetString(prmName, prmDefault), myBool.GetYesNo(true));

    }
}
