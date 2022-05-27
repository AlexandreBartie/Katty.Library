using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Katty
{
    public class myReflection
    {
        private object Me => Properties.Me;
        public Type Type => Properties.Type;

        public myMethods Methods;
        public myProperties Properties;

        public myReflection(object prmObject)
        {
            Methods = new myMethods(prmObject); Properties = new myProperties(prmObject);
        }

        public object Invoke(string prmMethod) => Invoke(prmMethod, prmArgs: null);
        public object Invoke(string prmMethod, myJSON prmArgs) => Methods.Invoke(prmMethod, prmArgs);

    }

    public class myMethods : myReflectionBase
    {
        private MethodInfo[] All => Type.GetMethods();

        public myMethods(object prmObject) : base(prmObject) { }

        public object Invoke(string prmMethod, myJSON prmArgs)
        {
            myMethod Method = new myMethod(Me, prmMethod);

            return Method.Invoke(prmArgs);
        }

    }

    public class myMethod : myReflectionBase
    {

        private MethodInfo Method;

        private bool IsFind() => (Method != null);

        private myMethodParameters Pars;

        public myMethod(object prmObject, string prmName) : base(prmObject)
        {
            Method = Type.GetMethod(prmName); Pars = new myMethodParameters(Method);
        }

        public object Invoke(myJSON prmArgs)
        {
            if (IsFind())
                return Type.InvokeMember(Method.Name, GetBindingFlags(), null, Me, Pars.GetValues(prmArgs));

            return null;
        }

        private BindingFlags GetBindingFlags() => BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public;

    }
    public class myMethodParameters
    {
        private MethodInfo Method;
        private ParameterInfo[] Pars => Method.GetParameters();

        public myMethodParameters(MethodInfo prmMethod)
        {
            Method = prmMethod;
        }

        public object[] GetValues(myJSON prmArgs)
        {
            if (prmArgs == null)
                return null;

            ArrayList List = new ArrayList();

            foreach (ParameterInfo Par in Pars)
                List.Add(prmArgs.GetValue(Par.Name));

            return List.ToArray();
        }

    }
    public class myProperties : myReflectionBase
    {
        public PropertyInfo[] All => Type.GetProperties();

        public myProperties(object prmObject) : base(prmObject) { }

    }

    public class myReflectionBase
    {
        public object Me { get; set; }

        public Type Type => Me.GetType();

        public myReflectionBase(object prmObject)
        {
            Me = prmObject;
        }

    }

}