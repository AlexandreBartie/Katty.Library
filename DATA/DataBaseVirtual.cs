using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class DataVirtualConnect
    {
        private OracleConnection conexao;

        public DataVirtualConnect(string prmConnection)
        {
            conexao = new OracleConnection(prmConnection);
        }

        public OracleConnection GetConnection => conexao;

        public void Open() => conexao.Open();

        public void Close() => conexao.Close();

        public bool Execute(string prmSQL, int prmTimeOut)
        {
            DataVirtualCommand Command = new DataVirtualCommand(prmSQL, this, prmTimeOut);

            return Command.GetNoResults();
        }

    }

    public class DataVirtualCommand
    {

        private OracleCommand command;

        public DataVirtualCommand(string prmSQL, DataVirtualConnect prmConnect, int prmTimeOut)
        {
            command = new OracleCommand(prmSQL, prmConnect.GetConnection);

            command.CommandTimeout = prmTimeOut;
        }

        public bool GetNoResults()
        {
            int result = command.ExecuteNonQuery();

            return result == -1;
        }

        public DataVirtualReader GetReader() => new DataVirtualReader(command);

    }

    public class DataVirtualReader
    {

        private OracleDataReader reader;

        private OracleCommand command;

        public DataVirtualReader(OracleCommand prmCommand)
        {
            command = prmCommand;

            reader = command.ExecuteReader();
        }

        public bool IsDBNull(int prmIndice) => reader.IsDBNull(prmIndice);

        public bool IsFind(string prmName) => (GetIndex(prmName) != -1);
        public int GetIndex(string prmName) => GetOrdinal(prmName);
        public string GetName(int prmIndice) => reader.GetName(prmIndice);
        public string GetType(int prmIndice) => reader.GetDataTypeName(prmIndice).ToLower();
        public DateTime GetDateTime(int prmIndice) => reader.GetDateTime(prmIndice);
        public Double GetDouble(int prmIndice) => reader.GetDouble(prmIndice);
        public string GetString(int prmIndice) => reader.GetOracleValue(prmIndice).ToString();

        public int GetFieldCount => reader.VisibleFieldCount;


        public bool Next() => reader.Read();
        public void Close() => reader.Close();

        private int GetOrdinal(string prmName)
        {
            try
            { return reader.GetOrdinal(prmName); }

            catch
            { return -1; }

        }
    }
}
