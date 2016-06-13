using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using JanusData.Support;
using System.Configuration;

namespace JanusData
{
    public class DbConnection : IDbConnection
    {
        private IDbConnection dbConnection;
        public string ConnectionString
        {
            get { return dbConnection.ConnectionString; }

            set { dbConnection.ConnectionString = value; }
        }

        public int ConnectionTimeout
        {
            get { return dbConnection.ConnectionTimeout; }
        }

        public string Database
        {
            get { return dbConnection.Database; }
        }

        public ConnectionState State
        {
            get { return dbConnection.State; }
        }

        public IDbTransaction BeginTransaction()
        {
            return dbConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return dbConnection.BeginTransaction(il);
        }

        public void Close()
        {
            dbConnection.Close();
        }

        public void ChangeDatabase(string databaseName)
        {
            dbConnection.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return dbConnection.CreateCommand();
        }

        public void Open()
        {
            dbConnection.Open();
        }

        public void Dispose()
        {
            if (dbConnection.State == ConnectionState.Open)
                dbConnection.Close();

            dbConnection.Dispose();
        }

        public DbConnection(string connectionName = null)
        {
            if(connectionName == null)
                connectionName = Constants.CONNECTION_NAME;

            if (string.IsNullOrEmpty(connectionName))
                throw new ApplicationException("Can not have an empty connection name.");

            var connStringSettings = ConfigurationManager.ConnectionStrings[connectionName];

            if (connStringSettings == null)
                throw new ApplicationException(string.Format("There is no connection string with connection name {0}", connectionName));

            var connectionString = connStringSettings.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
                throw new ApplicationException(string.Format("The connection name: {0} has an empty connection string.", connectionName));

            dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();
        }
    }
}
