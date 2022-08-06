using System;
using System.Text.Json;
using System.Diagnostics;

namespace Katty
{
    public class myJSON
    {

        public myJSONCore Core;

        private bool _IsOK;

        public bool IsOK { get => _IsOK; }
        public bool IsErro { get => (Erro != null); }
        public bool IsCurrent { get => Core.IsCurrent; }

        public Exception Erro { get => Core.erro; }

        public string flows { get => Core.flows; }
        public string lines { get => Core.lines; }
        public string values { get => Core.values; }

        public string txt(bool prmFlow) => myBool.IIf(prmFlow, flows, lines);

        public JsonProperty GetProperty(string prmKey) => Core.GetProperty(prmKey);

        public myJSON()
        {
            Core = new myJSONCore(this);

        }
        public myJSON(string prmData)
        {

            Core = new myJSONCore(this);

            Parse(prmData);

        }

        public void Add(string prmData)
        {
            Core.Add(prmData);
        }

        public void Add(string prmData, string prmMestre)
        {
            Core.AddCombine(prmData, prmMestre);
        }
        
        public bool Parse(string prmData)
        {

            Add(prmData);

            return (Save());

        }
        public bool Save()
        {

            _IsOK = Core.Save();

            return (IsOK);

        }
        public bool Next() => Core.Next();

        public bool Find(string prmKey) => Core.Find(prmKey);

        public string FindValue(string prmKey, string prmFormato) => Core.FindValue(prmKey, prmFormato);
        public string GetValue(string prmKey) => Core.GetValue(prmKey);
        public string GetValue(string prmKey, string prmPadrao) => Core.GetValue(prmKey, prmPadrao);

    }
    public class myJSONCore : myJSONFormat
    {

        public bool IsCombineFull;

        public bool IsCurrent;

        public Exception erro;

        public myJSONCore(myJSON prmJSON) : base(prmJSON) { }

        public void Add(string prmFlow)
        {

            string linha = @prmFlow;

            //Flow = Flow.Replace(@"\'", @"#""");
            linha = linha.Replace(@"'", "\"");

            Data.Add(linha);

        }

        public void AddCombine(string prmFlow, string prmMestre)
        {
            if (prmMestre == null)
                Add(prmFlow);
            else
                Add(prmFlow: GetCombine(prmFlow, prmMestre));
        }

        private string GetCombine(string prmFlow, string prmMestre)
        {

            myJSON Flow = new myJSON(prmFlow);

            myJSON Mestre = new myJSON(prmMestre);

            // Lista acomodara o Flow Combinado

            myMemo Memo = new myMemo(";");

            // Sobrepor valores do MESTRE que estão presentes no Flow

            foreach (JsonProperty prop in Flow.Core.Args)
            {

                JsonProperty mix = prop;

                if (Mestre.Find(prmKey: prop.Name))
                { mix = Mestre.GetProperty(prmKey: prop.Name); }


                Memo.Add(string.Format("'{0}': '{1}'", mix.Name, mix.Value));

            }

            // Inserir propriedades MESTRE que não aparecem no Flow

            foreach (JsonProperty prop in Mestre.Core.Args)
            {

                if (!Flow.Find(prmKey: prop.Name))
                { Memo.Add(string.Format("'{0}': '{1}'", prop.Name, prop.Value)); }

            }

            return ("{ " + Memo.csv + " }");

        }
        public bool Save()
        {
            try
            {
                doc = JsonDocument.Parse(flows);

                Corpo = root.EnumerateArray();

                Next();

                return (true);
            }

            catch (Exception e)
            { 
                Debug.WriteLine("Flow JSON: " + flows);
                Debug.WriteLine("Erro JSON: " + e.Message);

                Setup();  erro = e; return (false);
            }
        }
        public bool Next()
        {

            IsCurrent = Corpo.MoveNext();

            return (IsCurrent);
        }

        public bool Find(string prmKey)
        {

            JsonProperty prop = GetProperty(prmKey);

            try
            { string x = prop.Name; return (true); }

            catch 
            {  }

            return (false);

        }
        public string FindValue(string prmKey, string prmFormato)
        {

            string vlValor = GetValue(prmKey);

            if (vlValor != "")
                return (String.Format(prmFormato, vlValor));

            return (vlValor);

        }
        public string GetValue(string prmKey) => GetValue(prmKey, prmPadrao: "");

        public string GetValue(string prmKey, string prmPadrao)
        {
            try
            {
                return (GetProperty(prmKey).Value.GetString());
            }
            catch (Exception e)
            { erro = e; }

            return (prmPadrao);
        }

        public JsonProperty GetProperty(string prmKey)
        {

            foreach (JsonProperty Arg in Args)
            {

                if (Arg.Name.ToLower() == prmKey.ToLower())
                {
                    return Arg;
                }
            }

            return(new JsonProperty());
        }

    }
    
    public class myJSONFormat
    {
        internal myJSON JSON;

        internal myJSONData Data;

        internal JsonDocument doc;
        internal JsonElement root => doc.RootElement;
        internal JsonElement item => Corpo.Current;
        internal JsonElement.ObjectEnumerator Args => item.EnumerateObject();

        internal JsonElement.ArrayEnumerator Corpo;

        internal string flows => GetFlows();
        internal string lines => GetLines();
        internal string values => GetValues();

        public myJSONFormat(myJSON prmJSON)
        {
            JSON = prmJSON; Setup();
        }

        internal void Setup()
        {
            Data = new myJSONData(JSON);
        }

        private string GetFlows() => (Data.output);
        private string GetLines()
        {

            myMemo lines = new myMemo();

            foreach (JsonElement item in Corpo)

                foreach (JsonProperty propriedade in item.EnumerateObject())
                    lines.Add(GetFormatTupla(prmKey: propriedade.Name));

            return (lines.csv);

        }

        private string GetValues()
        {

            myMemo lines = new myMemo();

            foreach (JsonElement item in Corpo)

                foreach (JsonProperty propriedade in item.EnumerateObject())
                    lines.Add(GetFormatValue(prmKey: propriedade.Name));

            return (lines.csv);

        }
        private string GetFormatValue(string prmKey) => string.Format("'{0}'", JSON.GetValue(prmKey));
        private string GetFormatTupla(string prmKey) => string.Format("[{0}]: '{1}'", prmKey, JSON.GetValue(prmKey));

    }
    internal class myJSONData : myMemo
    {

        private myJSON JSON;

        public myJSONData(myJSON prmJSON)
        {

            JSON = prmJSON;

        }

        public string output
        {
            get

            {

                if (IsVazio)
                    return ("[ ]");
                else
                    return ("[ " + csv + " ]");

            }

        }

    }

}