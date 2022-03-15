using System.Diagnostics;
using System.Data;
using System.Collections.Generic;
using System;
using System.IO;

namespace BlueRocket.LIBRARY
{

    public class DataCursor : DataCursorDados
    {

        private string _sql;

        public string sql => _sql;

        public TraceLog Trace => DataBase.Trace;

        public DataCursor(string prmSQL, myTuplas prmMask, DataBase prmDataBase)
        {

            DataBase = prmDataBase;

            _sql = GetTratarSQL(prmSQL);

            if (DataBase.IsOK)
            {
                SetQuery(); SetMask(prmMask);
            }
            else
            { Trace.LogData.FailSQLNoDataBase(DataBase.tag, sql, DataBase.erro); Erro = DataBase.erro; }
        }

        private void SetQuery()
        {

            if (GetRequest(sql, prmTimeOut: DataBase.Connect.timeoutSQL))
            {
                Trace.OnSqlExecutado(DataBase.tag, sql, TimeCursor.Elapsed.milliseconds, TemDados);
            }
            else
                Trace.OnSqlError(DataBase.tag, sql, Erro);
        }

        private string GetTratarSQL(string prmSQL) => Bloco.GetBlocoTroca(prmSQL, prmDelimitadorInicial: "<##>", prmDelimitadorFinal: "<##>", prmDelimitadorNovo: "'");

    }

    public class DataCursorDados : DataCursorReader
    {
        
       public DataFormat Format => DataBase.Connect.Format;

        private DataTypesField DataTypes => DataBase.Connect.DataTypes;

        private xMask Mask;
        public bool IsMask { get => (Mask != null); }

        public void SetMask(myTuplas prmMask)
        {
            if (prmMask.IsFull)
                Mask = new xMask(prmMask);
        }

        public bool IsDBNull(int prmIndice) => reader.IsDBNull(prmIndice);
        public string GetName(int prmIndice) => reader.GetName(prmIndice);
        public string GetType(int prmIndice) => reader.GetType(prmIndice);
        public string GetValor(int prmIndice) => GetValorTratado(prmIndice);
        public string GetValor(string prmCampo) => GetValorTratado(prmCampo);

        private string GetValorTratado(string prmCampo) => GetValorTratado(prmIndice: reader.GetIndex(prmCampo));
        private string GetValorTratado(int prmIndice)
        {
            string tipo = GetType(prmIndice);

            string campo = GetName(prmIndice);

            if (DataTypes.IsTypeDate(tipo))
                return GetMaskDate(campo, prmDate: reader.GetDateTime(prmIndice));

            if (DataTypes.IsTypeDouble(tipo))
               return GetMaskDouble(campo, prmNumber: reader.GetDouble(prmIndice));

            return GetMask(campo, prmText: reader.GetString(prmIndice));
        }

        private string GetMask(string prmCampo, string prmText)
        {
            if (IsMask)
                return Format.GetTextFormat(prmText, Mask.GetFormat(prmCampo));

            return (prmText);
        }
        private string GetMaskDate(string prmCampo, DateTime prmDate)
        {
            if (IsMask)
                return Format.GetDateFormat(prmDate, Mask.GetFormat(prmCampo)); 

            return (Format.GetDateFormat(prmDate));
        }
        private string GetMaskDouble(string prmCampo, Double prmNumber)
        {
            if (IsMask)
                return Format.GetDoubleFormat(prmNumber, Mask.GetFormat(prmCampo));

            return (Format.GetDoubleFormat(prmNumber));
        }
        public string GetCSV(string prmSeparador)
        {
            string memo = "";
            string texto = "";
            string separador = "";

            if (TemDados)
            {

                for (int cont = 0; cont < reader.GetFieldCount; cont++)
                {
                    if (IsDBNull(cont))
                        texto = "";
                    else
                        texto = GetValor(cont);

                    memo += separador + texto;

                    separador = prmSeparador;

                }

            }

            return memo;

        }
        public string GetJSON()
        {
            string memo = "";
            string separador = "";

            if (TemDados)
            {
                for (int cont = 0; cont < reader.GetFieldCount; cont++)
                {
                    if (!IsDBNull(cont))
                    {

                        memo += separador + GetTupla(cont);

                        separador = ", ";

                    }

                }

                return ("{ " + memo + " }");
            }

            return ("{ }");
        }
        public string GetTupla(int prmIndice) => string.Format("'{0}': '{1}'", GetName(prmIndice), GetValor(prmIndice));

    }

    public class DataCursorReader : DataCursorBase
    {

        public DataVirtualReader reader;

        public bool TemDados;

        public bool GetRequest(string prmSQL, int prmTimeOut)
        {

            Erro = null; TemDados = false;

            try
            {
                command = new DataVirtualCommand(prmSQL, DataBase.Conexao, prmTimeOut);

                TimeCursor.Start();

                reader = command.GetReader();

                TemDados = Next();
            }
            catch (Exception e)
            { Erro = e; }

            finally
            {
                TimeCursor.Stop(); 
            }

            return (IsOK);
        }

        public bool Next() => reader.Next();
        public bool Fechar()
        {
            if (IsOK)
            { reader.Close(); }

            return (IsOK);
        }

    }

    public class DataCursorCommand : DataCursorBase
    {

        public bool Execute(string prmSQL, int prmTimeOut)
        {

            Erro = null;

            try
            {
                command = new DataVirtualCommand(prmSQL, DataBase.Conexao, prmTimeOut);

                TimeCursor.Start();

                command.GetNoResults();

            }
            catch (Exception e)
            { Erro = e; }

            finally
            {
                TimeCursor.Stop();
            }

            return (IsOK);
        }

    }

    public class DataCursorBase : DataCursorError
    {

        public DataBase DataBase;

        public DataVirtualCommand command;

        internal Cronometro TimeCursor;

        public DataCursorBase()
        {
            TimeCursor = new Cronometro();
        }

    }

    public class DataCursorError
    {

        public Exception Erro;

        public bool IsOK => (Erro == null);

    }

}




