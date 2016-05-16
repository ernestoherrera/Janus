using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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

        public DbConnection(string connectionString)
        {            
            dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();
        }
    }
}
