using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Katty
{

    public delegate void NotifyLog();

    public delegate void NotifySql(string prmMsgError);

    public class TraceLog : TraceWrite
    {

        public event NotifyLog LogExecutado;
        public event NotifySql SqlExecutado;

        public TraceTipo Geral;

        public TraceLogApp LogApp;

        public TraceLogData LogData;

        public TraceLogPath LogPath;

        public TraceLogFile LogFile;

        public TraceErro Erro;

        public TraceMSG Msg;

        public TraceLog()
        {

            Geral = new TraceTipo();

            LogApp = new TraceLogApp();

            LogData = new TraceLogData();

            LogFile = new TraceLogFile();

            LogPath = new TraceLogPath();

            Erro = new TraceErro();

            Msg = new TraceMSG();

            Setup(this);

        }

        public void OnLogExecutado()
        {
            LogExecutado?.Invoke();
        }
        public void OnSqlExecutado(string prmTag, string prmSQL, long prmTimeElapsed, bool prmDados)
        {

            Trace.LogData.SQLExecution(prmTag, prmSQL, prmTimeElapsed, prmDados);

            SqlExecutado?.Invoke(prmMsgError: GetError(prmDados));

        }
        public void OnSqlError(string prmTag, string prmSQL, Exception prmErro)
        {

            Trace.LogData.FailSQLConnection(prmTag, prmSQL, prmErro);

            SqlExecutado?.Invoke(prmMsgError: prmErro.Message);

        }

        private string GetError(bool prmDados) => myBool.IIf(prmDados, prmTrue: "", prmFalse: "ZERO Results");

    }

    public class TraceLogPath : TraceTipo
    {
        public void SetPath(string prmContexto, string prmPath) => msgDef(String.Format(@"{0,20} -path: {1}", prmContexto, prmPath));

        public void SetSubPath(string prmContexto, string prmPath) => msgDef(String.Format(@"{0,20} -subpath: {1}", prmContexto, prmPath));

    }
    public class TraceLogFile : TraceTipo
    {

        public void DataFileOpen(myFileTXT prmFile) => DataFileAction(prmAcao: "OPEN", prmContexto: "Importado com sucesso", prmFile);

        public void DataFileSave(myFileTXT prmFile) => DataFileAction(prmAcao: "CODE", prmContexto: "Script salvo com sucesso", prmFile, prmEncoding: "default");
        public void DataFileSave(myFileTXT prmFile, string prmEncoding) => DataFileAction(prmAcao: "SAVE", prmContexto: "Salvo com sucesso", prmFile, prmEncoding);
        public void DataFileMute(myFileTXT prmFile, string prmEncoding) => DataFileAction(prmAcao: "MUTE", prmContexto: "Silenciado com sucesso", prmFile, prmEncoding);

        private void DataFileAction(string prmAcao, string prmContexto, myFileTXT prmFile) => DataFileAction(prmAcao, prmContexto, prmFile, prmEncoding: "");
        private void DataFileAction(string prmAcao, string prmContexto, myFileTXT prmFile, string prmEncoding)
        {

            string txt;

            if (prmAcao == "MUTE")
                txt = "save";
            else
                txt = prmAcao.ToLower();

            //

            string msg = string.Format("act# -{0}: {1}.", txt, prmContexto);

            if (myString.IsFull(prmEncoding))
                msg += @" -encoding: " + prmEncoding;

            if (myString.GetFirst(prmFile.nome, prmDelimitador: ".") != "")
            {
                msg += @" -file: " + prmFile.nome;

            }


            msgFile(prmAcao, msg);

        }

        public void DataFileFormatTXT(string prmConteudo) => msgFile(prmTipo: "TXT", prmConteudo);
        public void DataFileFormatCSV(string prmConteudo) => msgFile(prmTipo: "CSV", prmConteudo);
        public void DataFileFormatJSON(string prmConteudo) => msgFile(prmTipo: "JSON", prmConteudo);

        public void FailDataFileEncoding(string prmEncoding) => msgErro(String.Format("Formato encoding [{0}] não encontrado ...", prmEncoding));
        public void FailDataFileOpen(myFileTXT prmFile) => FailDataFileOpenDefault(prmLocal: String.Format("-file: {0} -path: {1}", prmFile.nome, prmFile.path));
        public void FailJSONFormat(string prmContexto, string prmFlow, Exception prmErro) => msgErro(prmTexto: String.Format(@"Flow JSON: [invalid format] ... contexto: {0} Flow: {1}", prmContexto, prmFlow));

        private void FailDataFileOpenDefault(string prmLocal) => msgErro(String.Format("Falha na abertura do arquivo ... {0}", prmLocal));

    }
    public class TraceLogApp : TraceTipo
    {

        public void SetApp(string prmAppName, string prmAppVersion) { msgApp(string.Format("-name {0} -version: {1}", prmAppName, prmAppVersion)); }

    }
    public class TraceLogData : TraceLogData_Fail
    {

        public void DBConnection(string prmTag, string prmStatus) => msgData(string.Format("-db[{0}] -status: {1}", prmTag, prmStatus));
        public void DBSetup(string prmTag, string prmCommand) => msgNoSQL(string.Format("-db[{0}] -setup: {1}", prmTag, prmCommand));

        public void SQLExecution(string prmTag, string prmSQL, long prmTimeElapsed, bool prmTemDados) => GetSQLExecution(prmMsg: string.Format(@"-db[{0}] -sql: {1}", prmTag, prmSQL), prmSQL, prmTimeElapsed, prmTemDados);

        public void SQLViewsSelection(string prmTag, int prmQtde)
        {
            if (prmQtde > 0)
                msgSet(string.Format(@"-view[{0}] -itens: {1}", prmTag, prmQtde));
            else
                msgErro(string.Format(@"msg# -view[{0}] -desc: View sem dados", prmTag));
        }

        private void GetSQLExecution(string prmMsg, string prmSQL, long prmTimeElapsed, bool prmTemDados)
        {
            if (prmTemDados)
                msgSQL(prmMsg, prmSQL, prmTimeElapsed);
            else
                msgErroSQL(prmMsg, prmSQL, prmTimeElapsed, prmMsgError: "ZERO Results");
        }

    }
    public class TraceLogData_Fail : TraceTipo
    {

        public void FailDBBlocked(string prmTag, string prmConexao) => FailConnection(prmMSG: "Conexão bloqueada", prmTag, prmVar: "-string", prmConexao, prmErro: @"APENAS para testes unitários");
        public void FailDBConnection(string prmTag, string prmConexao, Exception prmErro) => FailConnection(prmMSG: "Conexão falhou", prmTag, prmVar: "-string", prmConexao, prmErro);

        public void FailDBSetup(string prmTag, string prmSetup) => FailConnection(prmMSG: "Setup DB falhou", prmTag, prmVar: "-setup", prmSetup, prmErro: "Indeterminado");

        public void FailSQLConnection(string prmTag, string prmSQL, Exception prmErro) => FailConnection(prmMSG: "SQL falhou", prmTag, prmVar: "-sql", prmSQL, prmErro);
        public void FailSQLNoDataBase(string prmTag, string prmSQL, Exception prmErro) => FailConnection(prmMSG: "DB Desconectado", prmTag, prmVar: "-sql", prmSQL, prmErro);
        public void FailFindDataView(string prmTag) => msgErro(prmTexto: string.Format("Data View não identificada ... >>> Flow: [{0}] não executou o SQL ...", prmTag));

        public void FailFindSQLCommand() => msgErro("-db: sql isnt found ...");

        private void FailConnection(string prmMSG, string prmTag, string prmVar, Exception prmErro) => FailConnection(prmMSG, prmTag, prmVar, GetMsgErro(prmErro));
        private void FailConnection(string prmMSG, string prmTag, string prmVar, string prmErro) => msgErro(String.Format(@"{0} >>> tag:[{1}] {2}", prmMSG, prmTag, prmVar), prmErro);

        private void FailConnection(string prmMSG, string prmTag, string prmVar, string prmSQL, Exception prmErro) => FailConnection(prmMSG, prmTag, prmVar, prmSQL, GetMsgErro(prmErro));
        private void FailConnection(string prmMSG, string prmTag, string prmVar, string prmSQL, string prmErro) => msgErroSQL(String.Format(@"{0} ... -db[{1}] {2}: {3} ", prmMSG, prmTag, prmVar, prmSQL), prmSQL, prmErro);

        public void FailSQLFailInterface(int prmHeaderColumns, int prmSQLColumns) => msgErro(prmTexto: string.Format("Interface SQL com número de colunas incompatível ... -header({0}) -sql({1})", prmHeaderColumns, prmSQLColumns));
        public void FailSQLFormatFindField(string prmCampo, string prmFormat) => msgErro(prmTexto: string.Format("Coluna SQL informada em Mask não encontrada ... -column: {0} -format: {1}", prmCampo, prmFormat));

        private string GetMsgErro(Exception prmErro) { if (prmErro != null) return (prmErro.Message); return (""); }

    }

    public class TraceTipo : TraceErro
    {

        public void msgApp(string prmTrace) => Message(prmTipo: "APP", prmTrace);

        public void msgCFG(string prmTrace) => Message(prmTipo: "CFG", prmTrace);
        public void msgCode(string prmTrace) => Message(prmTipo: "CODE", prmTrace);
        public void msgPlay(string prmTrace) => Message(prmTipo: "PLAY", prmTrace);

        public void msgDef(string prmTrace) => Message(prmTipo: "DEF", prmTrace, prmPrefixo: "def");
        public void msgSet(string prmTrace) => Message(prmTipo: "SET", prmTrace, prmPrefixo: "act");

        public void msgSQL(string prmTrace, string prmSQL, long prmTimeElapsed) => Message(prmTipo: "SQL", prmTrace, prmSQL, prmTimeElapsed, prmMsgError: null);

        public void msgNoSQL(string prmTrace) => Message(prmTipo: "xSQL", prmTrace, prmPrefixo: "def");

        public void msgData(string prmTrace) => Message(prmTipo: "DAT", prmTrace, prmPrefixo: "act");

        public void msgFile(string prmTipo, string prmMensagem) => Message(prmTipo, prmMensagem);

        public void msgAviso(string prmAviso) => Message(prmTipo: "WARN", prmAviso);
        public void msgFalha(string prmAviso) => Message(prmTipo: "FAIL", prmAviso);

    }
    public class TraceErro : TraceWrite
    {
        private string GetMsgError(string prmTexto, string prmMsgError) => String.Format(">>>> [{0}] {1}", prmMsgError, prmTexto);
        public void msgErro(string prmTexto) => Message(GetTypeError, prmTexto);
        public void msgErro(Exception e) => Message(GetTypeError, e.Message);

        public void msgErro(string prmTexto, Exception e) => msgErro(prmTexto, prmMsgError: e.Message);
        public void msgErro(string prmTexto, string prmMsgError) => Message(GetTypeError, GetMsgError(prmTexto, prmMsgError));

        public void msgErroSQL(string prmTexto, string prmSQL, string prmMsgError) => msgErroSQL(prmTexto, prmSQL, prmTimeElapsed: 0, prmMsgError);
        public void msgErroSQL(string prmTexto, string prmSQL, long prmTimeElapsed, string prmMsgError) => Message(prmTipo: GetTypeError, GetMsgError(prmTexto, prmMsgError), prmSQL, prmTimeElapsed, prmMsgError);

    }

    public class TraceWrite : TraceBase
    {

        protected static TraceLog Trace;

        public void Setup(TraceLog prmTrace)
        {
            Trace = prmTrace;
        }

        protected void Message(string prmTipo, string prmTexto, string prmSQL, long prmTimeElapsed, string prmMsgError) => Message(prmTipo, prmTexto, prmPrefixo: "", prmSQL, prmTimeElapsed, prmMsgError);
        protected void Message(string prmTipo, string prmTexto) => Message(prmTipo, prmTexto, prmPrefixo: "");
        protected void Message(string prmTipo, string prmTexto, string prmPrefixo) => Message(prmTipo, prmTexto, prmPrefixo, prmSQL: "", prmTimeElapsed: 0, prmMsgError: null);
        protected void Message(string prmTipo, string prmTexto, string prmPrefixo, string prmSQL, long prmTimeElapsed, string prmMsgError)
        {
            string texto = prmTexto;

            if (prmPrefixo != "")
                texto = prmPrefixo + "# " + texto;

            LogTrace(prmTipo, texto, prmSQL, prmTimeElapsed, prmMsgError);

        }

        private void LogTrace(string prmTipo, string prmTexto, string prmSQL, long prmTimeElapsed, string prmMsgError)
        {

            if (Trace.Msg.Exibir(prmTipo, prmTexto, prmSQL, prmTimeElapsed, prmMsgError))
                Trace.OnLogExecutado();
        }

    }

    public class TraceMSG : TraceHide
    {

        public string tipo;
        public string msg;

        public string sql;

        public long time_elapsed;

        public string msgError;

        public double time_seconds => Convert.ToDouble(time_elapsed) / 1000;

        public string elapsed_seconds => myFormat.DoubleToString(time_seconds, prmFormat: "##0.000");

        public string title => myBool.IIf(IsError, "ERRO", tipo);

        public string key => String.Format("[{0,6}] {1} ", time_elapsed, msg);
        public string txt => String.Format("[{0,4}] {1} ", title, msg);
        public string txt_sql => String.Format("[{0,4}] {1} ", title, sql);

        public bool IsError => (IsMatch(GetTypeError) || myString.IsFull(msgError));

        public bool IsHidden => Hidden.IsFind(tipo);
        public bool IsMatch(string prmTipo) => myString.IsMatch(tipo, prmTipo);

        public TraceMSG()
        { }

        public TraceMSG(string prmTipo, string prmMSG)
        {
            tipo = prmTipo;

            msg = prmMSG;
        }

        public TraceMSG(string prmMSG, string prmSQL, long prmTimeElapsed, string prmMsgError)
        {
            tipo = "SQL";

            msg = prmMSG;

            sql = prmSQL;

            time_elapsed = prmTimeElapsed;

            msgError = prmMsgError;
        }

        public TraceMSG Clonar() => new TraceMSG(msg, sql, time_elapsed, msgError);

        public bool Exibir(string prmTipo, string prmMSG, string prmSQL, long prmTimeElapsed, string prmMsgError)
        {

            tipo = prmTipo;
            msg = prmMSG;

            sql = prmSQL;

            time_elapsed = prmTimeElapsed;

            msgError = prmMsgError;

            if (IsHidden) return false;

#if DEBUG

            Debug.WriteLine(txt);

#else

            System.Console.WriteLine(txt);

#endif

            return true;

        }

    }

    public class TraceHide : TraceBase
    {
        public myDominio Hidden;

        public TraceHide()
        {
            SetHidden(prmList: "CFG,CODE,PLAY,DEF,xSQL");
        }
        public void SetHidden(string prmList) { Hidden = new myDominio(GetFormat(prmList)); }

        private string GetFormat(string prmList) => "{ " + prmList + " }";
    }

    public class TraceBase
    {
        public static string GetTypeError => "ERRO";

    }

}   
