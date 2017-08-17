using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace BCS.Core.DAL
{
    /// <summary>
    /// Provides methods for interacting with a database.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        DbParameter CreateParameter(string name, DbType type, object value);


        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="value">Direction of the parameter Input/Output.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        DbParameter CreateParameter(string name, DbType type, object value,ParameterDirection parameterdirection);

        /// <summary>
        /// Creates a SqlParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>Returns a SqlParameter created with the given arguments.</returns>
        SqlParameter CreateParameter(string name, SqlDbType type, object value);

        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="value">Direction of the parameter Input/Output.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        DbParameter CreateParameter(string name, SqlDbType type, object value, ParameterDirection parameterdirection);


        /// <summary>
        /// Creates a DbParameter with the given name, type, and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="value">The size of the parameter.</param>
        /// <param name="value">Direction of the parameter Input/Output.</param>
        /// <returns>Returns a DbParameter created with the given arguments.</returns>
        DbParameter CreateParameter(string name, SqlDbType type, object value,int size, ParameterDirection parameterdirection);

        /// <summary>
        /// Gets the value of the field in the given DataRow, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The expected type of the given field in the given datarow.</typeparam>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        /// Returns the value of the field in the given DataRow, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        T GetValueOrDefault<T>(DataRow dataRow, string columnName);

        /// <summary>
        /// Gets the value of the field in the given DataRowView, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </summary>
        /// <typeparam name="T">The expected type of the given field in the given datarow.</typeparam>
        /// <param name="dataRowView">The data row view.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>
        /// Returns the value of the field in the given DataRowView, cast as TypeParam T, if the field is not null; otherwise, it returns the default of the TypeParam T.
        /// </returns>
        T GetValueOrDefault<T>(DataRowView dataRowView, string columnName);

        /// <summary>
        /// Executes the given stored procedure, returning the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns the first table in the result set of the stored procedure.</returns>
        DataTable ExecuteDataTable(string connectionString, string storedProcedure, params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure, returning a DataTableReader for the first table in the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns a DataTableReader for the first table in the result set returned from the stored procedure.</returns>
        IDataReader ExecuteReader(string connectionString, string storedProcedure, params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure, returning the result set returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns the result set of the stored procedure.</returns>
        DataSet ExecuteDataSet(string connectionString, string storedProcedure, params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        void ExecuteNonQuery(string connectionString, string storedProcedure, params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        void ExecuteNonQuery(string connectionString, string storedProcedure, IEnumerable<DbParameter> inParameters);

        /// <summary>
        /// Executes the given stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        DbParameterCollection ExecuteNonQueryReturnOutputParams(string connectionString, string storedProcedure,  params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns the scalar result of the stored procedure.</returns>
        object ExecuteScalar(string connectionString, string storedProcedure, params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure, cast as the given T TypeParam.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns the scalar result of the stored procedure, cast as the given T TypeParam.</returns>
        T ExecuteScalar<T>(string connectionString, string storedProcedure, params DbParameter[] inParameters);

        /// <summary>
        /// Executes the given stored procedure, returning the scalar value returned from the stored procedure, cast as the given T TypeParam.
        /// </summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="inParameters">The parameters to pass into the stored procedure.</param>
        /// <returns>Returns the scalar result of the stored procedure, cast as the given T TypeParam.</returns>
        T ExecuteScalar<T>(string connectionString, string storedProcedure, IEnumerable<DbParameter> inParameters);

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
        List<T> ExecuteList<T>(string connectionString, string storedProcedure, string field, params DbParameter[] inParameters);

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
        List<T> ExecuteList<T>(string connectionString, string storedProcedure, Func<DataRow, T> funcToCreateObject, params DbParameter[] parameters);

        /// <summary>
        /// Executes the given stored procedure, as a batch update, passing in the given dataset and parameters.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="dataSet">The data set.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="inParameters">The in parameters.</param>
        void ExecuteBatchUpdate(string connectionString, string storedProcedure, DataSet dataSet, string tableName, params DbParameter[] inParameters);
    }
}
