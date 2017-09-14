using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebDisplay1.Utils
{
    public class DatabaseUtils
    {
        private static readonly string ConnectionString =
           ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

        private static readonly SqlConnection Connection = new SqlConnection(ConnectionString);

        public static SqlConnection GetConnection()
        {
            if (Connection.State == ConnectionState.Open || Connection.State == ConnectionState.Connecting)
            {
                return Connection;
            }
            Connection.Open();
            return Connection;
        }

        /// <summary>
        /// Helper method to read and convert data from a DbDataReader
        /// </summary>
        /// <typeparam name="T">Desired data type</typeparam>
        /// <param name="dataReader">Data reader to read from</param>
        /// <param name="fieldName">Name of the database column</param>
        /// <returns>Value from database in given data type</returns>
        public static T Read<T>(DbDataReader dataReader, string fieldName)
        {
            int fieldIndex;
            try
            {
                fieldIndex = dataReader.GetOrdinal(fieldName);
            }
            catch
            {
                return default(T);
            }

            if (dataReader.IsDBNull(fieldIndex))
            {
                return default(T);
            }

            var readData = dataReader.GetValue(fieldIndex);
            if (readData is T)
            {
                return (T)readData;
            }

            try
            {
                return (T)Convert.ChangeType(readData, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }
    }
}