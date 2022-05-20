using System.Diagnostics;
using System.Data;
using System.Collections.Generic;
using System;
using System.IO;

namespace Katty
{
    public class DataCursor : DataCursorDados
    {

        private string _sql;

        public string sql => _sql;

        public TraceLog Trace => DataBase.Trace;

        public TraceMSG Log;

        public DataCursor(string prmSQL, myMasks prmMask, DataBase prmDataBase)
        {

            DataBase = prmDataBase;

            _sql = GetTratarSQL(prmSQL);

            if (DataBase.IsOK)
            {
                SetQuery(); SetMasks(prmMask);
            }
            else
            { Trace.LogData.FailSQLNoDataBase(DataBase.tag, sql, DataBase.erro); Erro = DataBase.erro; }

            Log = Trace.Msg.Clonar();

        }

        private void SetQuery()
        {

            if (GetRequest(sql))
            {
                Trace.OnSqlExecutado(DataBase.tag, sql, TimeCursor.Elapsed.milliseconds, TemDados);
            }
            else
                Trace.OnSqlError(DataBase.tag, sql, Erro);
        }

        private string GetTratarSQL(string prmSQL) => Bloco.GetBlocoTroca(prmSQL, prmDelimitadorInicial: "<##>", prmDelimitadorFinal: "<##>", prmDelimitadorNovo: "'");

        private string GetLog()
        {
            string log = "";

            if (IsOK)
            {
                log = csv();
            }

            return log;
        }
    }

    public class DataCursorDados : DataCursorReader
    {
        
        public DataFormat Format => DataBase.Connect.Format;

        private DataTypesField DataTypes => DataBase.Connect.DataTypes;

        private TraceLog Trace => DataBase.Trace;

        public myMasks Masks;
        public bool HasMasks => GetHasMasks();

        public void SetMasks(myMasks prmMasks)
        {
            if (prmMasks != null)
                if (prmMasks.IsFull)
                    Masks = new myMasks(prmMasks);
        }

        public int qtdeColumns => reader.GetFieldCount;

        public bool IsFind(string prmColumn) => reader.IsFind(prmColumn);
        public bool IsDBNull(int prmIndice) => reader.IsDBNull(prmIndice);
        public string GetName(int prmIndice) => reader.GetName(prmIndice);
        public string GetType(int prmIndice) => reader.GetType(prmIndice);
        public string GetValor(int prmIndice) => GetFormat(prmIndice);
        public string GetValor(string prmColumn) => GetFormat(prmColumn);

        private string GetFormat(string prmColumn) => GetFormat(prmIndice: reader.GetIndex(prmColumn));
        private string GetFormat(int prmIndice)
        {
            string tipo = GetType(prmIndice);

            string column = GetName(prmIndice);

            if (IsDBNull(prmIndice))
                return "";

            if (DataTypes.IsTypeDate(tipo))
                return GetMaskDate(column, prmDate: reader.GetDateTime(prmIndice));

            if (DataTypes.IsTypeDouble(tipo))
               return GetMaskDouble(column, prmNumber: reader.GetDouble(prmIndice));

            return GetMaskText(column, prmText: reader.GetString(prmIndice));
        }

        private string GetMaskText(string prmColumn, string prmText)
        {
            if (HasMasks)
                return Format.GetTextFormat(prmText, Masks.GetFormat(prmColumn));

            return (prmText);
        }
        private string GetMaskDate(string prmColumn, DateTime prmDate)
        {
            if (HasMasks)
                return Format.GetDateFormat(prmDate, Masks.GetFormat(prmColumn)); 

            return (Format.GetDateFormat(prmDate));
        }
        private string GetMaskDouble(string prmColumn, Double prmNumber)
        {
            if (HasMasks)
                return Format.GetDoubleFormat(prmNumber, Masks.GetFormat(prmColumn));

            return (Format.GetDoubleFormat(prmNumber));
        }

        public string csv() => csv(prmSeparador: ",");
        public string csv(string prmSeparador)
        {
            xMemo memo = new xMemo(prmSeparador); string texto = "";

            if (TemDados)
            {

                for (int cont = 0; cont < qtdeColumns; cont++)
                {
                    if (IsDBNull(cont))
                        texto = "";
                    else
                        texto = GetValor(cont);

                    memo.Add(texto);
                }

            }

            return memo.txt;

        }
        public string json() 
        {
            if (TemDados)
            {
                xMemo memo = new xMemo(prmSeparador: ", ");

                for (int cont = 0; cont < qtdeColumns; cont++)
                {
                    if (!IsDBNull(cont))
                          memo.Add(GetTupla(cont));
                }

                return ("{ " + memo.txt + " }");
            }
            return ("{ }");
        }
        public string GetTupla(int prmIndice) => string.Format("'{0}': '{1}'", GetName(prmIndice), GetValor(prmIndice));

        private bool GetHasMasks()
        {
            if (Masks == null) return false;

            return Masks.IsFull;
        }

    }

    public class DataCursorReader : DataCursorBase
    {

        public DataVirtualReader reader;

        public bool TemDados;

        public bool GetRequest(string prmSQL)
        {

            Erro = null; TemDados = false;

            try
            {
                command = new DataVirtualCommand(prmSQL, DataBase.Conexao, timeoutSQL);

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

        public bool Execute(string prmSQL)
        {

            Erro = null;

            try
            {
                command = new DataVirtualCommand(prmSQL, DataBase.Conexao, timeoutSQL);

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

        internal int timeoutSQL => DataBase.Connect.timeoutSQL;

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




