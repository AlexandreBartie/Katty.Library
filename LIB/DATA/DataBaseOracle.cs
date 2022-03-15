using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueRocket.LIBRARY
{

    public class DataBaseOracleConnect
    {
        public OracleConnection Conexao;
    }
    public class DataBaseOracleCursor
    {
        public OracleDataReader reader;
    }
    public class DataBaseOracle : DataBaseOracleDefault
    {

        private string model = @"Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(Host = {0})(PORT = {1})))(CONNECT_DATA =(SERVICE_NAME = {2})));User ID={3};Password={4};Connection Timeout={5}";

        public string user;
        public string password;

        public string host;
        public string port;
        public string service;

        public DataBaseOracle(DataConnect prmConnect)
        { Connect = prmConnect; }

        public string GetString() => String.Format(model, host, port, service, user, password, Connect.varTimeOutDB);

    }
    public class DataBaseOracleDefault
    {

        public DataConnect Connect;

        private DataBaseOracle Oracle => Connect.Assist.Oracle;

        private myJSON Args;

        public void AddJSON(string prmTag, string prmDados)
        {

            Args = new myJSON(prmDados);

            Oracle.host = Args.GetValor("host", prmPadrao: "10.250.1.35");
            Oracle.port = Args.GetValor("port", prmPadrao: "1521");

            Oracle.service = Args.GetValor("service");

            Oracle.user = Args.GetValor("user", prmPadrao: "desenvolvedor_sia");
            Oracle.password = Args.GetValor("password", prmPadrao: "asdfg");

            Connect.AddDataBase(prmTag.ToUpper(), prmConexao: Oracle.GetString());

        }

        private string GetBranch(string prmBranch) => GetStage(prmStage: string.Format("branch_{0}", prmBranch));
        private string GetStage(string prmStage) => prmStage + ".prod01.redelocal.oraclevcn.com";

    }

}
