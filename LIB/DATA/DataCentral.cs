using BlueRocket.LIBRARY.Lib.Data;
using BlueRocket.LIBRARY.Lib.Generic;
using BlueRocket.LIBRARY.Lib.Parse;
using BlueRocket.LIBRARY.Lib.Vars;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace BlueRocket.LIBRARY.Factory.Data
{

    public class DataConnect
    {

        public TraceLog Trace;

        private bool bloqueado = false;

        public DataBases Bases;

        public DataFormat Format;

        public DataAssist Assist;

        public string varTimeOutDB = "##timeout##";

        public int timeoutDB;
        public int timeoutSQL;

        public string GetFullConnection(string prmStrConnection) => myString.GetSubstituir(prmStrConnection, varTimeOutDB, timeoutDB.ToString());

        public DataTypesField DataTypes => (Bases.DataTypes);

        public DataBase DataBaseCorrente => (Bases.Corrente);

        public bool IsDbOK => (Bases.IsOK);
        public bool IsDbBlocked => bloqueado;

        public DataConnect(TraceLog prmTrace)
        {
            Trace = prmTrace;
            
            Bases = new DataBases(this);

            Format = new DataFormat(this);

            Assist = new DataAssist(this);
        }

        public bool DoConnect() => Bases.DoConnect();

        public bool AddDataBase(string prmTag, string prmConexao) => Bases.Criar(prmTag, prmConexao);

        public void SetDBStatus(bool prmBloqueado) => bloqueado = prmBloqueado;

    }

    public class DataFormat
    {
        private DataConnect Connect;

        public DateTime anchor = DateTime.Now;

        public string formatDateDefault = "DD/MM/AAAA";

        public CultureInfo Culture;

        public DataFormat(DataConnect prmConnect)
        {
            Connect = prmConnect;
        }

        public string GetDateAnchor() => GetDateAnchor(formatDateDefault);
        public string GetDateAnchor(string prmFormat) => GetDateFormat(anchor, prmFormat);

        public string GetTextFormat(string prmText, string prmFormat) => TextToCSV(prmText, prmFormat);

        public string GetDateFormat(DateTime prmDate) => GetDateFormat(prmDate, prmFormat: "");
        public string GetDateFormat(DateTime prmDate, string prmFormat)
        {
            string format = prmFormat;

            if (myString.IsEmpty(format))
                format = formatDateDefault;

            return DateToCSV(prmDate, format);
        }
        public string GetDoubleFormat(double prmNumber) => GetDoubleFormat(prmNumber, prmFormat: "");
        public string GetDoubleFormat(double prmNumber, string prmFormat) => DoubleToCSV(prmNumber, prmFormat);

        private string TextToCSV(string prmText, string prmFormat) => myCSV.TextToCSV(prmText, prmFormat);
        private string DateToCSV(DateTime prmDate, string prmFormat) => myCSV.DateToCSV(prmDate, prmFormat);
        private string DoubleToCSV(Double prmNumber, string prmFormat) => myCSV.DoubleToCSV(prmNumber, prmFormat, prmCulture: Culture);

    }

    public class DataAssist
    {
        private DataConnect Connect;

        private DataBaseOracle _Oracle;
        public DataBaseOracle Oracle { get { if (_Oracle == null) _Oracle = new DataBaseOracle(); return _Oracle; } }

        public DataAssist(DataConnect prmConnect)
        {
            Connect = prmConnect;
        }
    }

    public class DataBase
    {

        public DataConnect Connect;

        public DataVirtualConnect Conexao;

        public string tag;

        private string status;

        public string stringConnection;

        private string baseConnection;

        public Exception erro;

        private bool _isOpen;

        public TraceLog Trace => Connect.Trace;

        public DataBase(string prmTag, string prmConexao, DataConnect prmConnect)
        {
            tag = prmTag;

            Connect = prmConnect;

            baseConnection = prmConexao;
        }

        public bool IsOK => _isOpen;
        public string log => string.Format("-db[{0}]: {1}", tag, status);

        private string SetStatus(string prmStatus) { status = prmStatus; _isOpen = (prmStatus == "CONECTADO"); return prmStatus; }

        public string GetStatus() { if (status == "") Setup(); return status; }

        public DataCursor GetCursor(string prmSQL, myTuplas prmMask) => new DataCursor(prmSQL, prmMask, this);

        public bool Setup()
        {

            stringConnection = Connect.GetFullConnection(baseConnection);

            // used to do unit-testing ...
            if (Connect.IsDbBlocked)
            {
                Trace.LogData.FailDBBlocked(tag, stringConnection);

                return (false);
            }

            try
            {

                Conexao = new DataVirtualConnect(stringConnection);

                Conexao.Open();

                Trace.LogData.DBConnection(tag, SetStatus("CONECTADO"));

                return (true);

            }

            catch (Exception e)
            { Trace.LogData.FailDBConnection(tag, stringConnection, e); erro = e; SetStatus("ERRO"); }

            return (false);
        }
        public bool ExecuteNoSQL(string prmNoSQL, int prmTimeOut)
        {

            if (Conexao.Execute(prmNoSQL, prmTimeOut))
            {
                Trace.LogData.DBSetup(tag, prmNoSQL); return true;
            }

            Trace.LogData.FailDBSetup(tag, prmNoSQL);

            return false;
        }

        public void Close()
        {
            try
            { Conexao.Close(); SetStatus("FECHADO"); }

            catch (Exception e)
            { erro = e; }

        }

    }

    public class DataBases : List<DataBase>
    {

        private DataConnect Connect;

        public DataBase Corrente;

        public DataTypesField DataTypes;

        private bool IsConnected;
        public bool IsOK => GetIsOK();

        public DataBases(DataConnect prmConnect)
        {
            Connect = prmConnect;

            DataTypes = new DataTypesField();
        }
        public bool Criar(string prmTag, string prmConexao)
        {
            Corrente = new DataBase(prmTag, prmConexao, Connect);

            Add(Corrente);

            return (Corrente.IsOK);
        }
        public bool DoConnect()
        {
            IsConnected = true;

            foreach (DataBase db in this)
                if (!db.Setup())
                    return false;

            return IsConnected;
        }

        public bool ExecuteNoSQL(string prmNoSQL, int prmTimeOut) => Corrente.ExecuteNoSQL(prmNoSQL, prmTimeOut);

        private bool GetIsOK()
        {

            if (!IsConnected)
                DoConnect();

            bool ok = false;

            foreach (DataBase db in this)
                if (db.IsOK)
                    ok = true;
                else
                    break;

            return ok;
        }
        public string log()
        {
            xMemo lista = new xMemo(prmSeparador: ", ");

            foreach (DataBase db in this)
                lista.Add(db.log);

            return ">dbase: " + lista.txt;
        }

    }

    public class DataTypesField
    {

        private myDominio TypesDate;
        private myDominio TypesDouble;

        public DataTypesField()
        {
            TypesDate = new myDominio(prmKey: "date", prmLista: "date");
            TypesDouble = new myDominio(prmKey: "number", prmLista: "double");
        }

        public bool IsTypeDate(string prmType) => TypesDate.IsContem(prmType);
        public bool IsTypeDouble(string prmType) => TypesDouble.IsContem(prmType);

    }

}
