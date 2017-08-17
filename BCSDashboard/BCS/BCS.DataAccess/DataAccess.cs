using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace BCS.Core.DAL
{
    /// <summary>
    /// Provides a concrete implementation of IDataAccess providing data access to a database.
    /// </summary>   
    public class DataAccess : IDataAccess
    {
        #region Fields

        /// <summary>
        /// The number of seconds to wait before timing out a transaction, defaulted to 180 if not set in the config.
        /// </summary>
        private static readonly int CommandTimeOutInSeconds = GetFromConfig<int>("CommandTimeOutInSeconds", 30);

        /// <summary>
        /// The number of record to process in a transaction, defaulted to 1000 if not set in the config.
        /// </summary>
        private static readonly int BulkCopyBatchSize = GetFromConfig<int>("BulkCopyBatchSize", 1000);

        #endregion

        #region Standard Data Access
        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        public static DbParameter CreateParameter(string name, DbType type, object value)
        {
            return new SqlParameter { ParameterName = name, DbType = type, Value = value ?? DBNull.Value };
        }

        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        public static DbParameter CreateParameter(string name, DbType type, object value,ParameterDirection parameterdirection)
        {
            SqlParameter sqlparameter = new SqlParameter { ParameterName = name, DbType = type, Value = value ?? DBNull.Value };
            sqlparameter.Direction = parameterdirection;

            return sqlparameter;
        }

        /// <summary>
        /// Creates a SqlParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a SqlParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        public static SqlParameter CreateParameter(string name, SqlDbType type, object value)
        {
            return new SqlParameter { ParameterName = name, SqlDbType = type, Value = value ?? DBNull.Value };
        }

        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        public static DbParameter CreateParameter(string name, SqlDbType type, object value, ParameterDirection parameterdirection)
        {
            SqlParameter sqlparameter = new SqlParameter { ParameterName = name, SqlDbType = type, Value = value ?? DBNull.Value };
            sqlparameter.Direction = parameterdirection;

            return sqlparameter;
        }


        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="value">The size of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        public static DbParameter CreateParameter(string name, SqlDbType type, object value,int size, ParameterDirection parameterdirection)
        {
            SqlParameter sqlparameter = new SqlParameter { ParameterName = name, SqlDbType = type,Size=size, Value = value ?? DBNull.Value };
            sqlparameter.Direction = parameterdirection;

            return sqlparameter;
        }

        /// <summary>
        /// Gets the value of the field in the given DataRow, cast as TypeParam T, if the field is not null or DBNull.Value; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The expected type of the given field in the given datarow.</typeparam>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        /// Returns the value of the field in the given DataRow, cast as TypeParam T, if the field is not null or DBNull.Value; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "We are validating the parameter with a Guard class. Code-Analysis is not smart enough to detect this.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DataRow", Justification = "Can be suppressed until we begin using Resource dictionaries for strings.")]
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "RRD.DSA.Core.Guard.Requires(System.Boolean,System.String)", Justification = "This is fine until we start to use Resource tables.")]
        public static T GetValueOrDefault<T>(DataRow dataRow, string columnName)
        {
            Guard.Requires(dataRow != null, string.Format("Unable to get the value of column \"{0}\" from a null DataRow.", columnName));
            return GetValueOrDefault<T>(dataRow[columnName]);
        }

        /// <summary>
        /// Gets the value of the field in the given DataRowView, cast as TypeParam T, if the field is not null or DBNull.Value; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The expected type of the given field in the given datarow.</typeparam>
        /// <param name="dataRowView">The data row view.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        /// Returns the value of the field in the given DataRowView, cast as TypeParam T, if the field is not null or DBNull.Value; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "We are validating the parameter with a Guard class. Code-Analysis is not smart enough to detect this.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DataRowView", Justification = "Can be suppressed until we begin using Resource dictionaries for strings.")]
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "RRD.DSA.Core.Guard.Requires(System.Boolean,System.String)", Justification = "This is fine until we start to use Resource tables.")]
        public static T GetValueOrDefault<T>(DataRowView dataRowView, string columnName)
        {
            Guard.Requires(dataRowView != null, string.Format("Unable to get the value of column \"{0}\" from a null DataRowView.", columnName));
            return GetValueOrDefault<T>(dataRowView[columnName]);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the first table in the result set of the stored procedure.
        /// </returns>
        public static DataTable ExecuteDataTable(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return ExecuteDataTable(connectionString, storedProcedure, 0, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="commandTimeoutInSeconds">The command timeout in seconds.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the first table in the result set of the stored procedure.
        /// </returns>
        public static DataTable ExecuteDataTable(string connectionString, string storedProcedure, int commandTimeoutInSeconds, params DbParameter[] inParameters)
        {
            DataSet dataSet = ExecuteDataSet(connectionString, storedProcedure, commandTimeoutInSeconds, inParameters);
            return dataSet != null && dataSet.Tables.Count > 0 ?
                dataSet.Tables[0] : null;
        }

        /// <summary>
        /// Executes the given stored procedure, returning a DataTableReader for the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns a DataTableReader for the first table in the result set returned from the stored procedure.
        /// </returns>
        public static IDataReader ExecuteReader(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            //DataSet dataSet = ExecuteDataSet(connectionString, storedProcedure, 0, inParameters);
            //return dataSet != null && dataSet.Tables.Count > 0 ?
            //    dataSet.Tables[0].CreateDataReader() : null;

            IDataReader reader = null;

           
            Database database = CreateDatabase(connectionString);

            using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
            {

                    command.CommandTimeout = 0;


                AddInParameters(database, command, inParameters);
                reader = database.ExecuteReader(command);
            }
            

            return reader;
        }

        /// <summary>
        /// Executes the given stored procedure, returning the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the result set of the stored procedure.
        /// </returns>
        public static DataSet ExecuteDataSet(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return ExecuteDataSet(connectionString, storedProcedure, 0, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="commandTimeoutInSeconds">The command timeout in seconds.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the result set of the stored procedure.
        /// </returns>
        public static DataSet ExecuteDataSet(string connectionString, string storedProcedure, int commandTimeoutInSeconds, params DbParameter[] inParameters)
        {
            DataSet dataSet = null;

            Transaction.ExecuteWithRetryOnDeadlockOrTimeout(() =>
            {
                Database database = CreateDatabase(connectionString);

                using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
                {
                    if (commandTimeoutInSeconds != 0)
                    {
                        command.CommandTimeout = commandTimeoutInSeconds;
                    }

                    AddInParameters(database, command, inParameters);
                    dataSet = database.ExecuteDataSet(command);
                }
            });

            return dataSet;
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        public static void ExecuteNonQuery(string connectionString, string storedProcedure, IEnumerable<DbParameter> inParameters)
        {
            Transaction.ExecuteWithRetryOnDeadlockOrTimeout(() =>
            {
                Database database = CreateDatabase(connectionString);

                using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
                {
                    command.CommandTimeout = CommandTimeOutInSeconds;
                    AddInParameters(database, command, inParameters);
                    database.ExecuteNonQuery(command);
                }
            });
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        public static void ExecuteNonQuery(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            Transaction.ExecuteWithRetryOnDeadlockOrTimeout(() =>
            {
                Database database = CreateDatabase(connectionString);

                using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
                {
                    command.CommandTimeout = CommandTimeOutInSeconds;
                    AddInParameters(database, command, inParameters);
                    database.ExecuteNonQuery(command);
                }
            });
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        public static DbParameterCollection ExecuteNonQueryReturnOutputParams(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            DbParameterCollection parametercollection = null;
            
            Transaction.ExecuteWithRetryOnDeadlockOrTimeout(() =>
            {
                Database database = CreateDatabase(connectionString);

                using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
                {
                    command.CommandTimeout = CommandTimeOutInSeconds;
                    AddInParameters(database, command, inParameters);
                    database.ExecuteNonQuery(command);
                    parametercollection = command.Parameters;
                }
            });
            return parametercollection;
        }

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the scalar result of the stored procedure.
        /// </returns>
        public static object ExecuteScalar(string connectionString, string storedProcedure, IEnumerable<DbParameter> inParameters)
        {
            object scalar = null;

            Transaction.ExecuteWithRetryOnDeadlockOrTimeout(() =>
            {
                Database database = CreateDatabase(connectionString);

                using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
                {
                    command.CommandTimeout = CommandTimeOutInSeconds;
                    AddInParameters(database, command, inParameters);
                    scalar = database.ExecuteScalar(command);
                }
            });

            return scalar;
        }

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the scalar result of the stored procedure.
        /// </returns>
        public static object ExecuteScalar(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            object scalar = null;

            Transaction.ExecuteWithRetryOnDeadlockOrTimeout(() =>
            {
                Database database = CreateDatabase(connectionString);

                using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
                {
                    command.CommandTimeout = CommandTimeOutInSeconds;
                    AddInParameters(database, command, inParameters);
                    scalar = database.ExecuteScalar(command);
                }
            });

            return scalar;
        }

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure, cast as the given T TypeParam.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the scalar result of the stored procedure, cast as the given T TypeParam.
        /// </returns>
        public static T ExecuteScalar<T>(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return GetValueOrDefault<T>(
                ExecuteScalar(
                    connectionString,
                    storedProcedure,
                    inParameters));
        }

        /// <summary>
        /// Executes the given stored procedure, returning a generic List constructed from the given field in the result returned from the stored procedure.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="field">The field to construct the list from.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns a generic List constructed from the given field in the result returned from the stored procedure.
        /// </returns>
        public static List<T> ExecuteList<T>(string connectionString, string storedProcedure, string field, params DbParameter[] inParameters)
        {
            return ExecuteList<T>(connectionString, storedProcedure, (dataRow) => GetValueOrDefault<T>(dataRow, field), inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning a generic List constructed using the given Func and the result returned from the stored procedure.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="funcToCreateObject">A function taking a DataRow, used to create the objects returned in the list.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Returns a generic List constructed using the given Func and the result returned from the stored procedure.
        /// </returns>
        public static List<T> ExecuteList<T>(string connectionString, string storedProcedure, Func<DataRow, T> funcToCreateObject, params DbParameter[] parameters)
        {
            var list = new List<T>();

            DataTable dataTable = DataAccess.ExecuteDataTable(connectionString, storedProcedure, parameters);

            if (dataTable != null && funcToCreateObject != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    list.Add(funcToCreateObject(dataRow));
                }
            }

            return list;
        }

        /// <summary>
        /// Executes the given stored procedure, as a batch update, passing in the given dataset and parameters.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="dataSet">The data set.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="inParameters">The in parameters.</param>
        public static void ExecuteBatchUpdate(string connectionString, string storedProcedure, DataSet dataSet, string tableName, params DbParameter[] inParameters)
        {
            var database = CreateDatabase(connectionString);

            using (DbCommand command = database.GetStoredProcCommand(storedProcedure))
            {
                AddInBatchParameters(database, command, inParameters);

                database.UpdateDataSet(dataSet, tableName, null, command, null, UpdateBehavior.Transactional, 0);
            }
        }

        /// <summary>
        /// Executes the bulk Copy, as a batch Insert, passing in the given dataTable 
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dataTable">The Data Table.</param>
        /// <param name="tableName">Name of the table.</param>
        public static void ExecuteBulkCopy(string connectionString, DataTable dataTable, string tableName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.UseInternalTransaction, null))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = BulkCopyBatchSize;
                    bulkCopy.BulkCopyTimeout = CommandTimeOutInSeconds;
                    con.Open();
                    //// write the data in the "dataTable"
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }

        #endregion

        #region IDataAccess Interface
        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        DbParameter IDataAccess.CreateParameter(string name, DbType type, object value)
        {
            return DataAccess.CreateParameter(name, type, value);
        }

        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        DbParameter IDataAccess.CreateParameter(string name, DbType type, object value,ParameterDirection parameterdirection)
        {
            return DataAccess.CreateParameter(name, type, value,parameterdirection);
        }

        /// <summary>
        /// Creates a SqlParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a SqlParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        SqlParameter IDataAccess.CreateParameter(string name, SqlDbType type, object value)
        {
            return DataAccess.CreateParameter(name, type, value);
        }



        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        DbParameter IDataAccess.CreateParameter(string name, SqlDbType type, object value, ParameterDirection parameterdirection)
        {
            return DataAccess.CreateParameter(name, type, value, parameterdirection);
        }

        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="value">The size of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        [DebuggerStepThrough]
        DbParameter IDataAccess.CreateParameter(string name, SqlDbType type, object value,int size, ParameterDirection parameterdirection)
        {
            return DataAccess.CreateParameter(name, type, value,size, parameterdirection);
        }

        /// <summary>
        /// Gets the value of the field in the given DataRow, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The expected type of the given field in the given datarow.</typeparam>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        /// Returns the value of the field in the given DataRow, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        T IDataAccess.GetValueOrDefault<T>(DataRow dataRow, string columnName)
        {
            return DataAccess.GetValueOrDefault<T>(dataRow, columnName);
        }



        /// <summary>
        /// Gets the value of the field in the given DataRowView, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The expected type of the given field in the given datarow.</typeparam>
        /// <param name="dataRowView">The data row view.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        /// Returns the value of the field in the given DataRowView, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        T IDataAccess.GetValueOrDefault<T>(DataRowView dataRowView, string columnName)
        {
            return DataAccess.GetValueOrDefault<T>(dataRowView, columnName);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns the first table in the result set of the stored procedure.</returns>
        DataTable IDataAccess.ExecuteDataTable(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return DataAccess.ExecuteDataTable(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning a DataTableReader for the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns a DataTableReader for the first table in the result set returned from the stored procedure.</returns>
        IDataReader IDataAccess.ExecuteReader(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return DataAccess.ExecuteReader(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the result set of the stored procedure.
        /// </returns>
        DataSet IDataAccess.ExecuteDataSet(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return DataAccess.ExecuteDataSet(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        void IDataAccess.ExecuteNonQuery(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            DataAccess.ExecuteNonQuery(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        void IDataAccess.ExecuteNonQuery(string connectionString, string storedProcedure, IEnumerable<DbParameter> inParameters)
        {
            DataAccess.ExecuteNonQuery(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        DbParameterCollection IDataAccess.ExecuteNonQueryReturnOutputParams(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
           return  DataAccess.ExecuteNonQueryReturnOutputParams(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the scalar result of the stored procedure.
        /// </returns>
        object IDataAccess.ExecuteScalar(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return DataAccess.ExecuteScalar(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure, cast as the given T TypeParam.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the scalar result of the stored procedure, cast as the given T TypeParam.
        /// </returns>
        T IDataAccess.ExecuteScalar<T>(string connectionString, string storedProcedure, params DbParameter[] inParameters)
        {
            return DataAccess.ExecuteScalar<T>(connectionString, storedProcedure, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure, cast as the given T TypeParam.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns the scalar result of the stored procedure, cast as the given T TypeParam.
        /// </returns>
        T IDataAccess.ExecuteScalar<T>(string connectionString, string storedProcedure, IEnumerable<DbParameter> inParameters)
        {
            return GetValueOrDefault<T>(ExecuteScalar(connectionString, storedProcedure, inParameters));
        }

        /// <summary>
        /// Executes the given stored procedure, returning a generic List constructed from the given field in the result returned from the stored procedure.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="field">The field to construct the list from.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>
        /// Returns a generic List constructed from the given field in the result returned from the stored procedure.
        /// </returns>
        List<T> IDataAccess.ExecuteList<T>(string connectionString, string storedProcedure, string field, params DbParameter[] inParameters)
        {
            return DataAccess.ExecuteList<T>(connectionString, storedProcedure, field, inParameters);
        }

        /// <summary>
        /// Executes the given stored procedure, returning a generic List constructed using the given Func and the result returned from the stored procedure.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="funcToCreateObject">A function taking a DataRow, used to create the objects returned in the list.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Returns a generic List constructed using the given Func and the result returned from the stored procedure.
        /// </returns>
        List<T> IDataAccess.ExecuteList<T>(string connectionString, string storedProcedure, Func<DataRow, T> funcToCreateObject, params DbParameter[] parameters)
        {
            return DataAccess.ExecuteList<T>(connectionString, storedProcedure, funcToCreateObject, parameters);
        }

        /// <summary>
        /// Executes the given stored procedure, as a batch update, passing in the given dataset and parameters.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="dataSet">The data set.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="inParameters">The in parameters.</param>
        void IDataAccess.ExecuteBatchUpdate(string connectionString, string storedProcedure, DataSet dataSet, string tableName, params DbParameter[] inParameters)
        {
            DataAccess.ExecuteBatchUpdate(connectionString, storedProcedure, dataSet, tableName, inParameters);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the object, cast as TypeParam T, if the object is not null or equal to DBNull.Value; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="o">The object to return as the given TypeParam T.</param>
        /// <returns>
        /// Returns the object, cast as TypeParam T, if the object is not null or equal to DBNull.Value; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        private static T GetValueOrDefault<T>(object o)
        {
            Type type = typeof(T);

            if (o != null && !DBNull.Value.Equals(o))
            {
                if (type.IsEnum)
                {
                return (T)Enum.Parse(type, o.ToString(), true);
                }
                else if (type.IsClass || type.IsValueType)
                {
                    return (T)o;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Adds the parameters to the command.
        /// If the parameter can be cast to ICloneable, this method will first clone the parameter before adding it to the command;
        /// otherwise, the parameter is added indirectly through Database.AddInParameter.
        /// This is necessary because the parameters are created outside of calls through Transaction.ExecuteWithRetryOnDeadlockOrTimeout.
        /// This means that if there is a retry because of a deadlock or timeout, the parameter may have already been added to a SqlParameterCollection,
        /// and any attempt at this point to re-add the parameter to another command results in an ArgumentException with the message
        /// "The SqlParameter is already contained by another SqlParameterCollection.".
        /// You might be thinking, why don't we always just use Database.AddInParameter?
        /// The reason we do not, is that our parameters might have a Sql Server specific SqlDbType, such as SqlDbType.Structured,
        /// which doesn't have a corresponding DbType.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The parameters.</param>
        private static void AddInParameters(
            Database database,
            DbCommand command,
            IEnumerable<DbParameter> parameters)
        {
            if (command != null && parameters != null && parameters.Any())
            {
                foreach (DbParameter parameter in parameters)
                {
                    parameter.Value = parameter.Value ?? DBNull.Value;

                    ICloneable cloneable = parameter as ICloneable;

                    if (cloneable != null)
                    {
                        DbParameter clonedParameter = (DbParameter)cloneable.Clone();
                        command.Parameters.Add(clonedParameter);
                    }
                    else
                    {
                        database.AddInParameter(command, parameter.ParameterName, parameter.DbType, parameter.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the database using the given connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Returns a database created from the given connection string.</returns>
        private static Database CreateDatabase(string connectionString)
        {
            return new SqlDatabase(connectionString);
        }

        /// <summary>
        /// Adds the batch parameters to the command
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="command">The command.</param>
        /// <param name="inParameters">The parameters.</param>
        private static void AddInBatchParameters(Database database, DbCommand command, params DbParameter[] inParameters)
        {
            if (command != null && inParameters != null)
            {
                foreach (DbParameter parameter in inParameters)
                {
                    database.AddInParameter(command, parameter.ParameterName, parameter.DbType, parameter.ParameterName, DataRowVersion.Current);
                }
            }
        }

        /// <summary>
        /// Gets a value from the config if it exists, otherwise returning the default value of T.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="key">The key in the config.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Returns a value from the config if it exists, otherwise returning the default value of T.</returns>
        private static T GetFromConfig<T>(string key, T defaultValue) where T : IConvertible, new()
        {
            string value = ConfigurationManager.AppSettings[key];

            return !string.IsNullOrEmpty(value) ?
                (T)Convert.ChangeType(value, typeof(T)) :
                defaultValue;
        }

        #endregion
    }
}
