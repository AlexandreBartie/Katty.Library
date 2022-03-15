using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace BlueRocket.LIBRARY
{

    public delegate void NotifyLOG();
    public delegate void NotifySQL();

    public class TraceLog : TraceWrite
    {

        public event NotifyLOG LogExecutado;
        public event NotifySQL SqlExecutado;

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

            SqlExecutado?.Invoke();

        }
        public void OnSqlError(string prmTag, string prmSQL, Exception prmErro)
        {

            Trace.LogData.FailSQLConnection(prmTag, prmSQL, prmErro);

            SqlExecutado?.Invoke();

        }
        public bool Exibir(string prmTipo, string prmTrace, string prmSQL, long prmTimeElapsed) => Msg.Exibir(prmTipo, prmTrace, prmSQL, prmTimeElapsed);

    }

    public class TraceLogPath : TraceTipo
    {
        public void SetPath(string prmContexto, string prmPath) => msgDef(String.Format(@"{0,15} -path: {1}", prmContexto, prmPath));

        public void SetSubPath(string prmContexto, string prmPath) => msgDef(String.Format(@"{0,15} -subpath: {1}", prmContexto, prmPath));

    }
    public class TraceLogFile : TraceTipo
    {

        public void DataFileOpen(FileTXT prmFile) => DataFileAction(prmAcao: "OPEN", prmContexto: "Importado com sucesso", prmFile);

        public void DataFileSave(FileTXT prmFile) => DataFileAction(prmAcao: "CODE", prmContexto: "Script salvo com sucesso", prmFile, prmEncoding: "default");
        public void DataFileSave(FileTXT prmFile, string prmEncoding) => DataFileAction(prmAcao: "SAVE", prmContexto: "Salvo com sucesso", prmFile, prmEncoding);
        public void DataFileMute(FileTXT prmFile, string prmEncoding) => DataFileAction(prmAcao: "MUTE", prmContexto: "Silenciado com sucesso", prmFile, prmEncoding);

        private void DataFileAction(string prmAcao, string prmContexto, FileTXT prmFile) => DataFileAction(prmAcao, prmContexto, prmFile, prmEncoding: "");
        private void DataFileAction(string prmAcao, string prmContexto, FileTXT prmFile, string prmEncoding)
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
        //public void FailDataFileSave(string prmArquivo, string prmPath) => msgErro(String.Format("Falha na criação do arquivo ... -file: {0} -path: {1}", prmArquivo, prmPath));
        public void FailDataFileOpen(FileTXT prmFile) => FailDataFileOpenDefault(prmLocal: String.Format("-file: {0} -path: {1}", prmFile.nome, prmFile.path));
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
        public void DBSetup(string prmTag, string prmSetup) => msgData(string.Format("-db[{0}] -setup: {1}", prmTag, prmSetup));

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
                msgErroSQL(prmMsg, prmSQL, prmTimeElapsed, prmErro: "ZERO Results");
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
        private void FailConnection(string prmMSG, string prmTag, string prmVar, string prmSQL, string prmErro) => msgErroSQL(String.Format(@"{0} ... -error: [{1}] -db[{2}] {3}: {4} ", prmMSG, prmErro, prmTag, prmVar, prmSQL), prmSQL, prmErro);

        private string GetMsgErro(Exception prmErro) { if (prmErro != null) return (prmErro.Message); return (""); }

    }

    public class TraceTipo : TraceErro
    {

        public void msgApp(string prmTrace) => Message(prmTipo: "APP", prmTrace);
        public void msgCode(string prmTrace) => Message(prmTipo: "CODE", prmTrace);
        public void msgDef(string prmTrace) => Message(prmTipo: "DEF", prmTrace, prmPrefixo: "def");
        public void msgSet(string prmTrace) => Message(prmTipo: "SET", prmTrace, prmPrefixo: "act" );
        public void msgPlay(string prmTrace) => Message(prmTipo: "PLAY", prmTrace);

        public void msgSQL(string prmTrace, string prmSQL, long prmTimeElapsed) => Message(prmTipo: "SQL", prmTrace, prmSQL, prmTimeElapsed);

        public void msgData(string prmTrace) => Message(prmTipo: "DAT", prmTrace, prmPrefixo: "act");

        public void msgFile(string prmTipo, string prmMensagem) => Message(prmTipo, prmMensagem);

        public void msgAviso(string prmAviso) => Message(prmTipo: "WARN", prmAviso);
        public void msgFalha(string prmAviso) => Message(prmTipo: "FAIL", prmAviso);

    }
    public class TraceErro : TraceWrite
    {

        private string GetMsgError(string prmTexto, string prmErro) => String.Format(">>>> [{0}] {1}", prmErro, prmTexto);
        private string GetTypeError() => "ERRO";

        public void msgErro(string prmTexto) => Message(GetTypeError(), prmTexto);
        public void msgErro(Exception e) => Message(GetTypeError(), e.Message);

        public void msgErro(string prmTexto, Exception e) => msgErro(prmTexto, prmErro: e.Message);
        public void msgErro(string prmTexto, string prmErro) => Message(GetTypeError(), GetMsgError(prmTexto, prmErro));

        public void msgErroSQL(string prmTexto, string prmSQL, string prmErro) => msgErroSQL(prmTexto, prmSQL, prmTimeElapsed: 0, prmErro);
        public void msgErroSQL(string prmTexto, string prmSQL, long prmTimeElapsed, string prmErro) => Message(GetTypeError(), GetMsgError(prmTexto, prmErro), prmSQL, prmTimeElapsed);

    }

    public class TraceWrite
    {

        protected static TraceLog Trace;

        public void Setup(TraceLog prmTrace)
        {
            Trace = prmTrace;
        }

        protected void Message(string prmTipo, string prmTexto, string prmSQL, long prmTimeElapsed) => Message(prmTipo, prmTexto, prmPrefixo: "", prmSQL, prmTimeElapsed);
        protected void Message(string prmTipo, string prmTexto) => Message(prmTipo, prmTexto, prmPrefixo: "");
        protected void Message(string prmTipo, string prmTexto, string prmPrefixo) => Message(prmTipo, prmTexto, prmPrefixo, prmSQL: "", prmTimeElapsed: 0);
        protected void Message(string prmTipo, string prmTexto, string prmPrefixo, string prmSQL, long prmTimeElapsed)
        {
            string texto = prmTexto;

            if (prmPrefixo != "")
                texto = prmPrefixo + "# " + texto;

            LogTrace(prmTipo, texto, prmSQL, prmTimeElapsed);

        }

        private void LogTrace(string prmTipo, string prmTexto, string prmSQL, long prmTimeElapsed)
        {

            if (Trace.Exibir(prmTipo, prmTexto, prmSQL, prmTimeElapsed))
                Trace.OnLogExecutado();
        }

    }

    public class TraceMSG
    {

        public string tipo;
        public string texto;

        public string sql;

        public long time_elapsed;

        public double time_seconds => Convert.ToDouble(time_elapsed) / 1000;

        public string elapsed_seconds => myFormat.DoubleToString(time_seconds, prmFormat: "##0.000");

        public string msg => String.Format("[{0,4}] {1} ", tipo, texto);

        public string key => String.Format("[{0,6}] {1} ", time_elapsed, texto);

        public bool IsHide => (myString.IsEqual(tipo, "CODE") || myString.IsEqual(tipo, "PLAY"));
        public bool IsErr => IsEqual("ERRO");
        public bool IsEqual(string prmTipo) => myString.IsEqual(tipo, prmTipo);

        public TraceMSG()
        {
        }

        public TraceMSG(string prmTipo, string prmTexto)
        {

            tipo = prmTipo;

            texto = prmTexto;

        }

        public TraceMSG(string prmTrace, string prmSQL, long prmTimeElapsed)
        {

            tipo = "SQL";

            texto = prmTrace;

            sql = prmSQL;

            time_elapsed = prmTimeElapsed;

        }
        public bool Exibir(string prmTipo, string prmTexto, string prmSQL, long prmTimeElapsed)
        {

            tipo = prmTipo;
            texto = prmTexto;

            sql = prmSQL;

            time_elapsed = prmTimeElapsed;

            if (IsHide) return false;

#if DEBUG

            Debug.WriteLine(msg);

#else

            System.Console.WriteLine(msg);

#endif

            return true;

        }

    }

}
